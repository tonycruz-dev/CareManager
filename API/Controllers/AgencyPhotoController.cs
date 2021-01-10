using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Controllers
{
    public class AgencyPhotoController : BaseApiController
    {
        private readonly IAgencyPhotoRepository _repo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public AgencyPhotoController(IAgencyPhotoRepository repo, UserManager<AppUser> userManager, IMapper mapper,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _repo = repo;
            _userManager = userManager;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
               _cloudinaryConfig.Value.CloudName,
               _cloudinaryConfig.Value.ApiKey,
               _cloudinaryConfig.Value.ApiSecret
           );

            _cloudinary = new Cloudinary(acc);
        }
        [HttpGet("{id}", Name = "GetAgencyPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _repo.GetPhoto(id);

            var photo = _mapper.Map<AgencyPhotoToReturnDto>(photoFromRepo);

            return Ok(photo);
        }
        [HttpGet(Name = "GetAgentPhotos")]
        public async Task<ActionResult<IReadOnlyList<AgencyPhotoToReturnDto>>> GetPhotos()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
            var agency = await _repo.GetAgencyByUserIdAsync(user.Id);

            var photoFromRepo = await _repo.GetPhotos(agency.Id);

            var photo = _mapper.Map<IReadOnlyList<AgencyPhotoToReturnDto>>(photoFromRepo);

            return Ok(photo);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser([FromForm]AgencyPhotoForCreation photoForCreationDto)
        {

            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
            var userFromRepo = await _repo.GetAgencyByUserIdAsync(user.Id);

            var file = photoForCreationDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation()
                            .Width(500).Height(500).Crop("fill").Gravity("face"),
                        Folder = "CareManager/"
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            var urlToReplace = uploadResult.Uri.AbsoluteUri.ToString();
            var replaceUrl = urlToReplace.Replace("http:", "https:");
            photoForCreationDto.Url = replaceUrl;
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<AgencyPhoto>(photoForCreationDto);
            var firstPhoto = false;
            if (!userFromRepo.AgencyPhotos.Any(u => u.IsMain))
            {
                photo.IsMain = true;
                firstPhoto = true;
            } 
            photo.AgencyId = userFromRepo.Id;
            var newPhoto = await _repo.CreatePhotoAsync(photo);
            if(firstPhoto)
            {
                user.Avatar = newPhoto.Url;
                await _repo.UpdateUserPhotoAsync(user);
            }

            if (newPhoto != null)
            {
                var photoToReturn = _mapper.Map<AgencyPhoto, AgencyPhotoToReturnDto>(newPhoto);
                return CreatedAtRoute("GetAgencyPhoto", new { id = photo.Id }, photoToReturn);
            }

            return BadRequest("Could not add the photo");
        }

        [HttpPut("setMain/{id}")]
        public async Task<IActionResult> SetMainPhoto(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var userFromRepo = await _repo.GetAgencyByUserIdAsync(user.Id);
            if (!userFromRepo.AgencyPhotos.Any(p => p.Id == id))
                return Unauthorized();

            var photoFromRepo = await _repo.GetPhoto(id);

            //if (photoFromRepo.IsMain)
            //    return BadRequest("This is already the main photo");

            var currentMainPhoto = await _repo.GetMainPhotoForUserAsync(userFromRepo.Id);
            currentMainPhoto.IsMain = false;
            await _repo.UpdatePhotoAsync(photoFromRepo);
            photoFromRepo.IsMain = true;
            var updatedphoto = await _repo.UpdatePhotoAsync(photoFromRepo);
           
            if (updatedphoto != null) 
            { 
              user.Avatar = updatedphoto.Url;
              await _repo.UpdateUserPhotoAsync(user);
              return NoContent();
            }
                

            return BadRequest("Could not set photo to main");
        }

        [HttpDelete("DeleteAgentPhoto/{id}")]
        public async Task<IActionResult> DeleteAgentPhoto(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();


            var photoFromRepo = await _repo.GetPhoto(id);

            if (photoFromRepo.IsMain)
                return BadRequest("You cannot delete your main photo");

            if (photoFromRepo.PublicId != null)
            {
                var deleteParams = new DeletionParams(photoFromRepo.PublicId);

                var result = _cloudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                {
                    await _repo.DeletePhotoAsync(photoFromRepo.Id);
                    return Ok();
                }
            }

            if (photoFromRepo.PublicId == null)
            {
                //_repo.Delete(photoFromRepo);
                await _repo.DeletePhotoAsync(photoFromRepo.Id);
                return Ok();
            }

            return BadRequest("Failed to delete the photo");
        }



    }
}
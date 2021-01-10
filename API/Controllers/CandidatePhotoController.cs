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
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Controllers
{

    public class CandidatePhotoController : BaseApiController
    {
        private readonly ICandidatePhotoRepository _repo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public CandidatePhotoController(ICandidatePhotoRepository repo, UserManager<AppUser> userManager, IMapper mapper,
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
        [HttpGet("{id}", Name = "GetCandidatePhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _repo.GetPhoto(id);

            var photo = _mapper.Map<CandidatePhotoToReturnDto>(photoFromRepo);

            return Ok(photo);
        }

        [HttpGet(Name = "GetPhotos")]
        public async Task<ActionResult<IReadOnlyList<CandidatePhotoToReturnDto>>> GetPhotos()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
            var candidate = await _repo.GetCandidateByUserIdAsync(user.Id);

            var photoFromRepo = await _repo.GetPhotos(candidate.Id);

            var photo = _mapper.Map<IReadOnlyList<CandidatePhotoToReturnDto>>(photoFromRepo);

            return Ok(photo);
        }
        // Task<IReadOnlyList<CandidatePhoto>> GetPhotos(int id)
        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser([FromForm]CandidatePhotoForCreationDto photoForCreationDto)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
           var userFromRepo = await _repo.GetCandidateByUserIdAsync(user.Id);

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

            var photo = _mapper.Map<CandidatePhoto>(photoForCreationDto);

            if (!userFromRepo.CandidatePhotos.Any(u => u.IsMain))
                photo.IsMain = true;
            photo.CandidateId = userFromRepo.Id;
            var newPhoto = await _repo.CreatePhotoAsync(photo);
            user.Avatar = newPhoto.Url;
            await _repo.UpdateUserPhotoAsync(user);

            if (newPhoto != null)
            {
                var photoToReturn = _mapper.Map<CandidatePhoto, CandidatePhotoToReturnDto>(newPhoto);
                return CreatedAtRoute("GetCandidatePhoto", new { id = photo.Id }, photoToReturn);
            }

            return BadRequest("Could not add the photo");
        }

        [HttpPut("setMain/{id}")]
        public async Task<IActionResult> SetMainPhoto(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var userFromRepo = await _repo.GetCandidateByUserIdAsync(user.Id);
            if (!userFromRepo.CandidatePhotos.Any(p => p.Id == id))
                return Unauthorized();

            var photoFromRepo = await _repo.GetPhoto(id);

            //if (photoFromRepo.IsMain)
            //    return BadRequest("This is already the main photo");

            var currentMainPhoto = await _repo.GetMainPhotoForUserAsync(userFromRepo.Id);
            currentMainPhoto.IsMain = false;
            await _repo.UpdatePhotoAsync(photoFromRepo);
            photoFromRepo.IsMain = true;
           var updatedphoto = await _repo.UpdatePhotoAsync(photoFromRepo);
            user.Avatar = photoFromRepo.Url;
           await _repo.UpdateUserPhotoAsync(user);
            if (updatedphoto != null)
                return NoContent();

            return BadRequest("Could not set photo to main");
        }

        [HttpDelete("DeletePhoto/{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
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
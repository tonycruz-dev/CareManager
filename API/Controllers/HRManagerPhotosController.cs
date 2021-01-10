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
    public class HRManagerPhotosController : BaseApiController
    {
        private readonly ICandidatePhotoRepository _repo;
        private readonly IAgencyPhotoRepository _repoAgency;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public HRManagerPhotosController(
            ICandidatePhotoRepository repo,
            IAgencyPhotoRepository repoAgency,
            UserManager<AppUser> userManager, 
            IMapper mapper,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _repo = repo;
            _repoAgency = repoAgency;
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

        [HttpGet("GetHRCandidatePhoto/{id}")]
        public async Task<IActionResult> GetHRCandidatePhoto(int id)
        {
            var photoFromRepo = await _repo.GetPhoto(id);

            var photo = _mapper.Map<CandidatePhotoToReturnDto>(photoFromRepo);

            return Ok(photo);
        }

        [HttpGet("GetHRCandidatePhotos/{id}")]
        public async Task<ActionResult<IReadOnlyList<CandidatePhotoToReturnDto>>> GetHRCandidatePhotos(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
            var candidate = await _repo.GetCandidateByIdAsync(id);

            var photoFromRepo = await _repo.GetPhotos(candidate.Id);

            var photo = _mapper.Map<IReadOnlyList<CandidatePhotoToReturnDto>>(photoFromRepo);

            return Ok(photo);
        }
        // Task<IReadOnlyList<CandidatePhoto>> GetPhotos(int id)
        [HttpPost("AddHRCandidatePhotoForUser")]
        public async Task<IActionResult> AddHRCandidatePhotoForUser([FromForm] CandidatePhotoForCreationDto photoForCreationDto)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var userFromRepo = await _repo.GetCandidateByIdAsync(photoForCreationDto.CandidateId);

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

            //if (!userFromRepo.CandidatePhotos.Any(u => u.IsMain))
            //    photo.IsMain = true;
            photo.CandidateId = userFromRepo.Id;
            var newPhoto = await _repo.CreatePhotoAsync(photo);
            // user.Avatar = newPhoto.Url;
            await _repo.UpdateHRUserPhotoAsync(photoForCreationDto.CandidateId, newPhoto.Url);
            // await _repo.UpdateUserPhotoAsync(user);

            if (newPhoto != null)
            {
                var photoToReturn = _mapper.Map<CandidatePhoto, CandidatePhotoToReturnDto>(newPhoto);
                return CreatedAtRoute("GetCandidatePhoto", new { id = photo.Id }, photoToReturn);
            }

            return BadRequest("Could not add the photo");
        }

        [HttpPut("setHRCandidateMain")]
        public async Task<IActionResult> setHRCandidateMain(HRCandidateUpdatePhotoDto setMain)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var userFromRepo = await _repo.GetCandidateByIdAsync(setMain.CandidateId);
            if (!userFromRepo.CandidatePhotos.Any(p => p.Id == setMain.Id))
                return Unauthorized();

            var photoFromRepo = await _repo.GetPhoto(setMain.Id);

            //if (photoFromRepo.IsMain)
            //    return BadRequest("This is already the main photo");

            var currentMainPhoto = await _repo.GetMainPhotoForUserAsync(userFromRepo.Id);
            if(currentMainPhoto != null)
               currentMainPhoto.IsMain = false;

            await _repo.UpdatePhotoAsync(photoFromRepo);
            photoFromRepo.IsMain = true;
            var updatedphoto = await _repo.UpdatePhotoAsync(photoFromRepo);
            //user.Avatar = photoFromRepo.Url;
            await _repo.UpdateHRUserPhotoAsync(setMain.CandidateId, photoFromRepo.Url);
            if (updatedphoto != null)
                return NoContent();

            return BadRequest("Could not set photo to main");
        }

        [HttpDelete("DeleteHRCandidatePhoto/{id}")]
        public async Task<IActionResult> DeleteHRCandidatePhoto(int id)
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

        // Agency Management

        [HttpGet("GetHRAgencyPhoto/{id}")]
        public async Task<IActionResult> GetHRAgencyPhoto(int id)
        {
            var photoFromRepo = await _repo.GetPhoto(id);

            var photo = _mapper.Map<AgencyPhotoToReturnDto>(photoFromRepo);

            return Ok(photo);
        }
        [HttpGet("GetHRAgentPhotos/{id}")]
        public async Task<ActionResult<IReadOnlyList<AgencyPhotoToReturnDto>>> GetHRAgentPhotos(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var agency = await _repoAgency.GetAgencyByIdAsync(id);

            var photoFromRepo = await _repoAgency.GetPhotos(agency.Id);

            var photo = _mapper.Map<IReadOnlyList<AgencyPhotoToReturnDto>>(photoFromRepo);

            return Ok(photo);
        }

        [HttpPost("AddHRAgencyPhotoForUser")]
        public async Task<IActionResult> AddHRAgencyPhotoForUser([FromForm] AgencyPhotoForCreation photoForCreationDto)
        {

            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
            var userFromRepo = await _repoAgency.GetAgencyByIdAsync(photoForCreationDto.AgencyId);

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
            var newPhoto = await _repoAgency.CreatePhotoAsync(photo);
            if (firstPhoto)
            {
                user.Avatar = newPhoto.Url;
                await _repoAgency.UpdateUserPhotoAsync(user);
            }

            if (newPhoto != null)
            {
                var photoToReturn = _mapper.Map<AgencyPhoto, AgencyPhotoToReturnDto>(newPhoto);
                return CreatedAtRoute("GetAgencyPhoto", new { id = photo.Id }, photoToReturn);
            }

            return BadRequest("Could not add the photo");
        }

        [HttpPut("setHRAgencyMain")]
        public async Task<IActionResult> setHRAgencyMain(HRAgencyUpdatePhotoDto setMain)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var userFromRepo = await _repoAgency.GetAgencyByIdAsync(setMain.AgencyId);
            if (!userFromRepo.AgencyPhotos.Any(p => p.Id == setMain.Id))
                return Unauthorized();

            var photoFromRepo = await _repoAgency.GetPhoto(setMain.Id);

            //if (photoFromRepo.IsMain)
            //    return BadRequest("This is already the main photo");

            var currentMainPhoto = await _repoAgency.GetMainPhotoForUserAsync(userFromRepo.Id);
            currentMainPhoto.IsMain = false;
            await _repoAgency.UpdatePhotoAsync(photoFromRepo);
            photoFromRepo.IsMain = true;
            var updatedphoto = await _repoAgency.UpdatePhotoAsync(photoFromRepo);

            if (updatedphoto != null)
            {
                user.Avatar = updatedphoto.Url;
                await _repoAgency.UpdateUserPhotoAsync(user);
                return NoContent();
            }


            return BadRequest("Could not set photo to main");
        }

        [HttpDelete("DeleteHRAgentPhoto/{id}")]
        public async Task<IActionResult> DeleteHRAgentPhoto(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();


            var photoFromRepo = await _repoAgency.GetPhoto(id);

            if (photoFromRepo.IsMain)
                return BadRequest("You cannot delete your main photo");

            if (photoFromRepo.PublicId != null)
            {
                var deleteParams = new DeletionParams(photoFromRepo.PublicId);

                var result = _cloudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                {
                    await _repoAgency.DeletePhotoAsync(photoFromRepo.Id);
                    return Ok();
                }
            }

            if (photoFromRepo.PublicId == null)
            {
                //_repo.Delete(photoFromRepo);
                await _repoAgency.DeletePhotoAsync(photoFromRepo.Id);
                return Ok();
            }

            return BadRequest("Failed to delete the photo");
        }

    }
}

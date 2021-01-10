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

    public class CandidateDocumentController : BaseApiController
    {
        private readonly ICandidateDocumentRepository _repo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public CandidateDocumentController(ICandidateDocumentRepository repo, UserManager<AppUser> userManager, IMapper mapper,
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
        [HttpGet("GetCandidateDocument/{id}")]
        public async Task<IActionResult> GetCandidateDocument(int id)
        {
            var photoFromRepo = await _repo.GetDocumentAsync(id);

            var photo = _mapper.Map<CandidateDocumentToReturnDto>(photoFromRepo);

            return Ok(photo);
        }
        [HttpGet("GetCandidateDocuments")]
        public async Task<ActionResult<IReadOnlyList<CandidateDocumentToReturnDto>>> GetCandidateDocuments()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
            var candidate = await _repo.GetCandidateByUserIdAsync(user.Email);
            var userDocuments = await _repo.GetDocuments(candidate.Id);

            var documentToReturn = _mapper.Map<IReadOnlyList<CandidateDocumentToReturnDto>>(userDocuments);
            return Ok(documentToReturn);
        }

        [HttpPost("AddUserDocument")]
        public async Task<IActionResult> AddUserDocument([FromForm]CandidateDocumentForCreation documentForCreationDto)
        {

            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
            var userFromRepo = await _repo.GetCandidateByUserIdAsync(user.Email);

            var file = documentForCreationDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Folder = "CareManager/"
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }
            var urlToReplace = uploadResult.Uri.AbsoluteUri.ToString();
            var replaceUrl = urlToReplace.Replace("http:", "https:");

            documentForCreationDto.Url = replaceUrl;
            documentForCreationDto.PublicId = uploadResult.PublicId;

            var document = _mapper.Map<CandidateDocument>(documentForCreationDto);
            document.Description = file.FileName;
            document.CandidateId = userFromRepo.Id;
            var newDocument = await _repo.CreateDocumentAsync(document);

            if (newDocument != null)
            {
                var documentToReturn = _mapper.Map<CandidateDocument, CandidateDocumentToReturnDto>(newDocument);
                return Ok(documentToReturn);
            }

            return BadRequest("Could not add the photo");
        }

      
        [HttpDelete("DeleteDocument/{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();


            var photoFromRepo = await _repo.GetDocumentAsync(id);

           

            if (photoFromRepo.PublicId != null)
            {
                var deleteParams = new DeletionParams(photoFromRepo.PublicId);

                var result = _cloudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                {
                    await _repo.DeleteDocumentAsync(photoFromRepo.Id);
                    return Ok();
                }
            }

            return BadRequest("Failed to delete the photo");
        }
    }
}
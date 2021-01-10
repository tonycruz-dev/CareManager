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
    public class HRManagerDocumentsController : BaseApiController
    {
        private readonly ICandidateDocumentRepository _repoCandidateDoc;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        private readonly IAgencyDocumentRepository _repo;

        public HRManagerDocumentsController(ICandidateDocumentRepository repo, 
            UserManager<AppUser> userManager, IMapper mapper,
            IAgencyDocumentRepository repoAgency,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _repoCandidateDoc = repo;
            _repo = repoAgency;
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
        public async Task<ActionResult<CandidateDocumentToReturnDto>> GetCandidateDocument(int id)
        {
            var documentFromRepo = await _repoCandidateDoc.GetDocumentAsync(id);

            var doc = _mapper.Map<CandidateDocumentToReturnDto>(documentFromRepo);

            return Ok(doc);
        }
        [HttpGet("GetCandidateDocuments/{id}")]
        public async Task<ActionResult<IReadOnlyList<CandidateDocumentToReturnDto>>> GetCandidateDocuments(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var userDocuments = await _repoCandidateDoc.GetDocuments(id);

            var documentToReturn = _mapper.Map<IReadOnlyList<CandidateDocumentToReturnDto>>(userDocuments);
            return Ok(documentToReturn);
        }

        [HttpPost("AddCandidateDocument")]
        public async Task<IActionResult> AddCandidateDocument([FromForm] CandidateDocumentForCreation documentForCreationDto)
        {

            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
            var userFromRepo = await _repoCandidateDoc.GetCandidateByIdAsync(documentForCreationDto.CandidateId);

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
            var newDocument = await _repoCandidateDoc.CreateDocumentAsync(document);

            if (newDocument != null)
            {
                var documentToReturn = _mapper.Map<CandidateDocument, CandidateDocumentToReturnDto>(newDocument);
                return Ok(documentToReturn);
            }

            return BadRequest("Could not add the photo");
        }


        [HttpDelete("DeleteCandidateDocument/{id}")]
        public async Task<IActionResult> DeleteCandidateDocument(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();


            var documentFromRepo = await _repoCandidateDoc.GetDocumentAsync(id);



            if (documentFromRepo.PublicId != null)
            {
                var deleteParams = new DeletionParams(documentFromRepo.PublicId);

                var result = _cloudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                {
                    await _repoCandidateDoc.DeleteDocumentAsync(documentFromRepo.Id);
                    return Ok();
                }
            }

            return BadRequest("Failed to delete the photo");
        }
        [HttpGet("GetAgentDocument/{id}")]
        public async Task<IActionResult> GetAgentDocument(int id)
        {
            var documentFromRepo = await _repo.GetDocumentAsync(id);

            var doc = _mapper.Map<AgencyDocumentToReturnDto>(documentFromRepo);

            return Ok(doc);
        }
        [HttpGet("GetAgencyDocuments/{id}")]
        public async Task<ActionResult<IReadOnlyList<AgencyDocumentToReturnDto>>> GetAgencyDocuments(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var agency = await _repo.GetAgentByEmailAsync(user.Email);
            var userDocuments = await _repo.GetDocuments(agency.Id);

            var documentToReturn = _mapper.Map<IReadOnlyList<AgencyDocumentToReturnDto>>(userDocuments);
            return Ok(documentToReturn);
        }
        [HttpPost("AddAgencyUserDocument")]
        public async Task<IActionResult> AddAgencyUserDocument([FromForm] AgencyDocumentForCreationDto documentForCreationDto)
        {

            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
            var userFromRepo = await _repo.GetAgentByEmailAsync(user.Email);

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

            var document = _mapper.Map<AgencyDocument>(documentForCreationDto);

            document.AgencyId = userFromRepo.Id;
            var newDocument = await _repo.CreateDocumentAsync(document);

            if (newDocument != null)
            {
                var documentToReturn = _mapper.Map<AgencyDocument, AgencyDocumentToReturnDto>(newDocument);
                return CreatedAtRoute("GetCandidateDocument", new { id = document.Id }, documentToReturn);
            }

            return BadRequest("Could not add the photo");
        }


        [HttpDelete("DeleteAgencyDocument/{id}")]
        public async Task<IActionResult> DeleteAgencyDocument(int id)
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

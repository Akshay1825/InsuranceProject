using InsuranceProject.DTOs;
using InsuranceProject.Models;
using InsuranceProject.Services;
using InsuranceProject.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly ICloudinaryService _cloudinaryService;

        public DocumentController(IDocumentService service,ICloudinaryService cloudinaryService)
        {
            _documentService = service;
            _cloudinaryService = cloudinaryService;

        }

        [HttpPut, Authorize(Roles = "ADMIN,EMPLOYEE,CUSTOMER")]
        public IActionResult Update([FromBody] Document document)
        {
            if (_documentService.UpdateCustomer(document))
            {
                return Ok(document);
            }
            return NotFound();
        }

        [HttpPost, Authorize(Roles = "ADMIN,EMPLOYEE,CUSTOMER")]
        public IActionResult Add(Document document)
        {
            var newId = _documentService.Add(document);
            return Ok(newId);
        }

        [HttpDelete, Authorize(Roles = "ADMIN,EMPLOYEE,CUSTOMER")]
        public IActionResult Delete(Guid id)
        {
            if (_documentService.Delete(id))
                return Ok(id);
            return BadRequest();
        }

        [HttpGet("DocTypes"), Authorize(Roles = "ADMIN,EMPLOYEE,CUSTOMER")]
        public IActionResult GetDocTypes()
        {
            var docTypes = Enum.GetValues(typeof(DocumentType))
                               .Cast<DocumentType>()
                               .Select(e => new { Name = e.ToString(), Value = (int)e })
                               .ToList();

            return Ok(docTypes);
        }

        [HttpPost("upload"), Authorize(Roles = "ADMIN,EMPLOYEE,CUSTOMER")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
        {
            var client = new HttpClient();
            var form = new MultipartFormDataContent();
            form.Add(new StreamContent(file.OpenReadStream()), "file", file.FileName);
            form.Add(new StringContent("sample_preset"), "upload_preset");

            var response = await client.PostAsync("https://api.cloudinary.com/v1_1/dxq7e2s2v/image/upload", form);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        [HttpGet("document-types"), Authorize(Roles = "ADMIN,EMPLOYEE,CUSTOMER")]
        public IActionResult GetDocumentTypes()
        {
            var documentTypes = Enum.GetNames(typeof(DocumentType));
            return Ok(documentTypes);
        }
    }
}


using InsuranceProject.DTOs;
using InsuranceProject.Models;
using InsuranceProject.Services;
using InsuranceProject.Types;
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

        [HttpPost]
        public IActionResult Add(Document document)
        {
            var newId = _documentService.Add(document);
            return Ok(newId);
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            if (_documentService.Delete(id))
                return Ok(id);
            return BadRequest();
        }

        [HttpGet("DocTypes")]
        public IActionResult GetDocTypes()
        {
            var docTypes = Enum.GetValues(typeof(DocumentType))
                               .Cast<DocumentType>()
                               .Select(e => new { Name = e.ToString(), Value = (int)e })
                               .ToList();

            return Ok(docTypes);
        }

        [HttpPost("upload")]
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


        //[HttpGet("Cloudinary")]
        //public IActionResult DownloadFile([FromQuery] Guid documentId)
        //{
        //    try
        //    {
        //        var fileUrl = _documentService.GetFileUrlById(documentId);

        //        if (string.IsNullOrEmpty(fileUrl))
        //        {
        //            return NotFound("File not found.");
        //        }

        //        return Ok(new { FileUrl = fileUrl }); // Return the file URL to be used on the client side
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}
    }
}


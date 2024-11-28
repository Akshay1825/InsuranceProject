using InsuranceProject.DTOs;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var documentDTOs = _documentService.GetAll();
            return Ok(documentDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var existingDocumentDTO = _documentService.Get(id);
            return Ok(existingDocumentDTO);
        }

        [HttpPost]
        public IActionResult Add(DocumentDto documentDto)
        {
            var newDocumentId = _documentService.Add(documentDto);
            return Ok(newDocumentId);
        }

        [HttpPut]
        public IActionResult Update(DocumentDto documentDto)
        {
            var UpdatedDocumentDTO = _documentService.Update(documentDto);
            if (UpdatedDocumentDTO != null)
                return Ok(UpdatedDocumentDTO);
            return NotFound("Document Not Found");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(DocumentDto documentDto)
        {
            if (_documentService.Delete(documentDto))
                return Ok("Document Deleted Successfully");
            return NotFound("Document Not Found");
        }
    }
}

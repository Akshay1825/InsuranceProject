using InsuranceProject.DTOs;

namespace InsuranceProject.Services
{
    public interface IDocumentService
    {
        public List<DocumentDto> GetAll();
        public DocumentDto Get(Guid id);
        public Guid Add(DocumentDto documentDto);
        public DocumentDto Update(DocumentDto documentDto);
        public bool Delete(DocumentDto documentDto);
    }
}

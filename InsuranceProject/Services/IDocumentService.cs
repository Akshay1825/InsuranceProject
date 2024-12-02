using InsuranceProject.DTOs;
using InsuranceProject.Models;

namespace InsuranceProject.Services
{
    public interface IDocumentService
    {
        public Guid Add(Document document);
        public bool Delete(Guid id);
    }
}

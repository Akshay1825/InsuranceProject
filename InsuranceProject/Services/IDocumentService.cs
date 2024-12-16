using InsuranceProject.DTOs;
using InsuranceProject.Helper;
using InsuranceProject.Models;

namespace InsuranceProject.Services
{
    public interface IDocumentService
    {
        public Guid Add(Document document);
        public bool Delete(Guid id);
        public PageList<Document> GetByCustomerId(PageParameter pageParameter, Guid customerID);

        public string GetFileUrlById(Guid documentId);

        public bool UpdateCustomer(Document document);
    }
}

using AutoMapper;
using CloudinaryDotNet;
using InsuranceProject.Data;
using InsuranceProject.DTOs;
using InsuranceProject.Exceptions;
using InsuranceProject.Helper;
using InsuranceProject.Models;
using InsuranceProject.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsuranceProject.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepository<Document> _repository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly ICloudinaryService _cloudinaryService;

        public DocumentService(IRepository<Document> repository, IRepository<Customer> customerRepository,ICloudinaryService cloudinaryService)
        {
            _repository = repository;
            _customerRepository = customerRepository;
            _cloudinaryService = cloudinaryService;
        }
        public Guid Add(Document document)
        {
            _repository.Add(document);
            return document.Id;
        }

        public bool Delete(Guid id)
        {
            var document = _repository.Get(id);
            if (document != null)
            {
                _repository.Delete(document);
                return true;
            }
            return false;
        }

        public PageList<Document> GetByCustomerId(PageParameter pageParameter,Guid customerID)
        {
            var customer = _customerRepository.GetAll().AsNoTracking().Include(x => x.Documents).FirstOrDefault(x => x.CustomerId == customerID); ;
            var documents = customer.Documents;
            if (documents!=null)
            {
                return PageList<Document>.ToPagedList(documents, pageParameter.PageNumber, pageParameter.PageSize);

            }
            throw new DocumentNotFoundException("No data found");
        }

        public string GetFileUrlById(Guid documentId)
        {
            var document = _repository.Get(documentId);
            if (document == null)
            {
                throw new Exception("Document not found");
            }

            return _cloudinaryService.GetFileUrl(document.Id); // Assume you store PublicId in the database
        }
    }
}

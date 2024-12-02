using AutoMapper;
using InsuranceProject.DTOs;
using InsuranceProject.Exceptions;
using InsuranceProject.Models;
using InsuranceProject.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsuranceProject.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepository<Document> _repository;

        public DocumentService(IRepository<Document> repository)
        {
            _repository = repository;
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
    }
}

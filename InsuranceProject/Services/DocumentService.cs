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
        private readonly IMapper _mapper;

        public DocumentService(IRepository<Document> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public Guid Add(DocumentDto documentDto)
        {
            var document = _mapper.Map<Document>(documentDto);
            _repository.Add(document);
            return document.DocumentId;
        }

        public bool Delete(DocumentDto documentDto)
        {
            var document = _mapper.Map<Document>(documentDto);
            var existingDocument = _repository.GetById(document.DocumentId);
            if (existingDocument != null)
            {
                _repository.Delete(existingDocument);
                return true;
            }
            return false;
        }

        public DocumentDto Get(Guid id)
        {
            var document = _repository.GetById(id);
            if (document == null)
            {
                throw new DocumentNotFoundException("Document Not Found");
            }
            var documentDto = _mapper.Map<DocumentDto>(document);
            return documentDto;
        }

        public List<DocumentDto> GetAll()
        {
            var documents = _repository.GetAll().ToList();
            List<DocumentDto> result = _mapper.Map<List<DocumentDto>>(documents);
            return result;
        }

        public DocumentDto Update(DocumentDto documentDto)
        {
            var existingDocument = _mapper.Map<Document>(documentDto);
            var updatedDocument = _repository.GetAll().AsNoTracking().FirstOrDefault(x => x.DocumentId == existingDocument.DocumentId);
            if (updatedDocument != null)
            {
                _repository.Update(updatedDocument);
            }
            var updatedDocumentDto = _mapper.Map<DocumentDto>(updatedDocument);
            return updatedDocumentDto;
        }
    }
}

using AutoMapper;
using Claims_AssignmentApplication.Exceptions;
using Claims_AssignmentApplication.Interfaces;
using Claims_AssignmentApplication.Models;
using Claims_AssignmentApplication.Models.DTOs;
using ReportClaimApplication.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Claims_AssignmentApplication.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepository<int, Document> _documentRepo;
        private readonly IMapper _mapper;

        public DocumentService(IRepository<int, Document> documentRepository, IMapper mapper)
        {
            _documentRepo = documentRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateDocument(DocumentUploadDTO document)
        {
            Document newDocument = _mapper.Map<Document>(document);
            var addedDocument = await _documentRepo.AddAsync(newDocument);
            return addedDocument.DocumentId;
        }

        public async Task<IEnumerable<Document>> GetAllDocuments()
        {
            try
            {
                var documents = await _documentRepo.GetAllAsync();
                return documents;
            }
            catch (CollectionEmptyException)
            {
                throw new CollectionEmptyException("Documents");
            }
        }

        public async Task<Document> GetDocument(int id)
        {
            try
            {
                var document = await _documentRepo.GetByIdAsync(id);
                if (document != null)
                {
                    return document;
                }
                throw new NotFoundException("Document");
            }
            catch (Exception)
            {
                throw new NotFoundException("Document");
            }
        }

     
    }
}

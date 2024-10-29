using Microsoft.EntityFrameworkCore;
using ReportClaimApplication.Context;
using ReportClaimApplication.Models;
using ReportClaimApplication.Exceptions;
using ReportClaimApplication.Interfaces;

namespace ReportClaimApplication.Repositories
{
    public class DocumentRepository : IRepository<Document>
    {
        private readonly ClaimDbContext _context;

        // Constructor with context
        public DocumentRepository(ClaimDbContext context)
        {
            _context = context;
        }

        // Add a new document
        public async Task<Document> AddAsync(Document entity)
        {
            try
            {
                await _context.Documents.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw new CouldNotAddException("Document");
            }
        }

        // Delete a document by ID
        public async Task<Document> DeleteAsync(int id)
        {
            var document = await GetByIdAsync(id);
            if (document != null)
            {
                _context.Documents.Remove(document);
                await _context.SaveChangesAsync();
                return document;
            }
            throw new NotFoundException("Document for deletion");
        }

        // Get a document by ID
        public async Task<Document> GetByIdAsync(int id)
        {
            return await _context.Documents.FirstOrDefaultAsync(d => d.DocumentId == id);
        }

        // Get all documents
        public async Task<IEnumerable<Document>> GetAllAsync()
        {
            var documents = await _context.Documents.ToListAsync();
            if (documents.Count == 0)
            {
                throw new CollectionEmptyException("Documents");
            }
            return documents;
        }

        // Update an existing document
        public async Task UpdateAsync(Document entity)
        {
            var existingDocument = await GetByIdAsync(entity.DocumentId);
            if (existingDocument == null)
            {
                throw new NotFoundException("Document not found for update");
            }

            try
            {
                _context.Documents.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new CouldNotUpdateException("Document");
            }
        }
    }
}

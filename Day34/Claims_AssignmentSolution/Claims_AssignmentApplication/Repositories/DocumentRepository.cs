using Claims_AssignmentApplication.Context;
using Claims_AssignmentApplication.Interfaces;
using Claims_AssignmentApplication.Models;
using Microsoft.EntityFrameworkCore;
using ReportClaimApplication.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Claims_AssignmentApplication.Repositories
{
    public class DocumentRepository : IRepository<int, Document>
    {
        private readonly ClaimContext _context;

        public DocumentRepository(ClaimContext context)
        {
            _context = context;
        }

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

        public async Task<bool> DeleteAsync(int key)
        {
            var document = await GetByIdAsync(key);
            if (document != null)
            {
                _context.Documents.Remove(document);
                await _context.SaveChangesAsync(); // Ensure changes are saved
                return true; // Return true if deletion is successful
            }
            throw new NotFoundException("Document for deletion");
        }

        public async Task<Document> GetByIdAsync(int key)
        {
            var document = await _context.Documents.FindAsync(key); // Use FindAsync for direct lookup
            if (document == null)
            {
                throw new NotFoundException("Document");
            }
            return document;
        }

        public async Task<IEnumerable<Document>> GetAllAsync()
        {
            var documents = await _context.Documents.ToListAsync();
            if (!documents.Any())
            {
                throw new CollectionEmptyException("Documents");
            }
            return documents;
        }

        public async Task UpdateAsync(Document entity)
        {
            try
            {
                _context.Documents.Update(entity);
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            catch (Exception)
            {
                throw new CouldNotUpdateException("Document");
            }
        }
    }
}

using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintTicketAPI.Tests.Repositories
{
    public class ComplaintFileRepositoryTests
    {
        private DbContextOptions<ComplaintTicketContext> _options;
        private ComplaintTicketContext _context;
        private ComplaintFileRepository _repository;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<ComplaintTicketContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new ComplaintTicketContext(_options);
            _repository = new ComplaintFileRepository(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Add_ShouldAddComplaintFileSuccessfully()
        {
            // Arrange
            var complaintFile = new ComplaintFile
            {
                Id = 1,
                FilePath = "/uploads/test_file.pdf",
                ComplaintId = 1
            };

            // Act
            var result = await _repository.Add(complaintFile);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("test_file.pdf", result.FilePath);
        }

        [Test]
        public async Task Get_ShouldReturnComplaintFile_WhenExists()
        {
            // Arrange
            var complaintFile = new ComplaintFile
            {
                Id = 2,
                FilePath = "/uploads/sample_file.pdf",
                ComplaintId = 1
            };
            await _repository.Add(complaintFile);

            // Act
            var result = await _repository.Get(2);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("sample_file.pdf", result.FilePath);
        }

        [Test]
        public async Task Get_ShouldReturnNull_WhenComplaintFileDoesNotExist()
        {
            // Act
            var result = await _repository.Get(99);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAll_ShouldReturnAllComplaintFiles()
        {
            // Arrange
            var file1 = new ComplaintFile { Id = 3, FilePath = "/uploads/file1.pdf", ComplaintId = 1 };
            var file2 = new ComplaintFile { Id = 4, FilePath = "/uploads/file2.pdf", ComplaintId = 1 };
            await _repository.Add(file1);
            await _repository.Add(file2);

            // Act
            var result = await _repository.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task Update_ShouldUpdateComplaintFileSuccessfully()
        {
            // Arrange
            var complaintFile = new ComplaintFile
            {
                Id = 5,
                FilePath = "/uploads/initial_file.pdf",
                ComplaintId = 1
            };
            await _repository.Add(complaintFile);

            var updatedFile = new ComplaintFile
            {
                Id = 5,
                FilePath = "/uploads/updated_file.pdf",
                ComplaintId = 1
            };

            // Act
            var result = await _repository.Update(updatedFile, 5);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("updated_file.pdf", result.FilePath);
        }

        [Test]
        public async Task Update_ShouldReturnNull_WhenComplaintFileDoesNotExist()
        {
            // Arrange
            var nonExistentFile = new ComplaintFile
            {
                Id = 999,
                FilePath = "/uploads/non_existent_file.pdf",
                ComplaintId = 1
            };

            // Act
            var result = await _repository.Update(nonExistentFile, 999);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task Delete_ShouldDeleteComplaintFileSuccessfully()
        {
            // Arrange
            var complaintFile = new ComplaintFile
            {
                Id = 6,
                FilePath = "/uploads/delete_test_file.pdf",
                ComplaintId = 1
            };
            await _repository.Add(complaintFile);

            // Act
            var result = await _repository.Delete(6);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("delete_test_file.pdf", result.FilePath);
        }

        [Test]
        public async Task Delete_ShouldReturnNull_WhenComplaintFileDoesNotExist()
        {
            // Act
            var result = await _repository.Delete(999);

            // Assert
            Assert.IsNull(result);
        }
    }
}

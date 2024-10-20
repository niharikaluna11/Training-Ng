using AutoMapper;
using EFFirstAPI.Context;
using EFFirstAPI.Exceptions;
using EFFirstAPI.Interfaces;
using EFFirstAPI.Models.DTO;
using EFFirstAPI.Models;
using EFFirstAPI.Repositories;
using EFFirstAPI.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFFirstTest
{
    internal class ProductServiceTesting
    {
        DbContextOptions options;
        ProductRepository repository;
        ShoppingContext shoppingContext;
        Mock<IMapper> mapper;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
                .UseInMemoryDatabase("Test")
                .Options;

            shoppingContext = new ShoppingContext(options);
            repository = new ProductRepository(shoppingContext);
            mapper = new Mock<IMapper>();
        }

        [Test]

        public async Task CreateProductTesting()
        {
            //Arrange

            var product = new ProductDTO
            {
                Name = "Test Product",
                Price = 10.0f,
                Quantity = 100
            };
            var productEntity = new Product
            {
                Name = "Test Product",
                Price = 10.0f,
                Quantity = 100
            };

            mapper.Setup(m => m.Map<Product>(product)).Returns(productEntity);  //dummying the method to return the result for testing
            IProductService productService = new ProductService(repository, mapper.Object);

            //Act

            var result = await productService.CreateProduct(product);

            //Assert
            Assert.AreEqual(1, result);
        }

        [Test]

        public async Task GetAllProuctTesting()
        {
            var product = new ProductDTO
            {
                Name = "Test Product",
                Price = 10.0f,
                Quantity = 100
            };
            var productEntity = new Product
            {
                Name = "Test Product",
                Price = 10.0f,
                Quantity = 100
            };

            mapper.Setup(m => m.Map<Product>(product)).Returns(productEntity);
            IProductService productService = new ProductService(repository, mapper.Object);

            //Act
            await productService.CreateProduct(product);
            var result = await productService.GetAllProduct();

            //Assert
            Assert.NotNull(result);
        }

        [Test]
        [TestCase(1)]
        public async Task GetProductTesting(int id)
        {
            var product = new ProductDTO
            {
                Name = "Test Product",
                Price = 10.0f,
                Quantity = 100
            };
            var productEntity = new Product
            {
                Name = "Test Product",
                Price = 10.0f,
                Quantity = 100
            };

            mapper.Setup(m => m.Map<Product>(product)).Returns(productEntity);
            IProductService productService = new ProductService(repository, mapper.Object);

            //Act
            await productService.CreateProduct(product);
            var result = await productService.GetProduct(id);
            //Assert
            Assert.AreEqual(result.Id, id);
        }

        [Test]
        [TestCase(80, 1)]
        [TestCase(90, 1)]
        public async Task UpdatePriceTesting(float price, int id)
        {
            var product = new ProductDTO
            {
                Name = "Test Product",
                Price = 10.0f,
                Quantity = 100
            };
            var productEntity = new Product
            {
                Name = "Test Product",
                Price = 10.0f,
                Quantity = 100
            };

            mapper.Setup(m => m.Map<Product>(product)).Returns(productEntity);
            IProductService productService = new ProductService(repository, mapper.Object);

            //Act
            await productService.CreateProduct(product);
            var result = await productService.UpdateProductPrice(price, id);
            //Assert
            Assert.AreEqual(result.Price, price);
        }

        //Exception Testing


        [Test]
        [TestCase("Not Found in Product", 1)]

        public async Task NotFoundExceptionTesting(string msg, int id)
        {
            //Arrange
            IProductService productService = new ProductService(repository, mapper.Object);

            //Act
            var exception = Assert.ThrowsAsync<NotFoundException>(async () => await productService.GetProduct(id));

            //Assert
            Assert.That(exception.Message, Is.EqualTo(msg));

        }

        [Test]
        [TestCase("Product Price Update Fail", 1, 30)]

        public async Task NotUpdateExceptionTesting(string msg, int id, float price)
        {

            //Arrange
            IProductService productService = new ProductService(repository, mapper.Object);

            //Act

            //var exception = Assert.ThrowsAsync<NotUpdateException>(async () => await productService.UpdateProductPrice(price,id))
            Assert.ThrowsAsync<NotUpdateException>(async () => await productService.UpdateProductPrice(price, id));

            //Assert
            //Assert.That(exception.Message, Is.EqualTo(msg));

        }

        [Test]
        [TestCase("Product Price Update Fail", 999, 30)]
        public async Task UpdatePriceNotFoundExceptionTesting(string msg, int id, float price)
        {
            // Arrange
            repository = new ProductRepository(shoppingContext); // Reset repository for clean state
            IProductService productService = new ProductService(repository, mapper.Object);

            // Act & Assert
            var exception = Assert.ThrowsAsync<NotUpdateException>(async () => await productService.UpdateProductPrice(price, id));
            Assert.That(exception.Message, Is.EqualTo(msg));
        }

        [Test]
        public async Task GetAllProduct_ThrowsCollectionEmptyException()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<int, Product>>();
            mockRepo.Setup(repo => repo.GetAll()).ThrowsAsync(new CollectionEmptyException("Product"));
            IProductService productService = new ProductService(mockRepo.Object, mapper.Object);

            // Act & Assert
            var exception = Assert.ThrowsAsync<CollectionEmptyException>(async () => await productService.GetAllProduct());
            Assert.That(exception.Message, Is.EqualTo("Empty Collection - Product"));
        }

        [Test]
        public async Task GetAllProduct_ReturnsProducts()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<int, Product>>();
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10.0f, Quantity = 5 },
                new Product { Id = 2, Name = "Product 2", Price = 20.0f, Quantity = 10 }
            };
            mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(products);
            IProductService productService = new ProductService(mockRepo.Object, mapper.Object);

            // Act
            var result = await productService.GetAllProduct();

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count());
        }

    }
}

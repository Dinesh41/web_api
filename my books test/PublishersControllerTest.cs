using Microsoft.EntityFrameworkCore;
using my_books.Data.models;
using my_books.Data.services;
using my_books.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using my_books.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace my_books_test
{
    public class PublishersControllerTest
    {
        //DBContextOptions
        public static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("MyBookDBTest")
            .Options;

        AppDbContext dbContext;
        PublishersService publishersService;
        PublishersController publishersController;

        [OneTimeSetUp]
        public void Setup()
        {
            //Set DBContext
            dbContext = new AppDbContext(dbContextOptions);
            dbContext.Database.EnsureCreated();
            //Seed Database
            SeedDatabase();
            //Set services
            publishersService = new PublishersService(dbContext);
            publishersController = new PublishersController(publishersService);
            publishersController.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        [Test]
        public void GetAllPublisher_WithNoSortBy_WithNoSearch_WithNoPageIndex_WithNoPageSize()
        {
            IActionResult response = publishersController.GetAllPublisher(null, null, null, null);
            
            Assert.That(response, Is.TypeOf<OkObjectResult>());

            var paginationvalue = publishersController.Response.Headers["X-Pagination"];

            Assert.That(paginationvalue,Is.Not.Empty);

            var metadata = new
            {
                TotalPages = 2,
                PageIndex = 1,
                HasNextPage = true,
                HasPreviousPage = false
            };

            var expectedPaginationValue = JsonConvert.SerializeObject(metadata);

            Assert.That(paginationvalue, Is.EqualTo(expectedPaginationValue));

            var publishers = (response as OkObjectResult).Value as List<Publisher>;
            Assert.That(publishers, Is.Not.Null);
            Assert.That(publishers.Count, Is.EqualTo(5));
        }

        private void SeedDatabase()
        {
            dbContext.Publishers.AddRange(
                new Publisher()
                {
                    Id = 1,
                    Name = "Publisher 1"
                },
                 new Publisher()
                 {
                     Id = 2,
                     Name = "Publisher 2"
                 },
                 new Publisher()
                 {
                     Id = 3,
                     Name = "Publisher 3"
                 },
                 new Publisher()
                 {
                     Id = 4,
                     Name = "Publisher 4"
                 },
                 new Publisher()
                 {
                     Id = 5,
                     Name = "Publisher 5"
                 },
                 new Publisher()
                 {
                     Id = 6,
                     Name = "Publisher 6"
                 });
            dbContext.SaveChanges();
        }

        //Destroy DBContext
        [OneTimeTearDown]
        public void Destroy()
        {
            dbContext.Database.EnsureDeleted();
        }
    }
}

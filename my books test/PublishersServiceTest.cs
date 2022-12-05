using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using my_books.Data;
using my_books.Data.models;
using my_books.Data.services;

namespace my_books_test
{
    public class PublishersServiceTest
    {
        //DBContextOptions
        public static DbContextOptions<AppDbContext> dbContextOptions= new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("MyBookDBTest")
            .Options;

        AppDbContext dbContext;
        PublishersService publishersService;

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
        }

        [Test]
        public void GetAllPublisher_WithNoSortBy_WithNoSearch_WithNoPageIndex_WithNoPageSize()
        {
            var publisher=publishersService.GetAllPublisher(null,null,null,null);
            Assert.That(publisher.Count, Is.EqualTo(5));
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
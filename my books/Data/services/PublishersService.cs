using my_books.Data.models;
using my_books.Data.ViewModels;

namespace my_books.Data.services
{
    public class PublishersService
    {
        private AppDbContext _appDbContext;
        public PublishersService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddPublisher(PublisherVM publisher)
        {
            _appDbContext.Add(new Publisher() { Name = publisher.Name });
            _appDbContext.SaveChanges();
        }
    }
}

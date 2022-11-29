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

        public Publisher? GetPublisherById(int id) => _appDbContext.Publishers.Where(x => x.Id == id).FirstOrDefault();

        public List<Publisher> GetAllPublisher(string? sortBy,string? searchName)
        {
            var allPublishers= _appDbContext.Publishers.OrderBy(n=>n.Name).ToList();
            if(sortBy == "Name_Dec")
            {
                allPublishers=allPublishers.OrderByDescending(n=>n.Name).ToList();
            }
            if (searchName != null)
            {
                allPublishers=allPublishers.Where(n=>n.Name!.Contains(searchName,StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return allPublishers;
        }
        
    }
}

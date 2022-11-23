using my_books.Data.models;
using my_books.Data.ViewModels;

namespace my_books.Data.services
{
    public class AuthorsService
    {
        private AppDbContext _appDbContext;
        public AuthorsService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddAuthor(AuthorVM author)
        {
            _appDbContext.Add(new Author() { Name = author.Name });
            _appDbContext.SaveChanges();
        }
    }
}

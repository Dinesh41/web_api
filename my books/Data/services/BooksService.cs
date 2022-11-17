using my_books.Data.models;
using my_books.Data.ViewModels;

namespace my_books.Data.services
{
    public class BooksService
    {
        private AppDbContext _appDbContext;
        public BooksService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddBook(BookVM book)
        {
            _appDbContext.Add(new Book() { Title=book.Title});
            _appDbContext.SaveChanges();
        }
    }
}

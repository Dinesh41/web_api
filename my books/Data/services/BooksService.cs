using Microsoft.AspNetCore.Mvc;
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

        public List<Book> GetAllBooks()
        {
            return _appDbContext.Books.ToList();
        }

        public Book? GetBookById(int id) => _appDbContext.Books.FirstOrDefault(x => x.Id == id);

        public Book? UpdateBookById(int id, BookVM bookVM)
        {
            var book = _appDbContext.Books.FirstOrDefault(x => x.Id == id);
            if (book != null)
            {
                book.Title = bookVM.Title;
                _appDbContext.SaveChanges();
            }
            return book;
        }

        public void DeleteBookByID(int id)
        {
            var book = _appDbContext.Books.FirstOrDefault(x => x.Id == id);
            if (book != null)
            {
                _appDbContext.Remove(book);
                _appDbContext.SaveChanges();
            }
        }
    }
}

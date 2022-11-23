namespace my_books.Data.models
{
    public class Author
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public List<Book_Author>? Book_Authors { get; set; }
    }
}

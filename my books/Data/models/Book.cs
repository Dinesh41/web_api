namespace my_books.Data.models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
        public List<Book_Author>? Book_Authors { get; set; }

    }
}

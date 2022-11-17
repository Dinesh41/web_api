using my_books.Data.models;

namespace my_books.Data
{
    public class AppDbSeeder
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var scope=applicationBuilder.ApplicationServices.CreateScope())
            {
                var context=scope.ServiceProvider.GetService<AppDbContext>();
                if (context!=null && !context.Books.Any())
                {
                    context.Books.AddRange(new Book(){ Title = "Book1" },
                    new Book() { Title = "Book2" }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}

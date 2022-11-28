using Microsoft.EntityFrameworkCore;
using my_books.Data;
using my_books.Data.services;
using my_books.Exceptions;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? dbConnectionString = builder.Configuration.GetSection("ConnectionStrings")["DefaultDBConnection"];
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(dbConnectionString));

builder.Services.AddTransient<BooksService>();
builder.Services.AddTransient<AuthorsService>();
builder.Services.AddTransient<PublishersService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.ConfigureBuiltInExceptionHandler();
app.ConfigureCustomExceptionMiddleware();   

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

AppDbSeeder.Seed(app);

app.Run();



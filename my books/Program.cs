using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using my_books.Data;
using my_books.Data.services;
using my_books.Exceptions;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/log.txt")
    .CreateLogger();
Log.Information("The global logger has been configured");
builder.Host.UseSerilog();


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1,0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    //config.ApiVersionReader = new HeaderApiVersionReader("custom-api-version");
    config.ApiVersionReader = new MediaTypeApiVersionReader();

});

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
    app.UseDeveloperExceptionPage();
}

//app.ConfigureBuiltInExceptionHandler();
//app.ConfigureCustomExceptionMiddleware();   

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

AppDbSeeder.Seed(app);

app.Run();



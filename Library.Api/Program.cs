using Library.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var connString = "Data Source=Library.db";
builder.Services.AddSqlite<LibraryContext>(connString);

var app = builder.Build();



app.Run();

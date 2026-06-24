using Library.Api.Data;
using Library.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();
builder.AddLibraryDb();

var app = builder.Build();


app.MigrateDb();

app.MapAuthorsEndpoints();

app.Run();

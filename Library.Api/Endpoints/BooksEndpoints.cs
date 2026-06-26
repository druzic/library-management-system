using Library.Api.Data;
using Library.Api.Dtos.Books;
using Library.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Endpoints;

public static class BooksEndpoints
{
    public static void MapBooksEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/books");

        //GET /books
        group.MapGet(
            "/",
            async (LibraryContext dbContext) =>
            {
                var books = await dbContext
                    .Books.AsNoTracking()
                    .Select(b => new BookDetailsDto(
                        b.Id,
                        b.Title,
                        b.ISBN,
                        b.Description,
                        b.Publisher,
                        b.PublishedYear,
                        b.PageCount,
                        b.Language,
                        b.AvailableCopies,
                        b.AuthorId,
                        b.Author.FirstName + " " + b.Author.LastName,
                        b.GenreId,
                        b.Genre.Name
                    ))
                    .ToListAsync();

                return Results.Ok(books);
            }
        );

        //GET /books/1
        group.MapGet(
            "/{id:int}",
            async (int id, LibraryContext dbContext) =>
            {
                var book = await dbContext
                    .Books.AsNoTracking()
                    .Where(b => b.Id == id)
                    .Select(b => new BookDetailsDto(
                        b.Id,
                        b.Title,
                        b.ISBN,
                        b.Description,
                        b.Publisher,
                        b.PublishedYear,
                        b.PageCount,
                        b.Language,
                        b.AvailableCopies,
                        b.AuthorId,
                        b.Author.FirstName + " " + b.Author.LastName,
                        b.GenreId,
                        b.Genre.Name
                    ))
                    .FirstOrDefaultAsync();

                return book is null ? Results.NotFound() : Results.Ok(book);
            }
        );

        //POST /books
        group.MapPost(
            "/",
            async (CreateBookDto newBook, LibraryContext dbContext) =>
            {
                var authorExists = await dbContext.Authors.AnyAsync(a => a.Id == newBook.AuthorId);

                if (!authorExists)
                    return Results.BadRequest("Author does not exist");

                var genreExists = await dbContext.Genres.AnyAsync(g => g.Id == newBook.GenreId);

                if (!genreExists)
                    return Results.BadRequest("Genre does not exist");

                var book = new Book
                {
                    Title = newBook.Title,
                    ISBN = newBook.ISBN,
                    Description = newBook.Description,
                    Publisher = newBook.Publisher,
                    PublishedYear = newBook.PublishedYear,
                    PageCount = newBook.PageCount,
                    Language = newBook.Language,
                    AvailableCopies = newBook.AvailableCopies,
                    AuthorId = newBook.AuthorId,
                    GenreId = newBook.GenreId,
                };
                dbContext.Books.Add(book);
                await dbContext.SaveChangesAsync();

                var result = await dbContext
                    .Books.Where(b => b.Id == book.Id)
                    .Select(b => new BookDetailsDto(
                        b.Id,
                        b.Title,
                        b.ISBN,
                        b.Description,
                        b.Publisher,
                        b.PublishedYear,
                        b.PageCount,
                        b.Language,
                        b.AvailableCopies,
                        b.AuthorId,
                        b.Author.FirstName + " " + b.Author.LastName,
                        b.GenreId,
                        b.Genre.Name
                    ))
                    .FirstAsync();

                return Results.Created($"/books/{book.Id}", result);
            }
        );

        //PUT /books/1
        group.MapPut(
            "/{id:int}",
            async (int id, UpdateBookDto UpdateBook, LibraryContext dbContext) =>
            {
                var existingBook = await dbContext.Books.FindAsync(id);

                if (existingBook is null)
                    return Results.NotFound();

                var authorExists = await dbContext.Authors.AnyAsync(a =>
                    a.Id == UpdateBook.AuthorId
                );

                if (!authorExists)
                    return Results.BadRequest("Author does not exist");

                var genreExists = await dbContext.Genres.AnyAsync(g => g.Id == UpdateBook.GenreId);

                if (!genreExists)
                    return Results.BadRequest("Genre does not exist");

                existingBook.Title = UpdateBook.Title;
                existingBook.ISBN = UpdateBook.ISBN;
                existingBook.Description = UpdateBook.Description;
                existingBook.Publisher = UpdateBook.Publisher;
                existingBook.PublishedYear = UpdateBook.PublishedYear;
                existingBook.PageCount = UpdateBook.PageCount;
                existingBook.Language = UpdateBook.Language;
                existingBook.AvailableCopies = UpdateBook.AvailableCopies;
                existingBook.AuthorId = UpdateBook.AuthorId;
                existingBook.GenreId = UpdateBook.GenreId;

                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            }
        );

        //DELETE /books/1
        group.MapDelete(
            "/{id:int}",
            async (int id, LibraryContext dbContext) =>
            {
                var existingBook = await dbContext.Books.FindAsync(id);

                if (existingBook is null)
                    return Results.NotFound();

                dbContext.Books.Remove(existingBook);
                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            }
        );
    }
}

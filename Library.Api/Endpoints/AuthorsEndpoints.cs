using Library.Api.Data;
using Library.Api.Dtos.Authors;
using Library.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Endpoints;

public static class AuthorsEndpoints
{
    public static void MapAuthorsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/authors");

        //GET /authors
        group.MapGet(
            "/",
            async (LibraryContext dbContext) => await dbContext.Authors.AsNoTracking().ToListAsync()
        );

        //GET /authors/1
        group
            .MapGet(
                "/{id:int}",
                async (int id, LibraryContext dbContext) =>
                {
                    var author = await dbContext
                        .Authors.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == id);

                    if (author is null)
                        return Results.NotFound();

                    return Results.Ok(author);
                }
            )
            .WithName("GetAuthor");
        ;

        //POST /authors
        group.MapPost(
            "/",
            async (CreateAuthorDto newAuthor, LibraryContext dbContext) =>
            {
                var author = new Author
                {
                    FirstName = newAuthor.FirstName,
                    LastName = newAuthor.LastName,
                    DateOfBirth = newAuthor.DateOfBirth,
                    Biography = newAuthor.Biography,
                };

                dbContext.Authors.Add(author);
                await dbContext.SaveChangesAsync();

                AuthorDto authorDto = new(
                    author.Id,
                    author.FirstName,
                    author.LastName,
                    author.DateOfBirth,
                    author.Biography
                );

                return Results.CreatedAtRoute("GetAuthor", new { id = author.Id }, authorDto);
            }
        );

        //PUT /authors/1
        group.MapPut(
            "/{id:int}",
            async (int id, UpdateAuthorDto updatedAuthor, LibraryContext dbContext) =>
            {
                var existingAuthor = await dbContext.Authors.FindAsync(id);

                if (existingAuthor is null)
                {
                    return Results.NotFound();
                }

                existingAuthor.FirstName = updatedAuthor.FirstName;
                existingAuthor.LastName = updatedAuthor.LastName;
                existingAuthor.DateOfBirth = updatedAuthor.DateOfBirth;
                existingAuthor.Biography = updatedAuthor.Biography;

                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            }
        );

        //DELETE /authors/1
        group.MapDelete(
            "/{id:int}",
            async (int id, LibraryContext dbContext) =>
            {
                var existingAuthor = await dbContext.Authors.FindAsync(id);
                if (existingAuthor is null)
                {
                    return Results.NotFound();
                }
                dbContext.Authors.Remove(existingAuthor);

                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            }
        );
    }
}

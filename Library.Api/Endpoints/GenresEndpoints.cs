using Library.Api.Data;
using Library.Api.Dtos.Genres;
using Library.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Endpoints;

public static class GenresEndpoints
{
    public static void MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/genres");

        //GET /genres
        group.MapGet(
            "/",
            async (LibraryContext dbContext) =>
            {
                var genres = await dbContext
                    .Genres.AsNoTracking()
                    .Select(genre => new GenreDto(genre.Id, genre.Name))
                    .ToListAsync();
                return Results.Ok(genres);
            }
        );

        //GET /genres/1
        group.MapGet(
            "/{id:int}",
            async (int id, LibraryContext dbContext) =>
            {
                var genre = await dbContext
                    .Genres.AsNoTracking()
                    .Where(genre => genre.Id == id)
                    .Select(genre => new GenreDto(genre.Id, genre.Name))
                    .FirstOrDefaultAsync();

                return genre is null ? Results.NotFound() : Results.Ok(genre);
            }
        );

        //POST /genres
        group.MapPost(
            "/",
            async (CreateGenreDto newGenre, LibraryContext dbContext) =>
            {
                Genre genre = new() { Name = newGenre.Name };
                dbContext.Genres.Add(genre);
                await dbContext.SaveChangesAsync();

                GenreDto genreDto = new(genre.Id, genre.Name);

                return Results.Created($"/genres/{genre.Id}", genreDto);
            }
        );
    }
}

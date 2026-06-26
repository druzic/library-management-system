namespace Library.Api.Dtos.Books;

public record BookDetailsDto(
    int Id,
    string Title,
    string ISBN,
    string? Description,
    string Publisher,
    int PublishedYear,
    int PageCount,
    string Language,
    int AvailableCopies,
    int AuthorId,
    string AuthorName,
    int GenreId,
    string GenreName
);

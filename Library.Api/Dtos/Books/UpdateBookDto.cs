namespace Library.Api.Dtos.Books;

public record UpdateBookDto(
    string Title,
    string ISBN,
    string? Description,
    string Publisher,
    int PublishedYear,
    int PageCount,
    string Language,
    int AvailableCopies,
    int AuthorId,
    int GenreId
);

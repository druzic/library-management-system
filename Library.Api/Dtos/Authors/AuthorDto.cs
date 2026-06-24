namespace Library.Api.Dtos.Authors;

public record AuthorDto(

    int Id,
    string FirstName,
    string LastName,
    DateOnly? DateOfBirth,
    string? Biography

);
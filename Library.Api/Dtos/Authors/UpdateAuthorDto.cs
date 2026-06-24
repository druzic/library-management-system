namespace Library.Api.Dtos.Authors;

public record UpdateAuthorDto(
    string FirstName,
    string LastName,
    DateOnly? DateOfBirth,
    string? Biography
);

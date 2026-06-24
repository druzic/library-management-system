namespace Library.Api.Dtos.Authors;

public record CreateAuthorDto(
    string FirstName,
    string LastName,
    DateOnly? DateOfBirth,
    string? Biography
);

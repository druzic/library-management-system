namespace Library.Api.Model
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string ISBN { get; set; } = "";

        public string? Description { get; set; }

        public string Publisher { get; set; } = "";

        public int PublishedYear { get; set; }

        public int PageCount { get; set; }

        public string Language { get; set; } = "";

        public int AvailableCopies { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; } = null!;

        public int GenreId { get; set; }

        public Genre Genre { get; set; } = null!;

        public List<Loan> Loans { get; set; } = [];
    }
}

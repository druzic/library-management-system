using Library.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Data
{
    public class LibraryContext(DbContextOptions<LibraryContext> options) 
        : DbContext(options)
    {
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Author> Author => Set<Author>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Loan> Loans => Set<Loan>();
    }
}

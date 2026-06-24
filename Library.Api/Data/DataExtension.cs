using Microsoft.EntityFrameworkCore;

namespace Library.Api.Data;

public static class DataExtension
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<LibraryContext>();
        dbContext.Database.Migrate();
    }

    public static void AddLibraryDb(this WebApplicationBuilder builder)
    {
        var connString = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddSqlite<LibraryContext>(connString);
    }
}

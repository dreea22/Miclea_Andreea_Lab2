using Microsoft.EntityFrameworkCore;

public class Miclea_Andreea_Lab2Context(DbContextOptions<Miclea_Andreea_Lab2Context> options) : DbContext(options)
{
    public DbSet<Miclea_Andreea_Lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<Miclea_Andreea_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    public DbSet<Miclea_Andreea_Lab2.Models.Author> Author { get; set; } = default!;
    public DbSet<Miclea_Andreea_Lab2.Models.Category> Category { get; set; } = default!;
    public DbSet<Miclea_Andreea_Lab2.Models.BookCategory> BookCategory { get; set; } = default!;
}
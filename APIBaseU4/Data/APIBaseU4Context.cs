using Microsoft.EntityFrameworkCore;

public class APIBaseU4Context(DbContextOptions<APIBaseU4Context> options) : DbContext(options)
{
    public DbSet<APIBaseU4.Models.Curso> Curso { get; set; } = default!;
    public DbSet<APIBaseU4.Models.Nivel> Nivel { get; set; } = default!;
}

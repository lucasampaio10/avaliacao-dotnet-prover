using Contatos.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Contatos.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Contato> Contatos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contato>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Nome).IsRequired().HasMaxLength(100);
            entity.Property(c => c.DataNascimento).IsRequired();
            entity.Property(c => c.Sexo).IsRequired();
            entity.Property(c => c.Ativo).IsRequired();
            entity.Ignore(c => c.Idade);
        });
    }
}
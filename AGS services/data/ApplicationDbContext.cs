using AGS_models;
using AGS_Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Carrusel> Carrusel { get; set; }
    public DbSet<User> Usuarios { get; set; }

    public DbSet<Proyecto> Proyectos { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    public DbSet<Evento> Eventos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("user");

        modelBuilder.Entity<Proyecto>().ToTable("Proyecto");


        modelBuilder.Entity<Carrusel>().ToTable("carrusel");
        modelBuilder.Entity<Servicio>().ToTable("servicios");
        modelBuilder.Entity<Evento>().ToTable("eventos");
    }
}
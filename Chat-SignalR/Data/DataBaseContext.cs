using Chat_SignalR.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat_SignalR.Data
{
    public class DataBaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Breanch> Breanches { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DataBaseContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasIndex(u => u.publicId)
            .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.publicId)
                .HasColumnType("uuid");

            modelBuilder.Entity<Breanch>()
            .HasIndex(u => u.PublicId)
            .IsUnique();

            modelBuilder.Entity<Breanch>()
                .Property(u => u.PublicId)
                .HasColumnType("uuid");

            modelBuilder.Entity<Message>()
            .HasIndex(u => u.publicId)
            .IsUnique();

            modelBuilder.Entity<Message>()
                .Property(u => u.publicId)
                .HasColumnType("uuid");
        }
    }
}

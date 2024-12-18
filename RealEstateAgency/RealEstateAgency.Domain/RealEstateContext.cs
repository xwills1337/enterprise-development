using Microsoft.EntityFrameworkCore;

namespace RealEstateAgency.Domain;

public class RealEstateAgencyContext(DbContextOptions<RealEstateAgencyContext> options) : DbContext(options)
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<RealEstate> RealEstates { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Client>()
            .Property(c => c.FullName)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Client>()
            .Property(c => c.Passport)
            .HasMaxLength(12)
            .IsRequired();

        modelBuilder.Entity<Client>()
            .Property(c => c.Phone)
            .HasMaxLength(15)
            .IsRequired();

        modelBuilder.Entity<Client>()
            .Property(c => c.Address)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<RealEstate>()
            .HasKey(r => r.Id);

        modelBuilder.Entity<RealEstate>()
            .Property(r => r.Address)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<RealEstate>()
            .Property(r => r.Type)
            .IsRequired();

        modelBuilder.Entity<RealEstate>()
            .Property(r => r.Square)
            .IsRequired();

        modelBuilder.Entity<RealEstate>()
            .Property(r => r.NumberOfRooms)
            .IsRequired();

        modelBuilder.Entity<Order>()
            .HasKey(o => o.Id);

        modelBuilder.Entity<Order>()
            .Property(o => o.Time)
            .IsRequired();

        modelBuilder.Entity<Order>()
            .Property(o => o.Type)
            .IsRequired();

        modelBuilder.Entity<Order>()
            .Property(o => o.Price)
            .IsRequired();

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Client)
            .WithMany()
            .HasForeignKey("ClientId")
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Item)
            .WithMany()
            .HasForeignKey("RealEstateId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}

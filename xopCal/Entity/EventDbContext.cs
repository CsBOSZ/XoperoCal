using Microsoft.EntityFrameworkCore;

namespace xopCal.Entity;

public class EventDbContext : DbContext
{
    private string _connectionString = "Host=172.17.0.2;Database=cal;Username=postgres;Password=r";
    // "server=localhost;port=3306;database=cal;uid=root;password=r";
    
    public DbSet<EventCal> EventCals { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>(x =>
        {
            x.Property(r => r.Name)
                .IsRequired();
            x.Property(r => r.Email)
                .IsRequired();

        });
           
            
        modelBuilder.Entity<EventCal>()
            .Property(r => r.Name)
            .IsRequired();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(_connectionString);
    }
}
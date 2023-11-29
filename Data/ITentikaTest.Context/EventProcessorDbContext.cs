using Context.Entities.Event;
using Context.Entities.Incident;
using Microsoft.EntityFrameworkCore;

namespace Context;

public class EventProcessorDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Incident> Incidents { get; set; }

    public EventProcessorDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>().HasKey(x => x.Id);
        modelBuilder.Entity<Event>().Property(x => x.Type).IsRequired();
        modelBuilder.Entity<Event>().Property(x => x.Time).IsRequired();

        modelBuilder.Entity<Incident>().HasKey(x => x.Id);
        modelBuilder.Entity<Incident>().Property(x => x.Type).IsRequired();
        modelBuilder.Entity<Incident>().Property(x => x.Time).IsRequired();

        modelBuilder.Entity<Incident>()
            .HasMany(x => x.Events)
            .WithOne()
            .HasForeignKey("IncidentId")
            .IsRequired(false);
    }
}
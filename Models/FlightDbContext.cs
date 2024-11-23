using Microsoft.EntityFrameworkCore;

namespace FlightService.Models
{
    public class FlightDbContext : DbContext
    {
        public FlightDbContext(DbContextOptions<FlightDbContext> options) : base(options) { }

        public DbSet<Flight> Flights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>(entity =>
            {
                entity.ToTable("Flights");
                entity.HasKey(e => e.FlightId);
                entity.Property(e => e.FlightNumber).HasMaxLength(10).IsRequired();
                entity.Property(e => e.Airline).HasMaxLength(50).IsRequired();
                entity.Property(e => e.DepartureCity).HasMaxLength(50).IsRequired();
                entity.Property(e => e.ArrivalCity).HasMaxLength(50).IsRequired();
                entity.Property(e => e.DepartureTime).IsRequired();
                entity.Property(e => e.ArrivalTime).IsRequired();
                entity.Property(e => e.AvailableSeats).IsRequired();
                entity.Property(e => e.TicketPrice).HasColumnType("DECIMAL(10, 2)").IsRequired();

                entity.HasIndex(e => e.FlightNumber).IsUnique(); 

            });
        }
    }
}

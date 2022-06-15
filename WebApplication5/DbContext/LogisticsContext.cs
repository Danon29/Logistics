using System;
using Logistics.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Logistics.DBContext
{
    public partial class LogisticsContext : DbContext
    {
        public LogisticsContext()
        {
        }

        public LogisticsContext(DbContextOptions<LogisticsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CitiesFrom> CitiesFroms { get; set; }
        public virtual DbSet<CitiesTo> CitiesTos { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<FlightCity> FlightCities { get; set; }
        public virtual DbSet<FlightOrder> FlightOrders { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<Recipient> Recipients { get; set; }
        public virtual DbSet<Sender> Senders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-D2H8SGN;Database=Logistics;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Car>(entity =>
            {
                entity.Property(e => e.CarNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.LoadCapacity)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<CitiesFrom>(entity =>
            {
                entity.HasKey(e => e.CityFromId);

                entity.ToTable("CitiesFrom");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<CitiesTo>(entity =>
            {
                entity.HasKey(e => e.CitieToId);

                entity.ToTable("CitiesTo");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.Property(e => e.DriverId).HasColumnName("DriverID");

                entity.Property(e => e.Birthdate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.Property(e => e.BeginDate).HasColumnType("date");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flights_Cars");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flights_Drivers");
            });

            modelBuilder.Entity<FlightCity>(entity =>
            {
                entity.HasKey(e => e.SecondId);

                entity.ToTable("FlightCity");

                entity.Property(e => e.SecondId).HasColumnName("secondId");

                entity.Property(e => e.ArrivalDate).HasColumnType("date");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.FlightCities)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightCity_CitiesTo");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.FlightCities)
                    .HasForeignKey(d => d.FlightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightCity_Flights");
            });

            modelBuilder.Entity<FlightOrder>(entity =>
            {
                entity.HasKey(e => e.FirstId);

                entity.ToTable("FlightOrder");

                entity.Property(e => e.FirstId).HasColumnName("firstId");

                entity.Property(e => e.BeginDate).HasColumnType("date");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.HasOne(d => d.ContractNumberNavigation)
                    .WithMany(p => p.FlightOrders)
                    .HasForeignKey(d => d.ContractNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightOrder_Orders");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.FlightOrders)
                    .HasForeignKey(d => d.FlightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightOrder_Flights");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.ContractNumber);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.CityFrom)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CityFromId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_CitiesFrom");

                entity.HasOne(d => d.CityTo)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CityToId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_CitiesTo");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Prices");

                entity.HasOne(d => d.Recipient)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.RecipientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Recipients");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Senders");
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Recipient>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Sender>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

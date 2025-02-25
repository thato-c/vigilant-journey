using AutomotiveRepairSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveRepairSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ScheduledService> ScheduledServices { get; set; }
        public DbSet<ServiceBatch> ServicesBatch { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Fuel> Fuels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehicle>().ToTable("Vehicle");
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Service>().ToTable("Service");
            modelBuilder.Entity<ScheduledService>().ToTable("ScheduledService");
            modelBuilder.Entity<ServiceBatch>().ToTable("ServiceBatch");
            modelBuilder.Entity<Invoice>().ToTable("Invoice");
            modelBuilder.Entity<Make>().ToTable("Make");
            modelBuilder.Entity<Model>().ToTable("Model");
            modelBuilder.Entity<Fuel>().ToTable("Fuel");

            // Configure the relationship between Customer and Vehicle (1:M)
            modelBuilder.Entity<Customer>()
                .HasMany(customer => customer.Vehicles)
                .WithOne(vehicle => vehicle.Customer)
                .HasForeignKey(vehicle => vehicle.CustomerId);

            // Configure the relationship between Make and Vehicle (1:M)
            modelBuilder.Entity<Make>()
                .HasMany(make => make.Vehicles)
                .WithOne(vehicle => vehicle.Make)
                .HasForeignKey(vehicle => vehicle.MakeId);

            // Configure the relationship between Model and Vehicle (1:M)
            modelBuilder.Entity<Model>()
                .HasMany(model => model.Vehicles)
                .WithOne(vehicle => vehicle.Model)
                .HasForeignKey(vehicle => vehicle.ModelId);

            // Configure the relationship between Fuel amd Vehicle (1:M)
            modelBuilder.Entity<Fuel>()
                .HasMany(fuel => fuel.Vehicles)
                .WithOne(vehicle => vehicle.Fuel)
                .HasForeignKey(vehicle => vehicle.FuelId);

            // Configure the relationship between Vehicle and ScheduledService
            modelBuilder.Entity<Vehicle>()
                .HasMany(vehicle => vehicle.ScheduledServices)
                .WithOne(scheduledService => scheduledService.Vehicle)
                .HasForeignKey(scheduledService => scheduledService.VehicleId);

            // Configure the relationship between Service and ScheduledService
            modelBuilder.Entity<Service>()
                .HasMany(service => service.ScheduledServices)
                .WithOne(scheduledService => scheduledService.Service)
                .HasForeignKey(scheduledService => scheduledService.ServiceId);

            // Configure the relationship between ServiceBatch and ScheduledService
            modelBuilder.Entity<ServiceBatch>()
                .HasMany(serviceBatch => serviceBatch.ScheduledServices)
                .WithOne(scheduledService => scheduledService.ServiceBatch)
                .HasForeignKey(scheduledService => scheduledService.ServiceBatchId);

            // Configure the relationship between Invoice and ServiceBatch
            modelBuilder.Entity<ServiceBatch>()
                .HasOne(serviceBatch => serviceBatch.Invoice)
                .WithOne(invoice => invoice.ServiceBatch)
                .HasForeignKey<Invoice>(invoice => invoice.ServiceBatchId);

            // Configure the relationship between Invoice and Payment
            modelBuilder.Entity<Invoice>()
                .HasMany(invoice => invoice.Payments)
                .WithOne(payment => payment.Invoice)
                .HasForeignKey(payment => payment.InvoiceId);
        }
    }
}

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Workshops.Domain.Entities;

namespace Workshops.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<WorkshopsEntity> Workshops { get; set; }
    public DbSet<CollaboratorEntity> Collaborators { get; set; }
    public DbSet<AttendeesRecordEntity> AttendeesRecords {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Application");
        
        modelBuilder.Entity<WorkshopsEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.Slug, e.RealizationDate })
                .IsUnique()
                .IsDescending(false, true);
            entity.Property(e => e.Name)
                .HasMaxLength(126)
                .IsRequired();
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsRequired();
            entity.Property(e => e.Slug)
                .IsRequired();
            entity.Property(e => e.RealizationDate)
                .IsRequired();
            entity.Property(e => e.Capacity)
                .IsRequired(false);
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnUpdate();
        });
        
        modelBuilder.Entity<CollaboratorEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
                .HasMaxLength(126)
                .IsRequired();
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnUpdate();
        });

        modelBuilder.Entity<AttendeesRecordEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Workshop)
                .WithMany(e => e.Attendees)
                .HasForeignKey(e => e.WorkshopId);
            entity.HasOne(e => e.Collaborator)
                .WithMany(e => e.Attendances)
                .HasForeignKey(e => e.CollaboratorId);
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnUpdate();
        });
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
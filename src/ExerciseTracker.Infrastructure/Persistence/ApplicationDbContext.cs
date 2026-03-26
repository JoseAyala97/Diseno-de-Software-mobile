using ExerciseTracker.Domain.Entities;
using ExerciseTracker.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTracker.Infrastructure.Persistence;

public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ExerciseRecord> ExerciseRecords => Set<ExerciseRecord>();
    public DbSet<Goal> Goals => Set<Goal>();
    public DbSet<Reminder> Reminders => Set<Reminder>();
    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ExerciseRecord>(entity =>
        {
            entity.ToTable("ExerciseRecords");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.UserId).IsRequired().HasMaxLength(450);
            entity.Property(x => x.DurationMinutes).IsRequired();
            entity.Property(x => x.Notes).HasMaxLength(400);
        });

        builder.Entity<Goal>(entity =>
        {
            entity.ToTable("Goals");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.UserId).IsRequired().HasMaxLength(450);
        });

        builder.Entity<Reminder>(entity =>
        {
            entity.ToTable("Reminders");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.UserId).IsRequired().HasMaxLength(450);
            entity.Property(x => x.Title).IsRequired().HasMaxLength(120);
            entity.Property(x => x.Message).HasMaxLength(250);
        });

        builder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notifications");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.UserId).IsRequired().HasMaxLength(450);
            entity.Property(x => x.Title).IsRequired().HasMaxLength(120);
            entity.Property(x => x.Message).HasMaxLength(250);
        });
    }
}

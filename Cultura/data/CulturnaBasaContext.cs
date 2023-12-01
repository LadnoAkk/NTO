using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Cultura.data;

public partial class CulturnaBasaContext : DbContext
{
    public CulturnaBasaContext()
    {
    }

    public CulturnaBasaContext(DbContextOptions<CulturnaBasaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Space> Spaces { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<WorkType> WorkTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("data source =C:\\Users\\1\\source\\repos\\NTO\\Cultura\\Database\\CulturnaBasa");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(entity =>
        {
            entity.ToTable("Application");

            entity.Property(e => e.Date).HasColumnType("TEXT (12)");
            entity.Property(e => e.Description).HasColumnType("TEXT (200)");
            entity.Property(e => e.Timing).HasColumnType("TEXT (12)");

            entity.HasOne(d => d.Event).WithMany(p => p.Applications)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Space).WithMany(p => p.Applications)
                .HasForeignKey(d => d.SpaceId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Status).WithMany(p => p.Applications)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Work).WithMany(p => p.Applications)
                .HasForeignKey(d => d.WorkId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.Property(e => e.Data).HasColumnType("TEXT (12)");
            entity.Property(e => e.Description).HasColumnType("TEXT (200)");

            entity.HasOne(d => d.Type).WithMany(p => p.Events)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.ToTable("EventType");

            entity.Property(e => e.Name).HasColumnType("TEXT (30)");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.ToTable("Reservation");

            entity.Property(e => e.BeginningDate).HasColumnType("TEXT (12)");
            entity.Property(e => e.BeginningTime).HasColumnType("TEXT (7)");
            entity.Property(e => e.Comments).HasColumnType("TEXT (200)");
            entity.Property(e => e.CreateDate).HasColumnType("TEXT (12)");
            entity.Property(e => e.EndingDate).HasColumnType("TEXT (12)");
            entity.Property(e => e.EndingTime).HasColumnType("TEXT (7)");

            entity.HasOne(d => d.Event).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Space).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.SpaceId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Space>(entity =>
        {
            entity.ToTable("Space");

            entity.Property(e => e.Name).HasColumnType("TEXT (30)");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.Name).HasColumnType("TEXT (30)");
        });

        modelBuilder.Entity<WorkType>(entity =>
        {
            entity.ToTable("WorkType");

            entity.Property(e => e.Name).HasColumnType("TEXT (30)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

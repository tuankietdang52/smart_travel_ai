using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartTravelAI.Models;

namespace SmartTravelAI.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TagKey> TagKeys { get; set; }

    public virtual DbSet<TagValue> TagValues { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserTrip> UserTrips { get; set; }

    public virtual DbSet<UserTripRoute> UserTripRoutes { get; set; }
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.

    public virtual DbSet<UserTripWaypoint> UserTripWaypoints { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TagKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tag_keys_pkey");

            entity.ToTable("tag_keys");

            entity.HasIndex(e => e.KeyName, "tag_keys_key_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.KeyName)
                .HasMaxLength(100)
                .HasColumnName("key_name");
        });

        modelBuilder.Entity<TagValue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tag_values_pkey");

            entity.ToTable("tag_values");

            entity.HasIndex(e => new { e.KeyId, e.ValueName }, "tag_values_key_id_value_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.KeyId).HasColumnName("key_id");
            entity.Property(e => e.ValueName)
                .HasMaxLength(100)
                .HasColumnName("value_name");

            entity.HasOne(d => d.Key).WithMany(p => p.TagValues)
                .HasForeignKey(d => d.KeyId)
                .HasConstraintName("tag_values_key_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasMany(d => d.TagKeys).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Favorite",
                    r => r.HasOne<TagKey>().WithMany()
                        .HasForeignKey("TagKey")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_fv_tag"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_fv_user"),
                    j =>
                    {
                        j.HasKey("UserId", "TagKey").HasName("favorites_pkey");
                        j.ToTable("favorites");
                        j.IndexerProperty<long>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<long>("TagKey").HasColumnName("tag_key");
                    });
        });

        modelBuilder.Entity<UserTrip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_trip_pkey");

            entity.ToTable("user_trip");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.TotalDistance).HasColumnName("total_distance");
            entity.Property(e => e.TotalDuration).HasColumnName("total_duration");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserTrips)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_trip_user_id_fkey");
        });

        modelBuilder.Entity<UserTripRoute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_trip_route_pkey");

            entity.ToTable("user_trip_route");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Distance).HasColumnName("distance");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.Geometry).HasColumnName("geometry");
            entity.Property(e => e.TripId).HasColumnName("trip_id");

            entity.HasOne(d => d.Trip).WithMany(p => p.UserTripRoutes)
                .HasForeignKey(d => d.TripId)
                .HasConstraintName("user_trip_route_trip_id_fkey");
        });

        modelBuilder.Entity<UserTripWaypoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_trip_waypoint_pkey");

            entity.ToTable("user_trip_waypoint");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArriveAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("arrive_at");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Index).HasColumnName("index");
            entity.Property(e => e.Lat).HasColumnName("lat");
            entity.Property(e => e.LeaveAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("leave_at");
            entity.Property(e => e.Lon).HasColumnName("lon");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.TagValue).HasColumnName("tag_value");
            entity.Property(e => e.TripId).HasColumnName("trip_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.TagValueNavigation).WithMany(p => p.UserTripWaypoints)
                .HasForeignKey(d => d.TagValue)
                .HasConstraintName("user_trip_tag");

            entity.HasOne(d => d.Trip).WithMany(p => p.UserTripWaypoints)
                .HasForeignKey(d => d.TripId)
                .HasConstraintName("user_trip_waypoint_trip_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

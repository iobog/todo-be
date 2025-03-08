using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ToDo.Api.Data;
public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TTag> TTags { get; set; }

    public virtual DbSet<TTask> TTasks { get; set; }

    public virtual DbSet<TTaskTag> TTaskTags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TTag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TTags__3214EC0708F070F5");

            entity.HasIndex(e => e.Name, "UQ__TTags__737584F6ED1DF5A0").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TTask__3214EC07438F5A01");

            entity.ToTable("TTask");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.IsCompleted).HasDefaultValue(false);
            entity.Property(e => e.Notes)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TTaskTag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TTaskTag__3214EC07AA372B8A");

            entity.HasOne(d => d.Tag).WithMany(p => p.TTaskTags)
                .HasForeignKey(d => d.TagId)
                .HasConstraintName("FK__TTaskTags__TagId__4D94879B");

            entity.HasOne(d => d.Task).WithMany(p => p.TTaskTags)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__TTaskTags__TaskI__4CA06362");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

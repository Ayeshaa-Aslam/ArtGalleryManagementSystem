using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace visualbasicproject;

public partial class ArtgalleryMsContext : DbContext
{
    public ArtgalleryMsContext()
    {
    }

    public ArtgalleryMsContext(DbContextOptions<ArtgalleryMsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aartist> Aartists { get; set; }

    public virtual DbSet<Art> Arts { get; set; }

    public virtual DbSet<Exhibition> Exhibitions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ArtgalleryMS");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aartist>(entity =>
        {
            entity.HasKey(e => e.ArtistId);

            entity.ToTable("Aartist");

            entity.Property(e => e.ArtistId).HasColumnName("Artist_Id");
            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Nationality)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Art>(entity =>
        {
            entity.ToTable("Art");

            entity.Property(e => e.ArtId).HasColumnName("Art_Id");
            entity.Property(e => e.ArtistId).HasColumnName("Artist_Id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.YearCreated).HasColumnName("Year_created");

            entity.HasOne(d => d.Artist).WithMany(p => p.Arts)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Art_Art");
        });

        modelBuilder.Entity<Exhibition>(entity =>
        {
            entity.ToTable("Exhibition");

            entity.Property(e => e.ExhibitionId).HasColumnName("Exhibition_Id");
            entity.Property(e => e.ArtId).HasColumnName("Art_Id");
            entity.Property(e => e.ArtistId).HasColumnName("Artist_Id");
            entity.Property(e => e.EndDate).HasColumnName("End_date");
            entity.Property(e => e.Location)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.StartDate).HasColumnName("Start_date");

            entity.HasOne(d => d.Artist).WithMany(p => p.Exhibitions)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Exhibition_Aartist");
        });

        OnModelCreatingPartial(modelBuilder);
    }

   partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Encuesta.Models;

public partial class EncuestaContext : DbContext
{
    public EncuestaContext()
    {
    }

    public EncuestaContext(DbContextOptions<EncuestaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Np> Nps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=MSI; database=ENCUESTA; Trusted_Connection=SSPI; Encrypt=false; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Np>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Nps__3214EC075E850D4A");

            entity.Property(e => e.Comment)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

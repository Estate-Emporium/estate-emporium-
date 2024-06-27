using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace estate_emporium.Models;

public partial class EstateDbContext : DbContext
{
    public EstateDbContext()
    {
    }

    public EstateDbContext(DbContextOptions<EstateDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PropertySale> PropertySales { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PropertySale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__Property__1EE3C41F6CFFE1AC");

            entity.HasOne(d => d.Status).WithMany(p => p.PropertySales).HasConstraintName("FK__PropertyS__Statu__267ABA7A");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Status__C8EE204377D645FF");

            entity.Property(e => e.StatusId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

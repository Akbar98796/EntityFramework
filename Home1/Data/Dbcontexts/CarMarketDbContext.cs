using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Reflection.PortableExecutable;
using System.Runtime;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WpfApp4.Data.Models;

namespace WpfApp4.Data.Dbcontexts;

public partial class CarMarketDbContext : DbContext
{
    public CarMarketDbContext()
    {
    }

    public CarMarketDbContext(DbContextOptions<CarMarketDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CarBrandModel> CarBrandModels { get; set; }

    public virtual DbSet<CarColorModel> CarColorModels { get; set; }

    public virtual DbSet<CarModel> CarModels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ConfigurationBuilder builder = new();

        builder.AddJsonFile("appsettings.json");
        IConfigurationRoot configuration = builder.Build();

        var connectionString = configuration.GetConnectionString("CarMarketDb");
        optionsBuilder.UseSqlServer(connectionString);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarModel>(entity =>
        {
            entity.HasIndex(e => e.CarBrandId, "IX_CarModels_CarBrandId");

            entity.HasIndex(e => e.CarColorId, "IX_CarModels_CarColorId");

            entity.HasOne(d => d.CarBrand)
                  .WithMany(p => p.CarModels)
                  .HasForeignKey(d => d.CarBrandId);

            entity.HasOne(d => d.CarColor)
                  .WithMany(p => p.CarModels)
                  .HasForeignKey(d => d.CarColorId);
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

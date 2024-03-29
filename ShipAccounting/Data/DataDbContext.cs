using Microsoft.EntityFrameworkCore;
using ShipAccounting.Models;
using System;

namespace ShipAccounting.Data;

public class DataDbContext(DbContextOptions<DataDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<Outcome>()
            .Property(o => o.Result)
            .HasConversion(r => r.ToString(), s => (Results)Enum.Parse(typeof(Results), s));
    }

    public DbSet<ShipClass> Classes { get; set; }
    public DbSet<Battle> Battles { get; set; }
    public DbSet<Ship> Ships { get; set; }
    public DbSet<Outcome> Outcomes { get; set; }
}

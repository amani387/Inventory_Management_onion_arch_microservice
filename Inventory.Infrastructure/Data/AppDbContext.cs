using Inventory.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Inventory.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }
}
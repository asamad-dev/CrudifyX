using CrudifyX.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CrudifyX.Web.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        base.OnModelCreating(builder);

        builder.Entity<Product>()
           .Property(p => p.Price)
           .HasPrecision(10, 2);

        builder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Wireless Mouse", Price = 19.99m, Quantity = 50 },
            new Product { Id = 2, Name = "Mechanical Keyboard", Price = 89.99m, Quantity = 30 },
            new Product { Id = 3, Name = "Gaming Monitor", Price = 249.99m, Quantity = 15 },
            new Product { Id = 4, Name = "USB-C Hub", Price = 29.99m, Quantity = 100 },
            new Product { Id = 5, Name = "External Hard Drive 1TB", Price = 59.99m, Quantity = 25 },
            new Product { Id = 6, Name = "Laptop Stand", Price = 34.99m, Quantity = 40 },
            new Product { Id = 7, Name = "Bluetooth Speaker", Price = 45.00m, Quantity = 20 },
            new Product { Id = 8, Name = "HD Webcam", Price = 39.99m, Quantity = 35 },
            new Product { Id = 9, Name = "Noise Cancelling Headphones", Price = 129.99m, Quantity = 10 },
            new Product { Id = 10, Name = "Smartphone Holder", Price = 12.99m, Quantity = 60 }
        );
    }
}

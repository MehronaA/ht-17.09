using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<ProductsTag> ProductsTags { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductsTag>()
        .HasKey(p => new { p.ProductId, p.TagId });

        modelBuilder.Entity<Category>()
        .HasMany(c => c.Products)
        .WithOne(p => p.Category)
        .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<Order>()
        .HasOne(o => o.User)
        .WithMany(u => u.Orders)
        .HasForeignKey(o => o.UserId);

        modelBuilder.Entity<Order>()
        .HasMany(o => o.OrderItems)
        .WithOne(ot => ot.Order)
        .HasForeignKey(ot => ot.OrderId);

        modelBuilder.Entity<OrderItem>()
        .HasOne(ot => ot.Product)
        .WithMany(p => p.OrderItems)
        .HasForeignKey(ot => ot.ProductId);

        modelBuilder.Entity<Product>()
        .HasMany(p => p.ProductTags)
        .WithOne(pt => pt.Product)
        .HasForeignKey(pt => pt.ProductId);

        modelBuilder.Entity<Tag>()
        .HasMany(t => t.ProductsTag)
        .WithOne(pt => pt.Tag)
        .HasForeignKey(pt => pt.Tag);

        modelBuilder.Entity<User>()
        .HasOne(u => u.Profile)
        .WithOne(p => p.User)
        .HasForeignKey<Profile>(p => p.UserId);


        modelBuilder.Entity<Category>()
        .Property(c => c.Name)
        .HasMaxLength(50)
        .IsRequired();

        modelBuilder.Entity<Category>()
        .Property(c => c.IsActive)
        .HasDefaultValue(false);

        modelBuilder.Entity<Category>()
        .Property(c => c.CreatedAt)
        .HasDefaultValue(DateTime.Now);

        modelBuilder.Entity<Category>()
        .Property(c => c.Id)
        .ValueGeneratedOnAdd();

        modelBuilder.Entity<Order>()
        .Property(o => o.Id)
        .ValueGeneratedOnAdd();

        modelBuilder.Entity<Order>()
        .Property(o => o.UserId)
        .IsRequired()
        .ValueGeneratedOnAdd();

        modelBuilder.Entity<OrderItem>()
        .Property(oi => oi.Id)
        .ValueGeneratedOnAdd();

        modelBuilder.Entity<OrderItem>()
        .Property(oi => oi.ProductId)
        .IsRequired();

        modelBuilder.Entity<OrderItem>()
        .Property(oi => oi.OrderId)
        .IsRequired();

        modelBuilder.Entity<OrderItem>()
        .Property(oi => oi.Quantity)
        .HasDefaultValue(0);

        modelBuilder.Entity<Product>()
        .Property(p => p.Id)
        .ValueGeneratedOnAdd();

        modelBuilder.Entity<Product>()
        .Property(p => p.Name)
        .HasMaxLength(150)
        .IsRequired();

        modelBuilder.Entity<Product>()
        .Property(p => p.Price)
        .HasDefaultValue(0);

        modelBuilder.Entity<Product>()
        .Property(p => p.Stock)
        .HasDefaultValue(0);

    }
    
}

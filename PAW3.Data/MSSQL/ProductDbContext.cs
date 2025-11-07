using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PAW3.Models.Entities;
using ModelsTask = PAW3.Models.Entities.Task;

namespace PAW3.Data.MSSQL;

public partial class ProductDbContext : DbContext
{
    public ProductDbContext()
    {
    }

    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Component> Components { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<ModelsTask> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAction> UserActions { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Action<Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category>> buildAction = entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2B758750D5");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
        };
        modelBuilder.Entity<Category>(buildAction);

        modelBuilder.Entity<Component>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Content)
                .HasMaxLength(100)
                .HasColumnName("content");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK__Inventor__F5FDE6D3B5E07827");

            entity.ToTable("Inventory");

            entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
            entity.Property(e => e.DateAdded).HasColumnType("datetime");
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3213E83FCD81B500");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IsRead)
                .HasDefaultValue(false)
                .HasColumnName("is_read");
            entity.Property(e => e.Message)
                .HasMaxLength(255)
                .HasColumnName("message");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6EDF6CF059B");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ProductName).HasMaxLength(255);
            entity.Property(e => e.Rating).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Products__Catego__3B40CD36");

            entity.HasOne(d => d.Inventory).WithMany(p => p.Products)
                .HasForeignKey(d => d.InventoryId)
                .HasConstraintName("FK__Products__Invent__395884C4");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__Products__Suppli__3A4CA8FD");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A8F3651B9");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(255);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__4BE66694FF68A36C");

            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.Address).HasMaxLength(1000);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.ContactName).HasMaxLength(255);
            entity.Property(e => e.ContactTitle).HasMaxLength(255);
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(255);
            entity.Property(e => e.SupplierName).HasMaxLength(255);
        });

        modelBuilder.Entity<ModelsTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tasks__3214EC07230D435E");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC40C832DB");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Username).HasMaxLength(255);
        });

        modelBuilder.Entity<UserAction>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Id).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Id).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.RoldId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("RoldID");
            entity.Property(e => e.UserId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("UserID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

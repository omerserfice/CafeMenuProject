using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CafeMenuProject.Models
{
    public partial class CafeMenuDbContext : DbContext
    {
        public CafeMenuDbContext()
        {
        }

        public CafeMenuDbContext(DbContextOptions<CafeMenuDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Productproperty> Productproperties { get; set; } = null!;
        public virtual DbSet<Property> Properties { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=CafeMenuDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("CATEGORY");

                entity.Property(e => e.Categoryid).HasColumnName("CATEGORYID");

                entity.Property(e => e.Categoryname)
                    .HasMaxLength(100)
                    .HasColumnName("CATEGORYNAME");

                entity.Property(e => e.Createddate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATEDDATE");

                entity.Property(e => e.Creatoruserid).HasColumnName("CREATORUSERID");

                entity.Property(e => e.Isdeleted).HasColumnName("ISDELETED");

                entity.Property(e => e.Parentcategoryid).HasColumnName("PARENTCATEGORYID");

                entity.HasOne(d => d.Creatoruser)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.Creatoruserid)
                    .HasConstraintName("FK_CATEGORY_USER");

                entity.HasOne(d => d.Parentcategory)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.Parentcategoryid)
                    .HasConstraintName("FK_CATEGORY_PRODUCTPROPERTY");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("PRODUCT");

                entity.Property(e => e.Productid).HasColumnName("PRODUCTID");

                entity.Property(e => e.Categoryid).HasColumnName("CATEGORYID");

                entity.Property(e => e.Createddate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATEDDATE");

                entity.Property(e => e.Creatoruserid).HasColumnName("CREATORUSERID");

                entity.Property(e => e.Imagepath).HasColumnName("IMAGEPATH");

                entity.Property(e => e.Isdeleted).HasColumnName("ISDELETED");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Productname)
                    .HasMaxLength(200)
                    .HasColumnName("PRODUCTNAME");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Categoryid)
                    .HasConstraintName("FK_PRODUCT_CATEGORY");

                entity.HasOne(d => d.Creatoruser)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Creatoruserid)
                    .HasConstraintName("FK_PRODUCT_USER");
            });

            modelBuilder.Entity<Productproperty>(entity =>
            {
                entity.ToTable("PRODUCTPROPERTY");

                entity.Property(e => e.Productpropertyid).HasColumnName("PRODUCTPROPERTYID");

                entity.Property(e => e.Productid).HasColumnName("PRODUCTID");

                entity.Property(e => e.Propertyid).HasColumnName("PROPERTYID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Productproperties)
                    .HasForeignKey(d => d.Productid)
                    .HasConstraintName("FK_PRODUCTPROPERTY_PRODUCT");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Productproperties)
                    .HasForeignKey(d => d.Propertyid)
                    .HasConstraintName("FK_PRODUCTPROPERTY_PROPERTY");
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.ToTable("PROPERTY");

                entity.Property(e => e.Propertyid).HasColumnName("PROPERTYID");

                entity.Property(e => e.Key).HasColumnName("KEY");

                entity.Property(e => e.Value)
                    .HasMaxLength(500)
                    .HasColumnName("VALUE");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER");

                entity.Property(e => e.Userid).HasColumnName("USERID");

                entity.Property(e => e.Hashpassword).HasColumnName("HASHPASSWORD");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("NAME");

                entity.Property(e => e.Saltpassword).HasColumnName("SALTPASSWORD");

                entity.Property(e => e.Surname)
                    .HasMaxLength(100)
                    .HasColumnName("SURNAME");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .HasColumnName("USERNAME");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

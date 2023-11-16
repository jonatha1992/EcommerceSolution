using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Domain
{


    public class Product : BaseDomainModel
    {

        [Column(TypeName = "NVARCHAR(100)")]
        public string? Name { get; set; }

        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "NVARCHAR(4000)")]
        public string? Description { get; set; }

        public int Rating { get; set; }

        [Column(TypeName = "NVARCHAR(100)")]
        public string? Seller { get; set; }

        public int Stock { get; set; }

        public ProductStatus Status { get; set; } = ProductStatus.Activo;

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<Image>? Images{ get; set; }
    }

    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product));

            builder.HasMany(p => p.Reviews)
                    .WithOne(r => r.Product)
                    .HasForeignKey(r => r.ProductId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Images)
                 .WithOne(r => r.Product)
                 .HasForeignKey(r => r.ProductId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
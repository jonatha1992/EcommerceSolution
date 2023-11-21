using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Domain
{


    public class Category : BaseDomainModel
    {

        [Column(TypeName = "NVARCHAR(100)")]
        public string? Name { get; set; }
        public virtual ICollection<Product>? Products { get; set; }

    }

    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.ToTable(nameof(Category));

            builder.HasMany(c => c.Products)
                    .WithOne(p=>p.Category)
                    .HasForeignKey(p=> p.CategoryId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
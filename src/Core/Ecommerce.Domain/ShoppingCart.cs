using Ecommerce.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Domain
{


    public class ShoppingCart : BaseDomainModel
    {
        public Guid? ShoppingCartMasterId { get; set; }
        public virtual ICollection<ShoppingCartItem>? ShoppingCartItems { get; set; }
    }

    public class configShoppingCart : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasMany(s=> s.ShoppingCartItems)
                   .WithOne(sc => sc.ShoppingCart)
                   .HasForeignKey(sc=> sc.ShoppingCartId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
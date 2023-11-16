using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Domain
{


    public class Order : BaseDomainModel
    {
     
        public string? BuyerName { get; set; }
        public string? BuyerEmail { get; set; }
        public OrderAddress? OrderAddress { get; set; }
        public IReadOnlyList<OrderItem>? OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public decimal ShippingPrice { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public string? StripeApiKey { get; set; }


        public Order() { }
        public Order(
         string buyerName,
         string buyerEmail,
         OrderAddress orderAddress,
         decimal subtotal,
         decimal total,
         decimal tax,
         decimal shippingPrice
     )
        {
            BuyerName = buyerName;
            BuyerEmail = buyerEmail;
            OrderAddress = orderAddress;
            Subtotal = subtotal;
            Total = total;
            Tax = tax;
            ShippingPrice = shippingPrice;
        }
    }

    public class OrderConfig : IEntityTypeConfiguration<Order>
    {

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(nameof(Order));
            builder.OwnsOne(o => o.OrderAddress, a =>
            {
                a.WithOwner();
            });

            builder.HasMany(o => o.OrderItems)
                .WithOne().
                OnDelete(DeleteBehavior.Cascade);

            builder.Property(o => o.Status).HasConversion(
                o => o.ToString(),
                o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o));

            builder.Property(x=>x.Subtotal).HasPrecision(10, 2);
            builder.Property(x => x.Total).HasPrecision(10, 2);
            builder.Property(x => x.Tax).HasPrecision(10, 2);
            builder.Property(x => x.ShippingPrice).HasPrecision(10, 2);

        }
    }
}
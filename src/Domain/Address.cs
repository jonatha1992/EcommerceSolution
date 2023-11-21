using Ecommerce.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Domain
{
    public class Address : BaseDomainModel
    {

        public string? AdressHome { get; set; }

        public string? Ciudad { get; set; }

        public string? Departamento { get; set; }

        public string? CodigoPostal { get; set; }

        public string? Username { get; set; }

        public string? Pais { get; set; }
    }


    public class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {

            builder.ToTable(nameof(Address));
        }
    }
}
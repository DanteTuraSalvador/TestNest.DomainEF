using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestNest.DomainEFDemo.Guests;
using TestNest.DomainEFDemo.StronglyTypeIds;
using TestNest.DomainEFDemo.Infrastructure.Persistence.Configurations.Common;

namespace TestNest.DomainEFDemo.Infrastructure.Persistence.Configurations;

public class IdentificationConfiguration : IEntityTypeConfiguration<Identification>
{
    public void Configure(EntityTypeBuilder<Identification> builder)
    {
        builder.ToTable("Identifications");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .ConfigureStronglyTypedId<IdTypeId>()
            .HasColumnName("IdTypeId")
            .IsRequired();

        builder.OwnsOne(identification => identification.IdTypeName, identificationTypeName =>
        {
            identificationTypeName.Property(idTypeName => idTypeName.Value)
                .HasColumnName("IdTypeName")
                .IsRequired();
        });
    }
}
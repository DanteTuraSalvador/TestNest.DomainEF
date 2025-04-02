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

        builder.HasKey(i => i.Id)
            .IsClustered(false);

        builder.Property(i => i.Id)
            .ConfigureStronglyTypedId<IdTypeId>()
            .HasColumnName("IdTypeId")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.OwnsOne(identification => identification.IdTypeName, identificationTypeName =>
        {
            identificationTypeName.Property(idTypeName => idTypeName.Value)
                .HasColumnName("IdTypeName")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired();
        });

        // Ensure the shadow property has the same max length and type
        builder.Property<string>("IdTypeName_Shadow")
            .HasColumnName("IdTypeName")
            .HasColumnType("NVARCHAR") // Same type
            .HasMaxLength(100) // Same max length
            .IsRequired();

        builder.HasIndex("IdTypeName_Shadow")
            .HasDatabaseName("IX_Identifications_IdTypeName")
            .IsClustered();


    }
}
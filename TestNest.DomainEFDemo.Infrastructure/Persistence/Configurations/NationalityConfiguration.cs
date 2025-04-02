using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestNest.DomainEFDemo.Guests;
using TestNest.DomainEFDemo.Infrastructure.Persistence.Configurations.Common;
using TestNest.DomainEFDemo.StronglyTypeIds;

namespace TestNest.DomainEFDemo.Infrastructure.Persistence.Configurations;

public class NationalityConfiguration : IEntityTypeConfiguration<Nationality>
{
    public void Configure(EntityTypeBuilder<Nationality> builder)
    {
        builder.ToTable("Nationalities");

        builder.HasKey(n => n.Id);

        builder.Property(n => n.Id)
             .ConfigureStronglyTypedId<NationalityId>()
             .HasColumnName("NationalityId")
             .IsRequired();

        builder.OwnsOne(nationality => nationality.NationalityName, nationalityName =>
        {
            nationalityName.Property(nationalityName => nationalityName.Value)
                .HasColumnName("NationalityName")
                .IsRequired();
        });

    }
}
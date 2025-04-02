using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using TestNest.DomainEFDemo.Guests;
using TestNest.DomainEFDemo.StronglyTypeIds;
using TestNest.DomainEFDemo.Infrastructure.Persistence.Configurations.Common;
using TestNest.DomainEFDemo.ValueObjects.Enums;

namespace TestNest.DomainEFDemo.Infrastructure.Persistence.Configurations;

public class GuestConfiguration : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        builder.ToTable("Guests");

        builder.HasKey(guest => guest.Id)
            .IsClustered();

        builder.Property(guest => guest.Id)
            .ConfigureStronglyTypedId<GuestId>()
            .HasColumnName("GuestId")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.Property(guest => guest.NationalityId)
            .ConfigureStronglyTypedId<NationalityId>()
            .HasColumnName("NationalityId")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.Property(guest => guest.IdTypeId)
            .ConfigureStronglyTypedId<IdTypeId>()
            .HasColumnName("IdTypeId")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.OwnsOne(guest => guest.GuestName, guestName =>
        {
            guestName.Property(personName => personName.FirstName)
                .HasColumnName("FirstName")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired();

            guestName.Property(personName => personName.MiddleName)
                .HasColumnName("MiddleName")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired(false);

            guestName.Property(personName => personName.LastName)
                .HasColumnName("LastName")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(guest => guest.GuestEmail, guestEmail =>
        {
            guestEmail.Property(emailAddress => emailAddress.Value)
                .HasColumnName("Email")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(guest => guest.GuestSimpleAddress, guestSimpleAddress =>
        {
            guestSimpleAddress.OwnsOne(simpleAddress => simpleAddress.Address, simpleAddress =>
            {
                simpleAddress.Property(address => address.AddressLine)
                    .HasColumnName("AddressLine")
                    .HasColumnType("NVARCHAR")
                    .HasMaxLength(200)
                    .IsRequired();

                simpleAddress.Property(address => address.City)
                    .HasColumnName("City")
                    .HasColumnType("NVARCHAR")
                    .HasMaxLength(100)
                    .IsRequired();
            });

            guestSimpleAddress.Property(address => address.Country)
                .HasColumnName("Country")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(guest => guest.IdNumber, idNumber =>
        {
            idNumber.Property(idNum => idNum.Value)
                .HasColumnName("IdNumber")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.Property(g => g.GuestType)
            .HasConversion(
                type => type.Id,
                id => GuestType.FromId(id))
            .HasColumnName("GuestType")
            .IsRequired();

        builder.HasOne<Nationality>()
            .WithMany()
            .HasForeignKey(guest => guest.NationalityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Identification>()
            .WithMany()
            .HasForeignKey(guest => guest.IdTypeId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}

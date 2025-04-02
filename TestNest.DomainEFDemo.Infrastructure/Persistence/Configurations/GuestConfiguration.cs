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

        builder.HasKey(guest => guest.Id);

        builder.Property(guest => guest.Id)
            .ConfigureStronglyTypedId<GuestId>()
            .HasColumnName("GuestId")
            .IsRequired();

        builder.Property(guest => guest.NationalityId)
            .ConfigureStronglyTypedId<NationalityId>()
            .HasColumnName("NationalityId")
            .IsRequired();

        builder.Property(guest => guest.IdTypeId)
            .ConfigureStronglyTypedId<IdTypeId>()
            .HasColumnName("IdTypeId")
            .IsRequired();

        builder.OwnsOne(guest => guest.GuestName, guestName =>
        {
            guestName.Property(personName => personName.FirstName)
                .HasColumnName("FirstName")
                .IsRequired();

            guestName.Property(personName => personName.MiddleName)
                .HasColumnName("MiddleName")
                .IsRequired(false);

            guestName.Property(personName => personName.LastName)
                .HasColumnName("LastName")
                .IsRequired();
        });

        builder.OwnsOne(guest => guest.GuestEmail, guestEmail =>
        {
            guestEmail.Property(emailAddress => emailAddress.Value)
                .HasColumnName("Email")
                .IsRequired();
        });

        builder.OwnsOne(guest => guest.GuestSimpleAddress, guestSimpleAddress =>
        {
            guestSimpleAddress.OwnsOne(simpleAddress => simpleAddress.Address, simpleAddress =>
            {
                simpleAddress.Property(address => address.AddressLine)
                    .HasColumnName("AddressLine")
                    .IsRequired();

                simpleAddress.Property(address => address.City)
                    .HasColumnName("City")
                    .IsRequired();
            });

            guestSimpleAddress.Property(address => address.Country)
                .HasColumnName("Country")
                .IsRequired();
        });

        builder.OwnsOne(guest => guest.IdNumber, idNumber =>
        {
            idNumber.Property(idNum => idNum.Value)
                .HasColumnName("IdNumber")
                .IsRequired()
                .HasMaxLength(50);
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
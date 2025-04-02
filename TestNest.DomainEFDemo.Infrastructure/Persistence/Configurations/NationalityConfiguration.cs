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

        builder.HasKey(n => n.Id)
            .IsClustered(false); // We want the NationalityName as the clustered index


        builder.Property(n => n.Id)
             .ConfigureStronglyTypedId<NationalityId>()
             .HasColumnName("NationalityId")
             .IsRequired();

        // ========================================================================
        // If we want to use the value object NationalityName 
        // in Nationality class, un-comment the following code below

        //builder.OwnsOne(nationality => nationality.NationalityName, nationalityName =>
        //{
        //    nationalityName.Property(nationalityName => nationalityName.Value)
        //        .HasColumnName("NationalityName")
        //        .HasColumnType("NVARCHAR")
        //        .HasMaxLength(100)  // Set the max length or modify as needed
        //        .IsRequired();
        //});


        // ========================================================================
        // And if we want, NationalityName to have an index,
        // un-comment the following code below

        //// Ensure the shadow property has the same max length and type
        //builder.Property<string>("NationalityName_Shadow")
        //    .HasColumnName("NationalityName")
        //    .HasColumnType("NVARCHAR") // Same type
        //    .HasMaxLength(100) // Same max length
        //    .IsRequired();

        //builder.HasIndex("NationalityName_Shadow")
        //    .HasDatabaseName("IX_Nationality_NationalityName")
        //    .IsUnique(true);
        // ========================================================================


        // ========================================================================
        // If we want to use the value object NationalityName 
        // in Nationality class, comment the following code below
        builder.Property(n => n.NationalityName)
           .HasColumnName("NationalityName")
           .HasColumnType("NVARCHAR")
           .HasMaxLength(100)  // Set the max length or modify as needed
           .IsRequired();

        builder.HasIndex(n => n.NationalityName)
            .HasDatabaseName("IX_Nationalities_NationalityName")
            .IsClustered() // If we want to make it clustered, we must remove other clustered indexes like the primary key (id) above
            .IsUnique(true);

    }
}
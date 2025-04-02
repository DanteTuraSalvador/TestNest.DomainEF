# 🚀 EF Core Demo with Strongly Typed IDs, Value Objects, and Smart Enums

This repository demonstrates an implementation of **Strongly Typed IDs**, **Value Objects**, **Smart Enums**, and **EF Core Migrations** in C#. It showcases how to build a rich domain model that ensures type safety, encapsulates business logic, and works seamlessly with EF Core for persistence.

## This implementation includes:

- ✅ **Strongly Typed IDs**: Custom ID types like `GuestId`, `NationalityId`, ensuring type safety and better expressiveness.
- ✅ **Value Objects**: Immutable objects that encapsulate domain logic, such as `PersonName`, `EmailAddress`, and `SimpleAddress`.
- ✅ **Smart Enums**: Enums with behavior and logic, like `GuestType`, which encapsulate specific domain-related functionality.
- ✅ **EF Core Migrations**: How to configure EF Core to properly persist these types in a relational database.

## Features

- 🔗 **Rich Domain Model**: Supports complex business logic with strongly typed IDs, value objects, and smart enums.
- ⚡ **Fluent Configuration**: Easy-to-use EF Core configuration to handle these types.
- 🛠️ **EF Core Migrations**: Handles migrations and schema updates for strongly typed IDs and value objects.
- 🛡️ **Type Safety**: Strongly typed IDs and value objects improve safety and readability of your domain models.
- 🧑‍💻 **Demo Project**: Includes example implementations for handling `Guest` entities and more.

## 📌 Core Implementation
See the Result pattern: [https://github.com/DanteTuraSalvador/TestNest.ResultPatterns](https://github.com/DanteTuraSalvador/TestNest.ResultPatterns)<br>
See the Result pattern: [https://github.com/DanteTuraSalvador/TestNest.ResultPatterns](https://github.com/DanteTuraSalvador/TestNest.ResultPatterns)<br>
See the Value Object: [https://github.com/DanteTuraSalvador/TestNest.ValueObjects](https://github.com/DanteTuraSalvador/TestNest.ValueObjects)<br>
See the StronglyType Id: [https://github.com/DanteTuraSalvador/TestNest.StronglyTypeId](https://github.com/DanteTuraSalvador/TestNest.StronglyTypeId)<br>

### Domain Model: Guest
```csharp
public sealed class Guest : BaseEntity<GuestId>
{
    public PersonName GuestName { get; }
    public EmailAddress GuestEmail { get; }
    public SimpleAddress GuestSimpleAddress { get; }
    public NationalityId NationalityId { get; }
    public IdTypeId IdTypeId { get; }
    public IdNumber IdNumber { get; }
    public GuestType GuestType { get; }

    private static readonly Lazy<Guest> _empty = new(() => new Guest());
    public static Guest Empty() => _empty.Value;

    private Guest() : base(GuestId.Empty())
        => (GuestName, GuestEmail, GuestSimpleAddress, NationalityId, IdTypeId, IdNumber, GuestType)
            = (PersonName.Empty(), EmailAddress.Empty(), SimpleAddress.Empty(),
               NationalityId.Empty(), IdTypeId.Empty(), IdNumber.Empty(), GuestType.None);

    // other method ommitted for brevity,
    // like the factory method Create and Update methods


}
```

### Domain Model: Nationality
```csharp
public sealed class Nationality : BaseEntity<NationalityId>
{
    public string NationalityName { get; }

    private static readonly Lazy<Nationality> _empty = new(()
        => new Nationality());

    public static Nationality Empty()
        => _empty.Value;

    private Nationality() : base(NationalityId.Empty())
        => NationalityName = string.Empty;

    public override string ToString()
        => NationalityName.ToString();

    // If we want to use the value object NationalityName
    // un-comment the following code below and comment the above code
    // make sure we have configure the Nationality configuration in Infrastructure
    // see NationalityConfiguration.cs

    //public NationalityName NationalityName { get; }

    //private static readonly Lazy<Nationality> _empty = new(() 
    //    => new Nationality());

    //public static Nationality Empty() 
    //    => _empty.Value;

    //private Nationality() : base(NationalityId.Empty()) 
    //    => NationalityName = NationalityName.Empty();

    //public override string ToString() 
    //    => NationalityName.ToString();


    // other method ommitted for brevity,
    // like the factory method Create and Update methods
}
```

### Value Objects: PersonName
```csharp
public sealed class PersonName : ValueObject
{
    private static readonly Lazy<PersonName> _empty = new(() => new PersonName());
    public static PersonName Empty() => _empty.Value;

    public string FirstName { get; }
    public string? MiddleName { get; }
    public string LastName { get; }

    private PersonName() 
        => (this.FirstName, this.MiddleName, this.LastName) 
            = (String.Empty, String.Empty, String.Empty);
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return this.FirstName;
        if (this.MiddleName is not null) yield return MiddleName;
        yield return this.LastName;
    }

    public override string ToString() => GetFullName();

    public string GetFullName()
    {
        return string.IsNullOrWhiteSpace(this.MiddleName)
            ? $"{this.FirstName} {this.LastName}"
            : $"{this.FirstName} {this.MiddleName} {this.LastName}";
    }

    // other method ommitted for brevity,
    // like the factory method Create and Update methods
}
```
### StronglyType Id: GuestId
```csharp
public sealed record GuestId(Guid Value) : StronglyTypedId<GuestId>(Value)
{
    public GuestId() : this(Guid.NewGuid()) { }

    public static GuestId Create(Guid value) 
        => value == Guid.Empty ? throw StronglyTypedIdException.NullId() 
            : new GuestId(value);

    public override string ToString() => Value.ToString();
}
```

### Value Objects: Address
```csharp
public sealed class Address : ValueObject
{
    public string AddressLine { get; }
    public string City { get; }
    
    private static readonly Lazy<Address> _empty = new(() => new Address());
    public static Address Empty() => _empty.Value;

    public Address()  => (AddressLine, City) = (string.Empty, string.Empty);

    protected override IEnumerable<object> GetAtomicValues() 
        => new object[] { AddressLine, City };

    public override string ToString() 
        => $"{AddressLine}, {City}";
    
    // other method ommitted for brevity,
    // like the factory method Create and Update methods   
}
```

### Value Objects: Simple Address (nested)
```csharp
public sealed class SimpleAddress : ValueObject
{
    public Address Address { get; }
    public string Country { get; }

    private static readonly Lazy<SimpleAddress> _empty = new(() => new SimpleAddress());
    public static SimpleAddress Empty() => _empty.Value;

    public SimpleAddress() => (Address, Country) = (Address.Empty(), string.Empty);

    protected override IEnumerable<object> GetAtomicValues() 
        => new object[] { Address, Country };

    public override string ToString() => $"{Address.AddressLine}, {Address.City}, {Country}";

    // other method ommitted for brevity,
    // like the factory method Create and Update methods
}
```
### Other Value Object 
  See the Source : [https://github.com/DanteTuraSalvador/TestNest.DomainEF/tree/master/TestNest.DomainEFDemo](https://github.com/DanteTuraSalvador/TestNest.DomainEF/tree/master/TestNest.DomainEFDemo)<br>

## 📌 EF Core Configuration
### ✅ Guest
This configuration maps the Guest entity to the Guests table in the database, using EF Core. It defines strongly typed IDs (GuestId, NationalityId, IdTypeId), value objects (GuestName, GuestEmail, GuestSimpleAddress, IdNumber), and a smart enum (GuestType). Each property is configured with specific column types, constraints, and relationships. The configuration also sets up foreign key relationships with Nationality and Identification entities, with restricted delete behavior. Overall, it ensures a robust, type-safe mapping of the domain model to the database.
```csharp
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

```

### ✅ Nationality
This configuration maps the Nationality entity to the Nationalities table. It sets up a primary key on the Id property, specifying that it should not be clustered, as the NationalityName is the clustered index. The Id is configured as a strongly typed ID (NationalityId). The NationalityName is mapped as a regular column (NVARCHAR type, max length 100) with an index created on it. There's an option to use a value object (NationalityName), and if desired, it can be indexed with a unique constraint and made clustered. If the value object is not used, the NationalityName property is directly mapped, and the index is applied. The configuration allows flexibility in how NationalityName is handled, either as a value object or as a regular property.
```csharp
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
```

### ✅ Identification
This configuration maps the Identification entity to the Identifications table. It sets up the primary key on the Id property, ensuring it is not clustered. The Id is a strongly typed ID (IdTypeId). The IdTypeName is configured as a value object and is mapped to a column (NVARCHAR type, max length 100). Additionally, a shadow property IdTypeName_Shadow is created for the IdTypeName, ensuring the same column type and max length, and an index is applied to it with a clustered index. This configuration allows flexible mapping of the IdTypeName value object while ensuring indexing for efficient lookups
```csharp
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
```
### ✅ EF Core Migration
Once you've set up your domain model and configurations, create and apply the EF Core migrations:
```csharp
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## 📌 Example Usage
### ✅ Guest 
```csharp
// Assumes that all value object has its own Create factory object that validates its input

var guestName = PersonName.Create("John", null, "Doe"); // PersonName.Create() creates a new PersonName (with optional middle name)
var guestEmail = EmailAddress.Create("john.doe@example.com"); // EmailAddress.Create() creates a new EmailAddress

// First, create the Address
var address = new Address("123 Main St", "City"); // Create Address object
var guestAddress = SimpleAddress.Create(address, "Country"); // SimpleAddress.Create() with Address and country

var nationalityId = NationalityId.Empty(); // Assuming NationalityId is an existing value (not new)
var idTypeId = IdTypeId.Empty(); // Assuming IdTypeId is an existing value (not new)
var idNumber = IdNumber.Create("AB123456"); // IdNumber.Create() creates a new IdNumber
var guestType = GuestType.Primary; // Assuming GuestType.Primary is a predefined smart enum value

// Now create the Guest object
// Guest.Create method create its own Id
var guest = Guest.Create(
    guestName,        // Pass the pre-created PersonName
    guestEmail,       // Pass the pre-created EmailAddress
    guestAddress,     // Pass the pre-created SimpleAddress
    nationalityId,    // Pass the pre-existing NationalityId
    idTypeId,         // Pass the pre-existing IdTypeId
    idNumber,         // Pass the pre-created IdNumber
    guestType         // Pass the pre-created GuestType
);
```
## 📜 License

This project is open-source and free to use.



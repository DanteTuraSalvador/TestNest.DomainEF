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
  See the Result pattern: [https://github.com/DanteTuraSalvador/TestNest.DomainEF/tree/master/TestNest.DomainEFDemo](https://github.com/DanteTuraSalvador/TestNest.DomainEF/tree/master/TestNest.DomainEFDemo)<br>

## 📌 Usage Examples
### ✅ Result Class
The Result class is used when there is no return value but you still want to communicate whether an operation succeeded or failed. It also carries any relevant error information in case of failure.
```csharp
var result = Result.Success(); // Indicates success
var errorResult = Result.Failure(ErrorType.Validation, "ERR001", "Invalid input"); // Indicates failure with error
```

### ✅ Result<T> Class
The Result<T> class is a generic version that wraps a successful result with a value of type T. If the operation fails, it contains error information, similar to the Result class.
```csharp
var result = Result<int>.Success(42); // Success with value
var errorResult = Result<int>.Failure(ErrorType.Validation, "ERR002", "Invalid data"); // Failure with error
```

### ✅ Fluent Chaining with Bind and Map
You can chain operations using Bind and Map methods, which allow you to transform or pass the result through multiple stages. If any stage fails, the chain stops immediately, and the failure result is returned.
```csharp
var result = Result<int>.Success(42)
    .Bind(x => Result<int>.Success(x + 1)) // Adds 1 to the result
    .Map(x => x * 2); // Multiplies the result by 2
```
### ✅ Error Handling
The error handling is done using the Error class, which stores error codes and messages. You can check for errors using IsSuccess and handle them accordingly.
```csharp
var result = Result<int>.Failure(ErrorType.Validation, "ERR003", "Out of bounds");
if (!result.IsSuccess)
{
    Console.WriteLine($"Error: {result.Errors.First().Message}");
}
```

## 📌 Implementing Value Objects and Concrete Class
### ✅ Price Value Object 
```csharp
public sealed class Price : ValueObject
{
    private static readonly Lazy<Price> _lazyEmpty = new(() => new Price());
    private static readonly Lazy<Price> _lazyZero = new(() => new Price(0, 0)); // ✅ Fix Zero Initialization

    public static Price Empty => _lazyEmpty.Value;
    public static Price Zero => _lazyZero.Value;

    public decimal StandardPrice { get; }
    public decimal PeakPrice { get; }
    public Currency Currency => Currency.PHP; // Fixed to PHP

    private Price() => (StandardPrice, PeakPrice) = (0, 0);
    private Price(decimal standardPrice, decimal peakPrice)  => (StandardPrice, PeakPrice) = (standardPrice, peakPrice);

    public static Result<Price> Create(decimal standardPrice, decimal peakPrice)
    {
        var errors = new List<Error>();

        if (standardPrice < 0)
        {
            var exception = PriceException.NegativeStandardPrice();
            errors.Add(new Error(exception.Code.ToString(), exception.Message));
        }

        if (peakPrice < 0)
        {
            var exception = PriceException.NegativePeakPrice();
            errors.Add(new Error(exception.Code.ToString(), exception.Message));
        }

        if (standardPrice >= 0 && peakPrice >= 0 && peakPrice < standardPrice)
        {
            var exception = PriceException.PeakBelowStandard();
            errors.Add(new Error(exception.Code.ToString(), exception.Message));
        }

        if (errors.Any())
        {
            return Result<Price>.Failure(ErrorType.Validation, errors);
        }

        return Result<Price>.Success(new Price(standardPrice, peakPrice));
    }

    public Result<Price> WithStandardPrice(decimal newStandardPrice) => Create(newStandardPrice, PeakPrice);
    public Result<Price> WithPeakPrice(decimal newPeakPrice) => Create(StandardPrice, newPeakPrice);

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return StandardPrice;
        yield return PeakPrice;
        yield return Currency;
    }

    public override string ToString() => $"{Currency.Symbol}{StandardPrice:F2} / {Currency.Symbol}{PeakPrice:F2} (Peak)";
}
```
### ✅ AccommodationPrice Value Object
```csharp
public sealed class AccommodationPrice : ValueObject
{
    public Price Price { get; }
    public decimal CleaningFee { get; }

    private static readonly Lazy<AccommodationPrice> _empty = new(() => new AccommodationPrice(Price.Empty, 0));
    private static readonly Lazy<AccommodationPrice> _zero = new(() => new AccommodationPrice(Price.Zero, 0));

    public static AccommodationPrice Empty => _empty.Value;
    public static AccommodationPrice Zero => _zero.Value;

    private AccommodationPrice() => (Price, CleaningFee) = (Price.Empty, 0);

    private AccommodationPrice(Price price, decimal cleaningFee)
        => (Price, CleaningFee) = (price, cleaningFee);

    public static Result<AccommodationPrice> Create(decimal standardPrice, decimal peakPrice, decimal cleaningFee)
        => Price.Create(standardPrice, peakPrice).Bind(price => Create(price, cleaningFee));

    public static Result<AccommodationPrice> Create(Price? price, decimal cleaningFee)
    {
        var errors = new List<Error>();
        
        if (price == null)
        {
            var exception = AccommodationPriceException.NullPrice();
            errors.Add(new Error(exception.Code.ToString(), exception.Message.ToString()));
        }
        if (price == Price.Empty)
        {
            var exception = AccommodationPriceException.NullPrice();
            errors.Add(new Error(exception.Code.ToString(), exception.Message.ToString()));
        }
        if (cleaningFee < 0)
        {
            var exception = AccommodationPriceException.NegativeCleaningFee();
            errors.Add(new Error(exception.Code.ToString(), exception.Message.ToString()));
        }

        if (errors.Any())
        {
            return Result<AccommodationPrice>.Failure(ErrorType.Validation, errors);
        }

        return Result<AccommodationPrice>.Success(new AccommodationPrice(price!, cleaningFee));
    }

    public static Result<AccommodationPrice> Create(Result<Price> priceResult, decimal cleaningFee)
        => !priceResult.IsSuccess ? Result<AccommodationPrice>.Failure(priceResult.ErrorType, priceResult.Errors)
            : Create(priceResult.Value!, cleaningFee);

    public Result<AccommodationPrice> WithPrice(Price newPrice)
    {
        if (this == Empty)
        {
            var exception = AccommodationPriceException.CannotModifyEmpty();
            return Result<AccommodationPrice>.Failure(ErrorType.Validation, new Error(exception.Code.ToString(), exception.Message));
        }

        if (newPrice is null)
        {
            var exception = AccommodationPriceException.NullPrice();
            return Result<AccommodationPrice>.Failure(ErrorType.Validation, new Error(exception.Code.ToString(), exception.Message));
        }

        return Create(newPrice, CleaningFee);
    }

    public Result<AccommodationPrice> WithPrice(Result<Price> newPriceResult)
        => newPriceResult.IsSuccess ? Create(newPriceResult.Value!, CleaningFee)
            : Result<AccommodationPrice>.Failure(newPriceResult.ErrorType, newPriceResult.Errors);

    public Result<AccommodationPrice> WithCleaningFee(decimal newCleaningFee)
    {
        if (this == Empty)
        {
            var exception = AccommodationPriceException.CannotModifyEmpty();
            return Result<AccommodationPrice>.Failure(ErrorType.Validation, new Error(exception.Code.ToString(), exception.Message));
        }

        if (newCleaningFee < 0)
        {
            var exception = AccommodationPriceException.NegativeCleaningFee();
            return Result<AccommodationPrice>.Failure(ErrorType.Validation, new Error(exception.Code.ToString(), exception.Message));
        }

        return Create(Price, newCleaningFee);
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Price;
        yield return CleaningFee;
    }

    public override string ToString() =>
        $"{Price} + Cleaning Fee: {Price.Currency.Symbol}{CleaningFee:F2}";
}
```
### ✅ EstablishmentAccomodationPrice Concrete Class
```csharp
public sealed class EstablishmentAccommodation
{
    public AccommodationPrice Price { get; }

    private static readonly Lazy<EstablishmentAccommodation> _empty = new(() => new EstablishmentAccommodation());
    public static EstablishmentAccommodation Empty => _empty.Value;

    private EstablishmentAccommodation() => Price = AccommodationPrice.Empty;

    private EstablishmentAccommodation(AccommodationPrice price) => Price = price;

    public static Result<EstablishmentAccommodation> Create(Result<AccommodationPrice> priceResult)
    {
        if (priceResult.IsSuccess)
        {
            return Result<EstablishmentAccommodation>
                .Success(new EstablishmentAccommodation(priceResult.Value!));
        }

        return Result<EstablishmentAccommodation>
            .Failure(priceResult.ErrorType, priceResult.Errors);
    }


    public static Result<EstablishmentAccommodation> Create(AccommodationPrice price)
    {
        if (price == AccommodationPrice.Empty)
        {
            return Result<EstablishmentAccommodation>
                .Failure(ErrorType.Validation, 
                    new Error(EstablishmentAccommodationException.InvalidAccommodationPrice().Code.ToString(),
                              EstablishmentAccommodationException.InvalidAccommodationPrice().Message));
        }

        return Result<EstablishmentAccommodation>
            .Success(new EstablishmentAccommodation(price));
    }


    public Result<EstablishmentAccommodation> UpdatePrice(AccommodationPrice newPrice)
    {
        if (newPrice == AccommodationPrice.Empty)
        {
            return Result<EstablishmentAccommodation>
                .Failure(ErrorType.Validation, 
                    new Error(EstablishmentAccommodationException.InvalidAccommodationPrice().Code.ToString(),
                              EstablishmentAccommodationException.InvalidAccommodationPrice().Message));
        }

        return Result<EstablishmentAccommodation>
            .Success(new EstablishmentAccommodation(newPrice));
    }


    public Result<EstablishmentAccommodation> UpdatePrice(Result<AccommodationPrice> newPriceResult)
    {
        if (newPriceResult.IsSuccess)
        {
            return Result<EstablishmentAccommodation>
                .Success(new EstablishmentAccommodation(newPriceResult.Value!));
        }

        return Result<EstablishmentAccommodation>
            .Failure(newPriceResult.ErrorType, newPriceResult.Errors);
    }
}
```
## 📜 License

This project is open-source and free to use.



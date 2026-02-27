using Inventory.Core.Exceptions;

namespace Inventory.Core.ValueObjects;

public class Price : IEquatable<Price>
{
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }

    private Price() { } // For EF Core

    public Price(decimal amount, string currency = "USD")
    {
        if (amount < 0)
            throw new DomainException("Price cannot be negative");

        if (string.IsNullOrWhiteSpace(currency))
            throw new DomainException("Currency cannot be empty");

        Amount = Math.Round(amount, 2);
        Currency = currency.ToUpperInvariant();
    }

    public Price Add(Price other)
    {
        if (other.Currency != Currency)
            throw new DomainException("Cannot add prices with different currencies");

        return new Price(Amount + other.Amount, Currency);
    }

    public Price Subtract(Price other)
    {
        if (other.Currency != Currency)
            throw new DomainException("Cannot subtract prices with different currencies");

        return new Price(Amount - other.Amount, Currency);
    }

    public Price Multiply(decimal multiplier)
    {
        return new Price(Amount * multiplier, Currency);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Price);
    }

    public bool Equals(Price? other)
    {
        if (other is null) return false;
        return Amount == other.Amount && Currency == other.Currency;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Amount, Currency);
    }

    public override string ToString()
    {
        return $"{Currency} {Amount:F2}";
    }
}
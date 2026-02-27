using Inventory.Core.Enums;
using Inventory.Core.Exceptions;
using Inventory.Core.ValueObjects;

namespace Inventory.Core.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Sku { get; private set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Price Price { get; private set; } = null!;
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }
    public ProductStatus Status { get; private set; }
    public int MinimumStock { get; set; }
    public int MaximumStock { get; set; }

    private Product() { } // For EF Core

    public Product(string name, string sku, Price price, int categoryId)
    {
        SetName(name);
        SetSku(sku);
        SetPrice(price);
        CategoryId = categoryId;
        Status = ProductStatus.Active;
        CreatedAt = DateTime.UtcNow;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Product name cannot be empty");

        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }

    private void SetSku(string sku)
    {
        if (string.IsNullOrWhiteSpace(sku))
            throw new DomainException("SKU cannot be empty");

        if (sku.Length < 5)
            throw new DomainException("SKU must be at least 5 characters");

        Sku = sku.ToUpperInvariant();
    }

    public void SetPrice(Price price)
    {
        Price = price ?? throw new DomainException("Price cannot be null");
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatePrice(decimal newAmount, string currency = "USD")
    {
        Price = new Price(newAmount, currency);
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        Status = ProductStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        Status = ProductStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Discontinue()
    {
        Status = ProductStatus.Discontinued;
        UpdatedAt = DateTime.UtcNow;
    }
}
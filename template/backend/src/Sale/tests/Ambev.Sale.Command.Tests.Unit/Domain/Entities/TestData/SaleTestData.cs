using Ambev.Sale.Command.Application.Dto;
using Ambev.Sale.Command.Application.Sale.Create;
using Ambev.Sale.Command.Domain.Enum;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;


public static class SaleTestData
{

    private static readonly Faker<Ambev.Sale.Command.Domain.Entities.SaleItem> SaleItemFaker = new Faker<Ambev.Sale.Command.Domain.Entities.SaleItem>()
        .CustomInstantiator(f => new Ambev.Sale.Command.Domain.Entities.SaleItem(            
            f.Random.Guid().ToString(), // ProductId
            f.Commerce.ProductName(), // ProductName
            f.Random.Int(1, 5), // Quantity
            f.Random.Decimal(1, 100), // UnitPrice
            SaleItemStatus.NotCancelled // Default value for Status
        ));

    private static readonly Faker<Ambev.Sale.Command.Domain.Entities.Sale> SaleFaker = new Faker<Ambev.Sale.Command.Domain.Entities.Sale>()
    .RuleFor(u => u.CustomerId, f => f.Random.Guid().ToString())
    .RuleFor(u => u.CustomerName, f => f.Person.UserName)
    .RuleFor(u => u.BranchId, f => f.Random.Guid().ToString())
    .RuleFor(u => u.BranchName, f => f.Person.UserName)
    .RuleFor(u => u.SaleItens, f => SaleItemFaker.Generate(f.Random.Int(1, 5))); // genera 1 / 5 itens

    public static Ambev.Sale.Command.Domain.Entities.Sale GenerateValidSale()
    {
        return SaleFaker.Generate();
    }
}

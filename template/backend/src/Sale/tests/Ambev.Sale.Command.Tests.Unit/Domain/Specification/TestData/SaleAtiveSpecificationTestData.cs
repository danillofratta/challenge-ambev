using Ambev.Sale.Command.Application.Sale.Create;
using Ambev.Sale.Command.Domain.Enum;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications.TestData;


public static class SaleAtiveSpecificationTestData
{
    private static readonly Faker<Ambev.Sale.Command.Domain.Entities.Sale> saleFaker = new Faker<Ambev.Sale.Command.Domain.Entities.Sale>()
        .RuleFor(u => u.CustomerId, f => f.Random.Guid().ToString())
        .RuleFor(u => u.CustomerName, f => f.Person.UserName)
        .RuleFor(u => u.BranchId, f => f.Random.Guid().ToString())
        .RuleFor(u => u.BranchName, f => f.Person.UserName);

    public static Ambev.Sale.Command.Domain.Entities.Sale GenerateUser(SaleStatus status)
    {
        var user = saleFaker.Generate();
        user.Status = status;
        return user;
    }

}

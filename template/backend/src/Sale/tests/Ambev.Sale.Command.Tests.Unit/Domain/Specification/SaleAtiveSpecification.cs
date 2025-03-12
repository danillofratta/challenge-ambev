using Ambev.DeveloperEvaluation.Unit.Domain.Specifications.TestData;
using Ambev.Sale.Command.Domain.Enum;
using Ambev.Sale.Command.Domain.Specification;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Sale.Command.Tests.Unit.Domain.Specification
{
    public class SaleAtiveSpecification
    {
        [Theory]
        [InlineData(SaleStatus.NotCancelled, true)]
        [InlineData(SaleStatus.Cancelled, false)]
        public void IsSatisfiedBy_ShouldValidateUserStatus(SaleStatus status, bool expectedResult)
        {
            // Arrange
            var user = SaleAtiveSpecificationTestData.GenerateUser(status);
            var specification = new SaleActiveSpecification();

            // Act
            var result = specification.IsSatisfiedBy(user);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}

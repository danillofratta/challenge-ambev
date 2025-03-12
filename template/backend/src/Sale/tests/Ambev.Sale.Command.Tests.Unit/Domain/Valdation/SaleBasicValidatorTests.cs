using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Ambev.Sale.Command.Domain.Validation;
using FluentValidation.TestHelper;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Sale.Command.Tests.Unit.Domain.Valdation
{
    public class SaleBasicValidatorTests
    {
        private readonly SaleBasicValidator _validator;

        public SaleBasicValidatorTests()
        {
            _validator = new SaleBasicValidator();
        }

        [Fact(DisplayName = "Require: Price must be greater than zero")]
        public void Given_Price_MustBe_Greater_Than_Zero_ShouldReturnInvalid()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.TotalAmount = 0;

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }
    }
}

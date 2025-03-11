﻿using Ambev.Sale.Command.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Sale.Command.Application.Dto
{
    public record UpdateSaleItemDto
    (
        Guid SaleId,
        Guid Id,
        string ProductId,
        string ProductName,
        int Quantity,
        decimal UnitPrice,
        decimal Discount,
        decimal TotalPrice,
        SaleStatus Status
    );
}

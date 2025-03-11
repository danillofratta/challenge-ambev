using Ambev.Sale.Contracts.Dto;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace Ambev.Sale.Contracts.Events
{
    public class SaleCanceledEvent
    {
        public Guid Id { get; set; }
    }
}

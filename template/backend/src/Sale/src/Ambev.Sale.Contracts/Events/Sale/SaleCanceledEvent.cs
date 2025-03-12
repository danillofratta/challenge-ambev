using Ambev.Sale.Contracts.Dto;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace Ambev.Sale.Contracts.Events
{
    /// <summary>
    /// Event responsável por sincronizar bando de dado write e read
    /// </summary>
    public class SaleCanceledEvent
    {
        public Guid Id { get; set; }
    }
}

namespace Ambev.Sale.Contracts.Events
{
    /// <summary>
    /// Event responsável por sincronizar bando de dado write e read
    /// </summary>
    public class SaleDeletedEvent  
    {
        public Guid Id { get; set; }
    }
}

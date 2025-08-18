namespace MonolithOrderSystem.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;


        public string? UserName { get; set; }
        public string? ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
    }
    
    public record CreateOrderRequest(
    [property: Range(1, int.MaxValue)] int UserId,
    [property: Range(1, int.MaxValue)] int ProductId,
    [property: Range(1, int.MaxValue)] int Quantity
);
}
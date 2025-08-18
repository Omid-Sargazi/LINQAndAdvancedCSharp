namespace MonolithOrderSystem.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, MinLength(2)]
        public string Name { get; set; } = "";

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    }

    public record CreateProductRequest(
    [property: Required, MinLength(2)] string Name,
    [property: Range(0, double.MaxValue)] decimal Price
);



}
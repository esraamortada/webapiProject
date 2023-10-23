namespace Anadolu.DTO
{
    public class CartProductDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }
        public string ProductImage { get; set; }
    }
}

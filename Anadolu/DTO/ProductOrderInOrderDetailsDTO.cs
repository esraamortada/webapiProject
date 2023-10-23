namespace Anadolu.DTO
{
    public class ProductOrderInOrderDetailsDTO
    {
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal ProductOrderTotalPrice { get; set; }
    }
}
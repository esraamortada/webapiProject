namespace Anadolu.DTO
{
    public class ProductWithDiscountDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double DiscountValue { get; set; }
        public DateTime? EndDate { get; set; }
        public float ProductPrice { get; set; }
        public string Image { get; set; }
        public decimal ProductPriceAfterDiscount { get; set; }
    }
}

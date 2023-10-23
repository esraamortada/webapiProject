namespace Anadolu.DTO
{
    public class ReturnDiscountDTO
    {
        public int ProdutId { get; set; }

        public DateTime? DiscountStartDate { get; set; }

        public double DiscountValue { get; set; }

        public DateTime DiscountEndDate { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPriceAfterDiscount { get; set; }

        public float ProductPrice { get; set; }
    }
}

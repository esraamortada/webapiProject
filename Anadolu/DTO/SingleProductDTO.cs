namespace Anadolu.DTO
{
    public class SingleProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPriceAfterDiscount { get; set; }
    }
}

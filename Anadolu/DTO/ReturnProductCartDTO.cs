namespace Anadolu.DTO
{
    public class ReturnProductCartDTO
    {
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

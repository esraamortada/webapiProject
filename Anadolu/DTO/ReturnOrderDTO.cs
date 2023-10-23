namespace Anadolu.DTO
{
    public class ReturnOrderDTO
    {
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public int TotalPrice { get; set; }
        public string UserId { get; set; }
        public int OrderId { get; set; }
    }
}

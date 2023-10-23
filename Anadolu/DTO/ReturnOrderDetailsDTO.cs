namespace Anadolu.DTO
{
    public class ReturnOrderDetailsDTO
    {
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public string PhoneNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatusName { get; set; }

        public List<ProductOrderInOrderDetailsDTO> ProductOrderInOrderDetails { get; set; }
    }
}

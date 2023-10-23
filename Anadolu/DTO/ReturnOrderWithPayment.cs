using System.ComponentModel.DataAnnotations;

namespace Anadolu.DTO
{
    public class ReturnOrderWithPayment
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public decimal? TotalPrice { get; set; }
        public int OrderStatusId { get; set; }
        public string UserId { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}

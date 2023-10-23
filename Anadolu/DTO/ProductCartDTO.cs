using Anadolu.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anadolu.DTO
{
    public class ProductCartDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string CartId { get; set; }
    }
}

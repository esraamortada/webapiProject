using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anadolu.Models
{
    // الادمن المفروض يضيف ديسكاوند علي البرودكت اللي
    // هيختاره من الليست اللي هتظهرله
    public class Discount
    {
    
        [Key]
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public DateTime StartDate { get; set; }

        public double Value { get; set; }

        public DateTime EndDate { get; set; }

        public decimal ProductPriceAfterDiscount { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
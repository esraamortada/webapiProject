using Anadolu.Models;

namespace Anadolu.DTO
{
    public class UpdateAllProductCartsDTO
    {
        public string CartId { get; set; }
        public List<ProductCartDTO> ProductCartDTOs { get; set; }
    }
}

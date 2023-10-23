namespace Anadolu.DTO
{
    public class ReturnProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int? Quantity { get; set; }
        public string Description { get; set; }
        public int SubCategoryId { get; set; }
        public string? ImagePath { get; set; }
    }
}

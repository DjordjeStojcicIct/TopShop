namespace TopShop.Application.UseCases.DTO.QueryDTO
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int StockCount { get; set; }
        public string? ProductImageFile { get; set; }
    }
}

namespace TopShop.Application.UseCases.DTO.CreateDTO
{
    public class CreateOrderItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get { return UnitPrice * Quantity; } }
    }
}

using TopShop.Application.UseCases.DTO.QueryDTO;

namespace TopShop.Application.UseCases.DTO.CreateDTO
{
    public class CreateOrderDTO
    {
        public AddressDTO ShippingAddress { get; set; }
        public ICollection<CreateOrderItemDTO> OrderItems { get; set; } = new HashSet<CreateOrderItemDTO>();
    }
}

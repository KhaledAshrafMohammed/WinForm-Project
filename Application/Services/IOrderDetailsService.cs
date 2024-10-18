namespace ELDOKKAN.Application.Services;
public interface IOrderDetailsService
{
    GetAllOrderDetailsDTO GetOrderDetailsById(int OrderDetailsId);
    IEnumerable<GetAllOrderDetailsDTO> GetAllOrderDetails();
    GetAllOrderDetailsDTO AddOrderDetails(CreateOrderDetailsDTO createOrderDetailsDto);
    bool UpdateOrderDetails(int OrderDetailsId, UpdateOrderDetailsDTO updateOrderDetailsDto);
    bool DeleteOrderDetails(int OrderDetailsId);
} 
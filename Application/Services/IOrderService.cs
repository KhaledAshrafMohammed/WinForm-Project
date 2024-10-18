namespace ELDOKKAN.Application.Services;
public interface IOrderService
{
    GetAllOrderDTO GetOrderById(int orderId);
    IEnumerable<GetAllOrderDTO> GetAllOrders();
    GetAllOrderDTO AddOrder(CreateOrderDTO createOrderDto);
    bool UpdateOrder(int orderId, UpdateOrderDTO updateOrderDto);
    bool DeleteOrder(int orderId);
} 
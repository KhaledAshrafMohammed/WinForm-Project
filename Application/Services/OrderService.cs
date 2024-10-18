namespace ELDOKKAN.Application.Services;
public class OrderService : IOrderService
{
    private readonly IOrderRepository repository;
    private readonly IMapper mapper;
    public OrderService(IOrderRepository _repository, IMapper _mapper)
    {
        repository = _repository;
        mapper = _mapper;
    }

    public GetAllOrderDTO AddOrder(CreateOrderDTO createOrderDto)
    {
        Order Order = mapper.Map<Order>(createOrderDto);
        if(Order == null)
            return null!;
        repository.Add(Order);
        repository.SaveChanges();
        return mapper.Map<GetAllOrderDTO>(Order);
    }

    public bool DeleteOrder(int OrderId)
    {
        repository.Delete(OrderId);
        return repository.SaveChanges() == 1;
    }

    public GetAllOrderDTO GetOrderById(int OrderId)
    {
        Order Order = repository.GetById(OrderId);
        GetAllOrderDTO getAllOrderDto = mapper.Map<GetAllOrderDTO>(Order);
        return getAllOrderDto;
    }

    public IEnumerable<GetAllOrderDTO> GetAllOrders()
    {
        return repository.GetAll().Select(ad => mapper.Map<GetAllOrderDTO>(ad));
    }
    public bool UpdateOrder(int orderId, UpdateOrderDTO updateOrderDTO)
    {
        Order order = repository.GetById(orderId);

        if(order == null)
            return false;

        order.CustomerID = updateOrderDTO.CustomerID;
        return repository.SaveChanges() == 1;
    }
}
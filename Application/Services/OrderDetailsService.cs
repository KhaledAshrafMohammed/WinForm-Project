namespace ELDOKKAN.Application.Services;
public class OrderDetailsService : IOrderDetailsService
{
    private readonly IOrderDetailsRepository repository;
    private readonly IMapper mapper;
    public OrderDetailsService(IOrderDetailsRepository _repository, IMapper _mapper)
    {
        repository = _repository;
        mapper = _mapper;
    }

    public GetAllOrderDetailsDTO AddOrderDetails(CreateOrderDetailsDTO createOrderDetailsDto)
    {
        OrderDetails OrderDetails = mapper.Map<OrderDetails>(createOrderDetailsDto);
        if(OrderDetails == null)
            return null!;
        repository.Add(OrderDetails);
        repository.SaveChanges();
        return mapper.Map<GetAllOrderDetailsDTO>(OrderDetails);
    }

    public bool DeleteOrderDetails(int OrderDetailsId)
    {
        repository.Delete(OrderDetailsId);
        return repository.SaveChanges() == 1;
    }

    public GetAllOrderDetailsDTO GetOrderDetailsById(int OrderDetailsId)
    {
        OrderDetails OrderDetails = repository.GetById(OrderDetailsId);
        GetAllOrderDetailsDTO getAllOrderDetailsDto = mapper.Map<GetAllOrderDetailsDTO>(OrderDetails);
        return getAllOrderDetailsDto;
    }

    public IEnumerable<GetAllOrderDetailsDTO> GetAllOrderDetails()
    {
        return repository.GetAll().Select(ad => mapper.Map<GetAllOrderDetailsDTO>(ad));
    }
    public bool UpdateOrderDetails(int OrderDetailsId, UpdateOrderDetailsDTO updateOrderDetailsDTO)
    {
        OrderDetails OrderDetails = repository.GetById(OrderDetailsId);

        if(OrderDetails == null)
            return false;

        OrderDetails.Quantity = updateOrderDetailsDTO.Quantity;
        OrderDetails.Discount = updateOrderDetailsDTO.Discount;
        OrderDetails.OrderStatus = updateOrderDetailsDTO.OrderStatus;

        return repository.SaveChanges() == 1;
    }
}
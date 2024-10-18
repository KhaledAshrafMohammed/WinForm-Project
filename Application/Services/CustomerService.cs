namespace ELDOKKAN.Application.Services;
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository repository;
    private readonly IMapper mapper;
    public CustomerService(ICustomerRepository _repository, IMapper _mapper)
    {
        repository = _repository;
        mapper = _mapper;
    }

    public GetAllCustomerDTO AddCustomer(CreateCustomerDTO createCustomerDto)
    {
        Customer customer = mapper.Map<Customer>(createCustomerDto);
        if (customer == null)
            return null!;
        repository.Add(customer);
        repository.SaveChanges();
        return mapper.Map<GetAllCustomerDTO>(customer);
    }

    public bool DeleteCustomer(int CustomerId)
    {
        repository.Delete(CustomerId);
        return repository.SaveChanges() == 1;
    }

    public GetAllCustomerDTO GetCustomerById(int customerId)
    {
        Customer customer = repository.GetById(customerId);
        GetAllCustomerDTO getAllCustomerDto = mapper.Map<GetAllCustomerDTO>(customer);
        return getAllCustomerDto;
    }

    public IEnumerable<GetAllCustomerDTO> GetAllCustomers()
    {
        return repository.GetAll().Select(ad => mapper.Map<GetAllCustomerDTO>(ad));
    }
    public bool UpdateCustomer(int CustomerId, GetAllCustomerDTO updateCustomerDTO)
    {
        Customer customer = repository.GetById(CustomerId);
        if(customer == null)
            return false;
        customer.Name = updateCustomerDTO.Name;
        customer.Address = updateCustomerDTO.Address;
        customer.PostalCode = updateCustomerDTO.PostalCode;
        customer.Phone = updateCustomerDTO.Phone; 
        customer.Password = updateCustomerDTO.Password; 
        return repository.SaveChanges() == 1;
    }
}
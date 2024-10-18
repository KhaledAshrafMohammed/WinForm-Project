namespace ELDOKKAN.Application.DTOs.Order;
#nullable disable
public class CreateOrderDTO
{
    public string CustomerID { get; set; }
    public DateTime? OrderDate { get; set; }
}
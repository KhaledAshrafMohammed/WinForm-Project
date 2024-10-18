namespace ELDOKKAN.Application.DTOs.Order;
#nullable disable
public class GetAllOrderDTO 
{
    public int OrderID { get; set; }
    public string CustomerID { get; set; }
    public DateTime? OrderDate { get; set; }
}
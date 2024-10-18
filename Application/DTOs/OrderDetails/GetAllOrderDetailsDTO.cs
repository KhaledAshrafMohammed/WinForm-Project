namespace ELDOKKAN.Application.DTOs.OrderDetails;
#nullable disable
public class GetAllOrderDetailsDTO 
{
    public int OrderID { get; set; }
    public int ProductID { get; set; }
    public short Quantity { get; set; }
    public float Discount { get; set; }
    public OrderStatus OrderStatus { get; set; }
}
namespace ELDOKKAN.Application.DTOs.OrderDetails;
#nullable disable
public class UpdateOrderDetailsDTO
{
    public short Quantity { get; set; }
    public float Discount { get; set; }
    public OrderStatus OrderStatus { get; set; }
}
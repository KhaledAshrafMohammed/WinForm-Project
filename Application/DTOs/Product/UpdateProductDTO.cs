namespace ELDOKKAN.Application.DTOs.Customer;
#nullable disable
public class UpdateProductDTO
{
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
}
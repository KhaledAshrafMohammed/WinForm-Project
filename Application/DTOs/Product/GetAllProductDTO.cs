namespace ELDOKKAN.Application.DTOs.Customer;
#nullable disable
public class GetAllProductDTO 
{
    public int ProductID { get; set; }
    public string Name { get; set; }
    public int? CategoryID { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
}
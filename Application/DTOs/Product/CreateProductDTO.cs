namespace ELDOKKAN.Application.DTOs.Product;
#nullable disable
public class CreateProductDTO
{
    public string Name { get; set; }
    public int? CategoryID { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
}
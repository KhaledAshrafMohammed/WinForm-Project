namespace ELDOKKAN.Models;
#nullable disable
public class Customer
{
    public int CustomerID { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    // navigation properties
    public virtual ICollection<Order> Orders { get; set; }
}
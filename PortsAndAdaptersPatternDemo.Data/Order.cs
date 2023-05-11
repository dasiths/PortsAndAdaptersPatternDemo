using System.ComponentModel.DataAnnotations;

namespace PortsAndAdaptersPatternDemo.Data;

public class Order
{
    [Key]
    public int OrderId { get; set; }
    public string Customer { get; set; }
    public List<Product> Products { get; set; }
}
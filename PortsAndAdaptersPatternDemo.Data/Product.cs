using System.ComponentModel.DataAnnotations;

namespace PortsAndAdaptersPatternDemo.Data;

public class Product
{
    [Key]
    public int ProductId { get; set; }
    public string Name { get; set; }
    public List<Order> Orders { get; set; }
}
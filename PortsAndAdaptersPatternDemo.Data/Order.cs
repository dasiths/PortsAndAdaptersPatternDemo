namespace PortsAndAdaptersPatternDemo.Data;

public class Order
{
    public int OrderId { get; set; }
    public string Customer { get; set; }
    public List<Product> Products { get; set; }
}
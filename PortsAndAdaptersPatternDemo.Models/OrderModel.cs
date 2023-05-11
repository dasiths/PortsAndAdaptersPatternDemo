namespace PortsAndAdaptersPatternDemo.Models;

public class OrderModel
{
    public int OrderId { get; set; }
    public string Customer { get; set; }
    public List<ProductModel> Products { get; set; }
}
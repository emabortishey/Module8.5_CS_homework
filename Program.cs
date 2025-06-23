using System.Xml;
using static System.Console;

public class Product
{
    public string Name { get; set; }
    public string Cost { get; set; }

    public Product(string name, string cost)
    {
        Name = name;
        Cost = cost;
    }

    public override string ToString()
    {
        return $"{Name}\t-\t{Cost}";
    }
}

public class Cart
{
    public Product[] Products { get; set; }

    public Cart(Product[] products)
    {
        Products = products;
    }
}

//using (XmlTextWriter writer = new XmlTextWriter("orders.xml", System.Text.Encoding.UTF8))
//{
//    writer.Formatting = Formatting.Indented;
using System.Xml;
using static System.Console;

// ZADANIE 2

/*

С помощью класса XmlTextWriter напишите приложение,
сохраняющее в xml-файл информацию о заказах. Каждый
заказ представляет собой несколько товаров. Информацию, 
характеризующую заказы и товары разработать
самостоятельно.

*/

BunchOfOrders bun = new BunchOfOrders
    (
        new List<Cart>()
        {
        (new Cart
            ( new List<Product>()
                {
                    new Product("pr1", 1), new Product("pr2", 2), new Product("pr3", 3)
                },
                "first order"
            )
        ),
        (new Cart
            ( new List<Product>()
                {
                    new Product("pr4", 1), new Product("pr5", 2), new Product("pr6", 3)
                },
                "second order"
            )
        )
        }
    );

public class Product
{
    public string Name { get; set; }
    public int Cost { get; set; }

    public Product() { }
    public Product(string name, int cost)
    {
        Name = name;
        Cost = cost;
    }

    public override string ToString()
    {
        return $"\n{Name}\t-\t{Cost}";
    }
}

public class Cart
{
    public List<Product> Products { get; set; }
    public string OrderDesc { get; set; }

    public Cart() { }
    public Cart(List<Product> products, string orderDesc)
    {
        Products = new List<Product>(products);

        OrderDesc = orderDesc;
    }

    public int GetCost()
    {
        int ret = 0;

        foreach(var item in Products)
        {
            ret += item.Cost;
        }

        return ret;
    }
}

public class BunchOfOrders
{
    public List<Cart> Carts { get; set; }

    public BunchOfOrders(List<Cart> carts)
    {
        Carts = new List<Cart>(carts);
    }

    public void LoadBunchXml(string way)
    {
        using (XmlTextWriter writer = new XmlTextWriter(way, System.Text.Encoding.UTF8))
        {
            writer.Formatting = Formatting.Indented;

            foreach(var cart in Carts)
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Orders");
                writer.WriteStartElement("Products");

                foreach(var item in cart.Products)
                {
                    writer.WriteStartElement("Product");
                    writer.WriteElementString("Name: ", item.Name);
                    writer.WriteElementString("Price", item.Cost.ToString());
                    writer.WriteEndElement();
                }

                writer.WriteStartElement("Description");
                writer.WriteStartElement(cart.OrderDesc);
                writer.WriteStartElement("Price for the whole cart: ", cart.GetCost().ToString());

                writer.WriteEndElement(); // Description
                writer.WriteEndElement(); // Products
                writer.WriteEndElement(); // Order
            }



        }
    }
}
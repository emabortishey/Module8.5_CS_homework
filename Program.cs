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
        return $"\n{Name}\t-\t{Cost}";
    }
}

public class Cart
{
    public List<Product> Products { get; set; }
    public string OrderDesc { get; set; }

    public Cart(List<Product> products, string orderDesc)
    {
        Products = new List<Product>(products);

        OrderDesc = orderDesc;
    }
}

public class BunchOfOrders
{
    public List<Cart> Carts { get; set; }

    public BunchOfOrders(List<Cart> carts)
    {
        Carts = new List<Cart>(carts);
    }
}

//using (XmlTextWriter writer = new XmlTextWriter("orders.xml", System.Text.Encoding.UTF8))
//{
//    writer.Formatting = Formatting.Indented;
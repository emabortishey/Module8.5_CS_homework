using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using static System.Console;

// ZADANIE 2

/*

С помощью класса XmlTextWriter напишите приложение,
сохраняющее в xml-файл информацию о заказах. Каждый
заказ представляет собой несколько товаров. Информацию, 
характеризующую заказы и товары разработать
самостоятельно.

*/

BunchOfOrders bun = new BunchOfOrders(
    new List<Cart>()
    {
        new Cart(
            new List<Product>()
            {
                new Product("Ноутбук", 45000),
                new Product("Мышь", 1200),
                new Product("Клавиатура", 2500)
            },
            "Офисная техника"
        ),
        new Cart(
            new List<Product>()
            {
                new Product("Монитор", 28000),
                new Product("Коврик для мыши", 500)
            },
            "Дополнительное оборудование"
        )
    }
);

bun.LoadBunToXml("bun.xml");


// ZADANIE 3

/*

Используя XSLT-преобразование сгенерируйте HTML-документ 
из XML-документа, полученного в задании 2.

*/

bun.TransformToHtml("bun.xml", "bun.xslt", "bun.html");

bun.ReadWithXmlTextReader("bun.xml");

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

    public void LoadBunToXml(string path)
    {
        using (XmlTextWriter writer = new XmlTextWriter(path, System.Text.Encoding.UTF8))
        {
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("Orders"); // корневой элемент
            // (вывод обозначения о том, что сейчас произойдёт
            // запись вывода всех заказов из BunchOfOrders

            // пробегаемся по всем корзинам с заказами
            foreach (var cart in Carts)
            {
                // обозначаем предстоящий вывод заказа
                writer.WriteStartElement("Order");

                // обозначаем вывод описания заказа
                writer.WriteElementString("Description", cart.OrderDesc);

                // вывод товаров в корзине
                writer.WriteStartElement("Products");

                // пробегаемся по списку товаров в корзине
                foreach (var item in cart.Products)
                {
                    // вывод отедльного товара
                    writer.WriteStartElement("Product");
                    writer.WriteElementString("Name", item.Name);
                    writer.WriteElementString("Price", item.Cost.ToString());
                    writer.WriteEndElement(); // завершение записи Product
                }
                writer.WriteEndElement(); // завершение записи Products

                // общая стоимость
                writer.WriteElementString("TotalCost", cart.GetCost().ToString());

                writer.WriteEndElement(); // завершение записи Order
            }

            writer.WriteEndElement(); // завершение записи Orders
            writer.WriteEndDocument();
        }
    }

    public void PrintOrders()
    {
        foreach (var cart in Carts)
        {
            WriteLine($"\n{cart.OrderDesc}\n");

            foreach (var item in cart.Products)
            {
                WriteLine(item.ToString());
            }

            WriteLine($"{cart.GetCost()}\n\n");
        }
    }

    public void TransformToHtml(string xmlPath, string xsltPath, string htmlOutputPath)
    {
        // создаем переменную класса XslCompiledTransform и загружаем в нее XSLT
        // (я не совсем поняла как конеретно создать файл такого типа
        // поэтому посмотрела примеры таких файлов и, оставив обязательные
        // строки для работы файла сверху, просто вставила текст из хмл файла)
        XslCompiledTransform transform = new XslCompiledTransform();
        // загрузка самого созданного хслт файла
        transform.Load(xsltPath);

        // выполняем преобразование из хмл в хтмл
        transform.Transform(xmlPath, htmlOutputPath);

        // выводим сообщение об успешном создании файла
        WriteLine($"HTML файл успешно создан: {htmlOutputPath}");
    }

    // метод для чтения хмл
    public void ReadWithXmlTextReader(string path)
    {
        WriteLine("\nЧтение с помощью XmlTextReader:\n");

        // с помощью класса хмл ридер создаем переменную
        // указывающую на текущий созданный нами файл
        using (XmlTextReader reader = new XmlTextReader(path))
        {
            // пока есть что читать, продолжаем цикл и
            // с помощью него же переходим на след. строку
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    // с помощью свича проверяем значения
                    // текущего элемента
                    switch (reader.Name)
                    {
                        case "Order":
                            {
                                Write("Заказ: ");

                                break;
                            }

                        case "Description":
                            {
                                // в каждом кейсе в котором мы выводим
                                // не просто строку, а содержимое файла,
                                // мы проводим проверку на успешное чтение
                                if (reader.Read())
                                {
                                    WriteLine($"{reader.Value}\n");
                                }
                                break;
                            }

                        case "Products":
                            {
                                WriteLine("Все покупки:\n");

                                break;
                            }

                        case "Product":
                            {
                                Write("- ");

                                break;
                            }

                        case "Name":
                            {
                                if (reader.Read())
                                {
                                    Write($"{reader.Value}: ");
                                }
                                break;
                            }

                        case "Price":
                            {
                                if (reader.Read())
                                {
                                    Write($"{reader.Value} руб.\n");
                                }
                                break;
                            }

                        case "TotalCost":
                            {

                                if (reader.Read())
                                {
                                    WriteLine($"\nОбщая стоимость: {reader.Value} руб.\n");
                                }
                                break;
                            }
                    
                    }
                }
            }
        }
    }
}
using EFFirstApp.Models;

namespace EFFirstApp
{
    internal class Program
    {
        ShoppingContext context = new ShoppingContext();
        Product CreateAndPopulateProduct()
        {
            Product product = new Product();
            Console.WriteLine("Please enter the Product Name");
            product.Name = Console.ReadLine() ?? "";
            Console.WriteLine("Please enter the Product Price");
            product.Price = float.Parse(Console.ReadLine() ?? "0");
            Console.WriteLine("Please enter the Product Quantity");
            product.Quantity = int.Parse(Console.ReadLine() ?? "0");
            Console.WriteLine("Please enter the Product Image");
            product.Image = Console.ReadLine() ?? "";
            return product;
        }
        void InsertProduct()
        {
            Product product = CreateAndPopulateProduct();
            try
            {
                context.Products.Add(product);//This will add to the local collection
                context.SaveChanges();//This will execute all the DML commands
                Console.WriteLine("Product added");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Product not added");
            }
        }
        void GetProducts()
        {
            var products = context.Products.ToList();
            products = products.OrderByDescending(p => p.Price).ToList();
            PrintProducts(products);
        }
        void PrintProducts(List<Product> products)
        {
            foreach (var product in products)
            {
                Console.WriteLine($"Id:{product.Id}\nName:{product.Name}\nPrice:{product.Price}\nQuantity:{product.Quantity}\nImage:{product.Image}");
                Console.WriteLine("-------------------------");
            }
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            //program.InsertProduct();
            program.GetProducts();
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem
{
    public class Supplier :  IsupplierRegis
    {
        public int SupplierID { get; set; }
        public string Name { get; set; }

        // Private list of products
        private List<Product> products = new List<Product>();

        // Public getter for accessing products
        public List<Product> Products
        {
            get { return products; }
        }
        public static List<Supplier> GetAllSuppliers()
        {
            return registeredSuppliers;
        }
        public static List<Product> GetAllProducts()
        {
            return registeredSuppliers.SelectMany(s => s.Products).ToList();
        }

        // Static list to hold all registered suppliers
        private static List<Supplier> registeredSuppliers = new List<Supplier>();

        public void RegisterSupplier(Supplier supplier)
        {
            registeredSuppliers.Add(supplier);
            Console.WriteLine($"Supplier {supplier.SupplierID} has been registered.");
        }

        public Supplier LoginSupplier(int supplierId)
        {
            return registeredSuppliers.FirstOrDefault(s => s.SupplierID == supplierId);
        }
        public void addProduct(Product product)
        {
            products.Add(product);
            Console.WriteLine($"Product {product.Id} has been added by supplier {Name}.");
        }

        public void UpdateProductQuantity(string productName, int quantity)
        {
            var product = products.FirstOrDefault(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));
            if (product != null)
            {
                product.Quantity += quantity;
                Console.WriteLine($"Product {product.Name}'s quantity updated to {product.Quantity}.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }
       
        public void ListAllProducts()
        {
            var allProducts = Supplier.registeredSuppliers.SelectMany(s => s.Products).ToList();

            if (allProducts.Count > 0)
            {
                Console.WriteLine("===== All Products =====");
                foreach (var product in allProducts)
                {
                    Console.WriteLine("------------------------------");
                    Console.WriteLine($"Supplier ID   : {product.SupplierID}");
                    Console.WriteLine($"Product ID    : {product.Id}");
                    Console.WriteLine($"Name          : {product.Name}");
                    Console.WriteLine($"Price         : {product.Price:Rs}");
                    Console.WriteLine($"Quantity      : {product.Quantity}");

                    if (product.Quantity == 0)
                    {
                        Console.WriteLine("Status        : Out of Stock");
                    }

                    Console.WriteLine("------------------------------");
                }
            }
            else
            {
                Console.WriteLine("No products found.");
            }
        }


        public void ListProducts(int supplierId)
        {
            var supplierProducts = products.Where(p => p.SupplierID == supplierId).ToList();

            if (supplierProducts.Count > 0)
            {
                Console.WriteLine($"===== Products for Supplier ID: {supplierId} =====");
                foreach (var product in supplierProducts)
                {
                    Console.WriteLine("------------------------------");
                    Console.WriteLine($"Product ID    : {product.Id}");
                    Console.WriteLine($"Name          : {product.Name}");
                    Console.WriteLine($"Price         : {product.Price:Rs}");
                    Console.WriteLine($"Quantity      : {product.Quantity}");

                    if (product.Quantity == 0)
                    {
                        Console.WriteLine("Status        : Out of Stock");
                    }

                    Console.WriteLine("------------------------------");
                }
            }
            else
            {
                Console.WriteLine("No products found for this supplier.");
            }
        }



    }
}

using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.Numerics;
using System.Xml.Linq;
using ProductManagementSystem;

namespace productapp
{
    internal class Program
    {
        static void ListCustomersAndSuppliers()
        {
            // List all suppliers
            Console.WriteLine("===== All Suppliers =====");
            var suppliers = Supplier.GetAllSuppliers();
            foreach (var supplier in suppliers)
            {
                Console.WriteLine($"Supplier ID: {supplier.SupplierID}, Name: {supplier.Name}");
            }

            // List all customers
            Console.WriteLine("===== All Customers =====");
            var customers = Customer.GetAllCustomers();
            foreach (var customer in customers)
            {
                Console.WriteLine($"Customer ID: {customer.CustomerID}, Name: {customer.Name}, Phone: {customer.PhoneNumber}");
            }

        }
        void PrintMenu()
        {
            Console.WriteLine(" Main Menu :");
            Console.WriteLine("1 - Hey, Supplier");
            Console.WriteLine("2 - Hey, Customer");
            Console.WriteLine("3 - View all the products :)");
            Console.WriteLine("4 - Clear Screen");  // Add this option
            Console.WriteLine("5 - View customer & suppliers ");
            Console.WriteLine("0 - Exit");
        }
        void PrintMenuSup()
        {
            Console.WriteLine("Supplier Main Menu :");
            Console.WriteLine("1 - Register as supplier");
            Console.WriteLine("2 - login as supplier");
            Console.WriteLine("3 - View all products ");
            Console.WriteLine("4 - Clear Screen");  // Add this option
            Console.WriteLine("0 - Exit");

        }
        void PrintMenucust()
        {
            Console.WriteLine(" Customer Main Menu :");
            Console.WriteLine("1 - Register as customer");
            Console.WriteLine("2 - login as customer");
            Console.WriteLine("4 - Clear Screen");  // Add this option
            Console.WriteLine("3 - View all products ");
            Console.WriteLine("0 - Exit");
        }

        void ProductManagementSystem()
        {
            var choice = 0;
            
            Supplier suppliermng = new Supplier();
            Customer customermng = new Customer();
            do
            {
                PrintMenu();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        // supplier
                        Console.WriteLine("   HELLO , SUPPLIER :)   ");


                        int supplierChoice;
                        do
                        {
                            // Show the supplier-specific menu
                            PrintMenuSup();

                            supplierChoice = Convert.ToInt32(Console.ReadLine());

                            switch (supplierChoice)
                            {
                                case 1:
                                    // Register a new supplier
                                    Console.WriteLine("Enter Supplier ID to register:");
                                    int id = Convert.ToInt32(Console.ReadLine());

                                    Console.WriteLine("Enter Supplier Name to register:");
                                    var name = Console.ReadLine();
                                    var newSupplier = new Supplier { SupplierID = id, Name = name };

                                    suppliermng.RegisterSupplier(newSupplier);

                                    Console.WriteLine("Now you are registered and logged in as a supplier!");

                                    // Automatically log in the registered supplier and manage products
                                    ManageSupplierProducts(newSupplier);
                                    break;

                                case 2:
                                    // Login an existing supplier
                                    Console.WriteLine("Enter Supplier ID to log in:");
                                    int sid = Convert.ToInt32(Console.ReadLine());

                                    var loggedInSupplier = suppliermng.LoginSupplier(sid);

                                    if (loggedInSupplier != null)
                                    {
                                        Console.WriteLine($"Welcome back, {loggedInSupplier.Name}!");

                                        // Manage supplier product operations
                                        ManageSupplierProducts(loggedInSupplier);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Supplier not found. Please register first.");
                                    }
                                    break;

                                case 3:

                                    // View all products from all suppliers
                                    Console.WriteLine("Viewing all products from all suppliers:");
                                    suppliermng.ListAllProducts();
                                    break;

                                case 0:
                                    Console.WriteLine("Exiting supplier menu...");
                                    break;

                                case 4:
                                    Console.Clear();
                                    break;

                                

                                default:
                                    Console.WriteLine("Invalid option, please try again.");
                                    break;
                            }


                        } while (supplierChoice != 0);  // Exit loop when user chooses 0
                   
                        

                        break;

                    case 2:
                        Console.WriteLine("    HELLO , CUSTOMER :)   ");
                        int customerchoice;
                        do
                        {
                            // Show the supplier-specific menu
                            PrintMenucust();

                            customerchoice = Convert.ToInt32(Console.ReadLine());

                            switch (customerchoice)
                            {
                                case 1:
                                    Console.WriteLine("Enter customer ID to register:");
                                    int id = Convert.ToInt32(Console.ReadLine());

                                    Console.WriteLine("Enter customer Name to register:");
                                    var name = Console.ReadLine();

                                    Console.WriteLine("Enter customer phone number to register:");
                                    var phoneNumber = Console.ReadLine();

                                    // Basic validation for phone number
                                    //if (string.IsNullOrEmpty(phoneNumber) || !phoneNumber.All(char.IsDigit) || phoneNumber.Length == 10)
                                    //{
                                    //    Console.WriteLine("Invalid phone number. It should be numeric and  10  digits long.");
                                    //}
                                    //else
                                    //{
                                        var newCustomer = new Customer { CustomerID = id, Name = name, PhoneNumber = phoneNumber };

                                        customermng.RegisterCustomer(newCustomer);

                                        Console.WriteLine("You are registered as a customer!");

                                        // Automatically log in the registered customer
                                        ManageCustomerProducts(newCustomer);
                                    
                                    
                                    break;

                                case 2:
                                    // Login an existing supplier
                                    Console.WriteLine("Enter customer ID to log in:");
                                    int cid = Convert.ToInt32(Console.ReadLine());

                                    var loggedIncustomer = customermng.LoginCustomer(cid);

                                    if (loggedIncustomer != null)
                                    {
                                        Console.WriteLine($"Welcome back, {loggedIncustomer.Name}!");

                                        // Manage customer product operations
                                        ManageCustomerProducts(loggedIncustomer);
                                    }
                                    else
                                    {
                                        Console.WriteLine("customer not found. Please register first.");
                                    }
                                    break;

                                case 3:

                                    // View all products from all suppliers
                                    Console.WriteLine("Viewing all products from all suppliers:");
                                    suppliermng.ListAllProducts();
                                    break;

                                case 0:
                                    Console.WriteLine("Exiting supplier menu...");
                                    break;

                                case 4:
                                    Console.Clear();
                                    break;
                                default:
                                    Console.WriteLine("Invalid option, please try again.");
                                    break;
                            }

                        } while (customerchoice != 0);  // Exit loop when user chooses 0

                        break;

                    case 3:
                        
                            // View all products from all suppliers
                            Console.WriteLine("Viewing all products from all suppliers:");
                            suppliermng.ListAllProducts();                                        
                        break;

                    case 4:
                        Console.Clear();
                        break;
                    case 5:
                        ListCustomersAndSuppliers();
                        break;
                    default:
                        Console.WriteLine(" Invalid option ");
                        break;
                }
            } while (choice != 0);

        }
        void ManageCustomerProducts(Customer customer)
        {
            Customer customermng=new Customer();
            Supplier suppliermng = new Supplier();

            Console.WriteLine("1 - BUY Productss");
            Console.WriteLine("2 - View Products in general");
            Console.WriteLine("4- clear screen");

            int custchoice = Convert.ToInt32(Console.ReadLine());

            switch (custchoice)
            {
                case 1:
                    // BUY a new product
                    bool continueShopping = true;

                    while (continueShopping)
                    {
                        Console.WriteLine("Enter Product Name to purchase:");
                        string productName = Console.ReadLine();

                        Console.WriteLine("Enter Quantity to purchase:");
                        int purchaseQuantity = Convert.ToInt32(Console.ReadLine());

                        // Check if the product is available and has enough quantity
                        var product = Supplier.GetAllProducts().FirstOrDefault(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));

                        if (product != null)
                        {
                            if (product.Quantity >= purchaseQuantity)
                            {
                                // Process the purchase
                                product.Quantity -= purchaseQuantity;
                                Console.WriteLine($"Successfully purchased {purchaseQuantity} of {productName}. Remaining stock: {product.Quantity}");

                                // Ask if the customer wants to buy another product
                                Console.WriteLine("Do you want to buy another product? (yes/no)");
                                var response = Console.ReadLine().ToLower();
                                if (response != "yes")
                                {
                                    Console.WriteLine("thanks for purchasing");
                                    continueShopping = false;
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Not enough stock for {productName}. Only {product.Quantity} items left.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Product '{productName}' not found.");
                        }
                    }
                    break;

                case 2:
                    // view product 
                    Console.WriteLine("Viewing all products from all suppliers:");
                    suppliermng.ListAllProducts();
                    break;

               

                case 4:
                    Console.Clear();
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }


        }
        void ManageSupplierProducts(Supplier supplier)
        {
            Supplier suppliermng = new Supplier();
            Console.WriteLine("1 - Add Product");
            Console.WriteLine("2 - Update Product Quantity");
            Console.WriteLine("3 - View Products added by you");
            Console.WriteLine("4- clear screen");

            var supplierChoice = Convert.ToInt32(Console.ReadLine());

            switch (supplierChoice)
            {
                case 1:
                    // Add a new product
                    Product product = new Product();

                    // Take product details from the user
                    Console.WriteLine("Enter Product ID:");
                    product.Id = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter Product Name:");
                    product.Name = Console.ReadLine();

                    Console.WriteLine("Enter Product Price:");
                    product.Price = Convert.ToDecimal(Console.ReadLine());

                    Console.WriteLine("Enter Product Quantity:");
                    product.Quantity = Convert.ToInt32(Console.ReadLine());

                    // Link the product with the supplier by setting the SupplierID
                    product.SupplierID = supplier.SupplierID;

                    supplier.addProduct(product);
                    break;

                case 2:
                    // Update product quantity
                    Console.WriteLine("Enter Product Name to update:");
                    var productName = Console.ReadLine();
                    Console.WriteLine("Enter new quantity to add:");
                    var quantity = Convert.ToInt32(Console.ReadLine());

                    supplier.UpdateProductQuantity(productName, quantity);
                    break;

                case 3:
                    // View products added by suppliers
                    Console.WriteLine("Enter Supplier ID to view their products (or enter 0 to view all products):");
                    int viewSupplierId = Convert.ToInt32(Console.ReadLine());

                    if (viewSupplierId == 0)
                    {
                        // View all products from all suppliers
                        Console.WriteLine("Viewing all products from all suppliers:");
                        suppliermng.ListAllProducts();
                    }
                    else
                    {
                        // View products for a specific supplier
                        var supplierToView = suppliermng.LoginSupplier(viewSupplierId);
                        if (supplierToView != null)
                        {
                            Console.WriteLine($"Viewing products for Supplier {supplierToView.Name}:");
                            supplierToView.ListProducts(supplierToView.SupplierID);
                        }
                        else
                        {
                            Console.WriteLine("Supplier not found.");
                        }
                    }
                    break;

                case 4:
                    Console.Clear();
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Welcome in Product Management System!");
            //Create an application that will take Product details
            //Product Class
            //   Id, Name, Price, Quantity
            //Customer
            //   Id, Name, Phone
            //Supplier
            //   Id, Name

            //Supplier can add product - increase quantity
            //Customer can buy product - decrease quantity


            //Implement it with the proper business logic
            var program = new Program();
            program.ProductManagementSystem();
            Console.ReadKey();

        }
    }
}

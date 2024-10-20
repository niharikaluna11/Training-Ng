using CustomerApplicationD27.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApplicationD27.Services
{
    internal class CustomerService : IShoppingService
    {
        SqlConnection conn = new SqlConnection("Server=5CD413DKS0\\DEMOINSTANCE;Integrated Security=true;Initial Catalog=NorthWind;TrustServerCertificate=True");

        //1) View Previous Order(all orders sorted by date in desc)

        public bool CheckCustomer(string customerId)
        {
            SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM Customers WHERE CustomerID =@custId", conn);
            try
            {
                conn.Open();
                sqlCommand.Parameters.AddWithValue("@custId", customerId);
               
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
               // conn.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }
      
        public void ViewPreviousOrders(string customerId)
        {
            string query = "SELECT * FROM Orders WHERE CustomerID = @CustomerId ORDER BY OrderDate DESC";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@CustomerId", customerId);

            try
            {
                if (CheckCustomer(customerId)) {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // Display order d"etails

                        Console.WriteLine($"OrderID: {reader["OrderID"]}, Order Date: {reader["OrderDate"]}");
                        Console.WriteLine("---------------------------------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("Could not verify customer id.");
                }
              
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        //   2) View Order summary take order number from user
        //   - Order number, Customer name, Products ordered 
        public void ViewOrderSummary(string customerId)
        {
            Console.Write("Enter Order ID: ");
            if (int.TryParse(Console.ReadLine(), out int shipperOrderId))
            {
                string query = "SELECT Orders.OrderID, Customers.CompanyName, Products.ProductName " +
                           "FROM [Order Details] " +
                           "INNER JOIN Orders ON [Order Details].OrderID = Orders.OrderID " +
                           "INNER JOIN Customers ON Orders.CustomerID = Customers.CustomerID " +
                           "INNER JOIN Products ON [Order Details].ProductID = Products.ProductID " +
                           "WHERE Orders.OrderID = @OrderId AND Customers.CustomerID =@custId";

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@OrderId", shipperOrderId);
                command.Parameters.AddWithValue("@custId", customerId);
                try
                {
                    if (CheckCustomer(customerId))
                    {
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine($"Order ID: {reader["OrderID"]}, Customer: {reader["CompanyName"]}, Product: {reader["ProductName"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Could not verify customer id.");
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
                finally
                {
                    conn.Close();
                }

            }
            else
            {
                Console.WriteLine("Invalid Order ID.");
            }
              
        }


        //   3) View shipper details for given order number
        public void ViewShipperDetails(string customerId)
        {
            Console.Write("Enter Order ID: ");
            if (int.TryParse(Console.ReadLine(), out int shipperOrderId))
            {
                string query = "SELECT Shippers.CompanyName, Shippers.Phone FROM Shippers " +
                           "INNER JOIN Orders ON Shippers.ShipperID = Orders.ShipVia " +
                           "WHERE Orders.OrderID = @OrderId AND Customers.CustomerID =@custId";

            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@OrderId", shipperOrderId);
            command.Parameters.AddWithValue("@custId", customerId);
             try
            {

                    if (CheckCustomer(customerId))
                    {
                        conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine($"Shipper: {reader["CompanyName"]}, Phone: {reader["Phone"]}");
                }
                else
                {
                    Console.WriteLine("No shipper details found for this order.");
                }
                }
                    else
                {
                    Console.WriteLine("Could not verify customer id.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                conn.Close();
            }
            }
            else
            {
                Console.WriteLine("Invalid Order ID.");
            }
        }
    }
}

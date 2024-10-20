using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerApplicationD27.Interfaces;
using CustomerApplicationD27.Models;
using Microsoft.Data.SqlClient;

namespace CustomerApplicationD27.Services
{
    internal class UserService : IAuthService
    {
        static  string UserName, Password;
       
        SqlConnection conn = new SqlConnection("Server=5CD413DKS0\\DEMOINSTANCE;Integrated Security=true;Initial Catalog=NorthWind;TrustServerCertificate=True");
         
        //checking user exist or not
        public bool CheckUser(string username, string password)
        {
            SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM Users WHERE Username=@un AND Password=@pass", conn);
            try
            {
                conn.Open();
                sqlCommand.Parameters.AddWithValue("@un", username);
                sqlCommand.Parameters.AddWithValue("@pass", password);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        // login
        public bool Login()
        {
            string username, password;
            Console.Write("Please enter the username: ");
            username = Console.ReadLine();
            Console.Write("Please enter your password: ");
            password = Console.ReadLine();

            try
            {
                if (CheckUser(username, password))
                {
                    Console.WriteLine("Login successful!");
                    return true;
                }
                else
                {
                    Console.WriteLine("Could not verify user. Please register first.");
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                return false;
            }
        }

        // register
        public void Register()
        {

            string name;

            Console.Write("Enter your Name :");
            name = Console.ReadLine();

            Console.Write("Enter your UserName :");
            UserName = Console.ReadLine();

            Console.Write("Enter your Password :");
            Password = Console.ReadLine();


           
            string insertQuery = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";

           
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            
                cmd.Parameters.AddWithValue("@Username", UserName);
                cmd.Parameters.AddWithValue("@Password", Password);

            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("User created successfully");
                }
                else
                {
                    Console.WriteLine("User creation failed");
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

        // update password
        public void UpdatePassword()
        {

            string username, password, newPassword;
            Console.WriteLine("Please enter the username");
            username = Console.ReadLine();
            Console.WriteLine("Pleae enter your current password");
            password = Console.ReadLine();
            try
            {
                if (CheckUser(username, password))
                {
                    Console.WriteLine("Please enter your new password");
                    newPassword = Console.ReadLine();
                    SqlCommand sqlCommand = new SqlCommand("UPDATE Users SET Password=@newpass WHERE Username=@un", conn);
                    sqlCommand.Parameters.AddWithValue("@newpass", newPassword);
                    sqlCommand.Parameters.AddWithValue("@un", username);
                    try
                    {
                        conn.Open();
                        int rowsAffected = sqlCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Password updated successfully");
                        }
                        else
                        {
                            Console.WriteLine("Password update failed");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Could not verify user. Sorry cannot update password now.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }

        }


    }
}

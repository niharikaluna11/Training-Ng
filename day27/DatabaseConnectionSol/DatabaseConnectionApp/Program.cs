﻿using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Data;

namespace DatabaseConnectionApp
{


    internal class Program
    {
        SqlConnection conn = new SqlConnection("Server=5CD413DKS0\\DEMOINSTANCE;Integrated Security=true;Initial Catalog=NorthWind;TrustServerCertificate=True");

        public Program()
        {
           // conn.Open();
           // Console.WriteLine($"The current connection state is {conn.State}");
           //// conn.Close();
        }
        void GetProductDeatilsFromDatabase()
        {
            SqlCommand cmd = new SqlCommand();
             cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Products";
            //cmd.CommandText = "SELECT * FROM Employees";
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    Console.WriteLine($"Name: \t{reader["ProductName"]}");
                    Console.WriteLine($"Price: \t${reader["UnitPrice"]}");
                    Console.WriteLine("-------------------------------------");
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

        void CreateUser()
        {
            string username, password;
            Console.Write("Enter username: ");
            username = Console.ReadLine();
            Console.Write("Enter password: ");
            password = Console.ReadLine();

            //string insertQuery = "Insert into Users values('"+username+"', '"+password+"')";

            string insertQuery = $"Insert into Users values('{username}', '{password}')";

            SqlCommand cmd = new SqlCommand(insertQuery, conn);
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
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        bool CheckUser(string username, string password)
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
        void DeleteUser()
        {
            string username,password;
            string ans;
            Console.WriteLine("Please enter your name");
            username = Console.ReadLine();

           

            Console.WriteLine("Enter your password");
            password = Console.ReadLine();

            try
            {
                if (CheckUser(username, password))
                {
                    Console.WriteLine("Are your sure you want to delete user (yes/no) ?m ");
                    ans = Console.ReadLine();

                    if (ans == "yes")
                    {
                        Console.WriteLine("deleting");
                        SqlCommand sqlCommand = new SqlCommand("DELETE FROM Users WHERE username=@un", conn);

                        sqlCommand.Parameters.AddWithValue("@un", username);

                        try
                        {
                            conn.Open();
                            int rowsAffected = sqlCommand.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                Console.WriteLine(" deleted successfully");
                            }
                            else
                            {
                                Console.WriteLine("deletion failed");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine(" ok ");
                    }
                }
               
                  
                else
                {
                    Console.WriteLine("Could not verify user. Sorry :(.");
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
        void UpdatePassword()
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

        void UnderstandingDisconnectedArchitecture()
        {
            Console.WriteLine($"The current connection state is {conn.State}");

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("SELECT * FROM Products", conn);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet);
                Console.WriteLine($"The current connection state is {conn.State}");
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Console.WriteLine($"Name: {row["ProductName"]}");
                    Console.WriteLine($"Price: {row["UnitPrice"]}");
                    Console.WriteLine("-------------------------------------");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        void FetchCategoryData()
        {
            
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("select * from Categories", conn);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet);
                Console.WriteLine($"The current connection state is {conn.State}");
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Console.WriteLine($"ID: {row["CategoryID"]}");
                    Console.WriteLine($"Name: {row["CategoryName"]}");
                    Console.WriteLine($"Desc: {row["Description"]}");
                    Console.WriteLine($"Pic: {row["Picture"]}");
                    Console.WriteLine("-------------------------------------");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }





        static void Main(string[] args)
        {
            Console.WriteLine("Hello,  :)!");
            Program program = new Program();
            //  program.GetProductDeatilsFromDatabase();
            //program.CreateUser();
           //program.FetchCategoryData();
            //program.UnderstandingDisconnectedArchitecture();
            //program.UpdatePassword();
            program.DeleteUser();
          }

    } }

      
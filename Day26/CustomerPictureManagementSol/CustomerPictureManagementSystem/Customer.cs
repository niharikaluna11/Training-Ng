using System;
using System.IO;

namespace CustomerPictureManagementSystem
{
    public class Customer
    {

        public string Name { get; private set; }
        public string[] PicturePaths { get; private set; } // Store multiple picture paths

        // Enter details Method: take name and picture path
        public void EnterDetails()
        {
            while (true)
            {
                Name = GetValidName();

                // Generate folder name
                string folderName = $"{Name}";

                /*The @ allows you to write the path without needing to escape the backslashes. 
                Normally, you would have to write it as "c:\\myFolder\\myfile.txt" if you didn't use @*/

                string baseLocation = @"D:\CustomerImages";

                /*Path.Combine: This method is used to concatenate paths in a way that is aware 
                of the platform's directory separator character. 
                If baseLocation is C:\myFolder and folderName is CustomerData,
                the customerFolderPath would be C:\myFolder\CustomerData
                used in Windows vs. Linux/macOS.*/

                string customerFolderPath = Path.Combine(baseLocation, folderName);

                // Create the customer folder if it doesn't exist
                if (!Directory.Exists(customerFolderPath))
                {
                    Directory.CreateDirectory(customerFolderPath);
                    Program.DisplayColoredMessage($"Folder created: {customerFolderPath}", "Black", "White");
                    //Console.WriteLine($"Folder created: {customerFolderPath}");
                }
                else
                {
                    Program.DisplayColoredMessage($"Folder Already Existed: {folderName}", "Black", "White");
                }

                /*Enter the picture paths in this functoin and 
                copying the image  to another folder */

                PicturePaths = GetPicturePaths(customerFolderPath);

                /*
                 Log out and while condition things
                 */
                
                Console.WriteLine("Do you want to log out? (yes/no)");
                string userInput = Console.ReadLine()?.Trim().ToLower();

                if (userInput != "no")
                {
                    Program.DisplayColoredMessage("Logging out...", "Blue", "White");

                    break; // Exiting
                }

            }
        }

        // Method to get a valid name (no digits allowed)
        private string GetValidName()
        {
            string userName = string.Empty;
            bool isValidName = false;

            while (!isValidName)
            {
                try
                {
                    Program.DisplayColoredMessage("Enter your name:", "Blue", "Black");
                    userName = Console.ReadLine();

                    // Check if username contains any digits
                    foreach (char c in userName)
                    {
                        if (char.IsDigit(c))
                        {
                            throw new ArgumentException("Username should not contain any digits.");
                        }
                    }

                    isValidName = true;  // Exit the loop if the name is valid
                }
                catch (Exception ex)
                {
                    Program.DisplayColoredMessage($"Error: {ex.Message} \t Please try again.\n", "White", "Red");
                }
            }

            return userName;
        }

        // Valid picture paths from the user 
        //can enter many pictuiress so string of customerfolder image 
        private string[] GetPicturePaths(string customerFolderPath)
        {
            var picturePathsList = new List<string>();
            bool addMorePictures = true;

            while (addMorePictures)
            {
                string picturePath = string.Empty;
                bool isValidPath = false;

                while (!isValidPath)
                {
                    try
                    {
                        Program.DisplayColoredMessage("Enter the full path of the picture \n" +
                            "(write no if you dont wanna give any picture :):", "Blue", "Black");
                        //trimming img
                        picturePath = Console.ReadLine()?.Trim();

                        if(picturePath == "no")
                        {
                            break;
                           
                        }
                        // Check if the file exists at the provided path
                        if (File.Exists(picturePath))
                        {
                            picturePathsList.Add(picturePath); //multiple pic added
                            isValidPath = true;
                        }
                        else
                        {
                            throw new FileNotFoundException($"File not found: {picturePath}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.DisplayColoredMessage($"Error: {ex.Message}", "White", "Red");
                        Program.DisplayColoredMessage("Please try again.\n", "White", "Red");
                    }
                }

               
                    // Ask if the user wants to add another picture
                    Console.WriteLine("Do you want to add another picture? (yes/no)");
                    string userInput = Console.ReadLine()?.Trim().ToLower();
                    if (userInput != "yes")
                    {
                        addMorePictures = false; // Exit the loop if the user does not want to continue
                    }

                
                
            }

            // Copy all valid pictures to the customer's folder
            foreach (var path in picturePathsList)
            {
                CopyPictureToCustomerFolder(path, customerFolderPath);
            }

            return picturePathsList.ToArray(); // Return the list of picture paths as an array
        }

        // Method to copy the picture to the customer folder
        private void CopyPictureToCustomerFolder(string sourcePath, string customerFolderPath)
        {
            try
            {
                string fileName = Path.GetFileName(sourcePath);
                string destinationPath = Path.Combine(customerFolderPath, fileName);

                // Copy the file to the destination folder, overwrite if it already exists
                File.Copy(sourcePath, destinationPath, true);
                Program.DisplayColoredMessage($"Copied: {fileName}", "Red", "Yellow");
            }
            catch (Exception ex)
            {
                Program.DisplayColoredMessage($"Error copying file: {ex.Message}", "White", "Red");
            }
        }
    }
}

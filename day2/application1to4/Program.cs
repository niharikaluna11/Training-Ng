namespace application1to4
{
    internal class Program
    {
        

            /// <summary>
            /// task 1 :
            ///  Take 10 numbers from user and find the average of numbers that are divisible by 7
            /// </summary>
            static void task1()
            {
                Console.WriteLine("task 1");
                Console.WriteLine("Task 1: Enter 10 numbers:");
                int[] numbers = new int[10];
                List<int> divisibleBySeven = new List<int>();

                for (int i = 0; i < 10; i++)
                {
                    numbers[i] = int.Parse(Console.ReadLine());

                }
                Console.WriteLine("numbers which are divisible by 7 are");
                for (int i = 0; i < 10; i++)
                {
                    if (numbers[i] % 7 == 0)
                    {
                        divisibleBySeven.Add(numbers[i]);
                        Console.WriteLine(numbers[i]);
                    }
                }


                if (divisibleBySeven.Count > 0)
                {
                    double average = divisibleBySeven.Average();
                    Console.WriteLine($"The average of numbers divisible by 7 is: {average}");
                }
                else
                {
                    Console.WriteLine("No numbers divisible by 7.");
                }


            }

            /// <summary>
            /// task 2 :
            /// 
            /// </summary>

            static bool IsPrime(int number)
            {
                if (number < 2) return false;
                for (int i = 2; i <= Math.Sqrt(number); i++)
                {
                    if (number % i == 0) return false;
                }
                return true;
            }

            static void task2()
            {
                Console.WriteLine("task 2");
                Console.WriteLine("Task 2: Enter the minimum and maximum values to find prime numbers:");
                int min = int.Parse(Console.ReadLine());
                int max = int.Parse(Console.ReadLine());

                Console.WriteLine($"Prime numbers between {min} and {max}:");
                for (int num = min; num <= max; num++)
                {
                    if (IsPrime(num))
                    {
                        Console.Write(num + " ");
                    }
                }
                Console.WriteLine();
            }

            /// <summary>
            /// task 3 :
            //
            /// </summary>
            static void task3()
            {
                Console.WriteLine("task 3");
                Console.WriteLine("Task 3: Enter numbers until -1 is entered. Printing numbers ending with 3 or divisible by 3:");
                int number;
                List<int> num = new List<int>();
                while ((number = int.Parse(Console.ReadLine())) != -1)
                {
                    if (number % 10 == 3 || number % 3 == 0)
                    {
                        num.Add(number);

                    }

                }
                foreach (var n in num)
                {
                    Console.WriteLine(n);
                }
                Console.ReadKey();
            }

           

            static void Main(string[] args)
            {
                Console.WriteLine("Hello, Appl World!");

                //  Create an Application that will
                //1) Take 10 numbers from user and find the average of numbers that are divisible by 7
                //2) Take min and max value from user and find all the prime numbers in the range
                //3) Take number from user until user enters - 1.Print all the numbers that end with 3 or divisible by 3
                //4) Take input from user(max 9999) and print the number in words
                //example
                //1023 - One Thousand and Twenty Three
                //2841 - Two Thousand Eight Hundred and Forty One

                //5) Create and application that will be used for Expense Tracking of Employees in a company
                //Create the Employee and the Expense class(Models) now with the appropriate attributes and behaviour

                Console.WriteLine("\nChoose a task to execute:");
                Console.WriteLine("1 - Take 10 numbers and find average of numbers divisible by 7");
                Console.WriteLine("2 - Find all prime numbers in a range");
                Console.WriteLine("3 - Print numbers that end with 3 or divisible by 3 until -1 is entered");
                
                Console.WriteLine("0 - Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        task1();
                        // Task 1: Average of numbers divisible by 7
                        break;
                    case "2":
                        task2();
                        // Task 2: Prime numbers in a range
                        break;
                    case "3":
                        task3();
                        // Task 3: Numbers ending/divisible by 3
                        break;
                    


                    default:
                        Console.ReadKey();
                        break;
                }

                Console.ReadKey();
            }
        }
    }



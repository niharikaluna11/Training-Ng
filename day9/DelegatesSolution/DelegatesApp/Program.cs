namespace DelegatesApp1
{
    internal class Program
    {

        //public delegate void CalculateDelegate(int n1, int n2);

        public Program()
        {
            //CalculateDelegate calculateDelegate = new CalculateDelegate(Multiply);
            //Action<int, int> calculateDelegate = Multiply;
            Func<int, int, int> calculateDelegate = Multiply;


            calculateDelegate += Add;//Multicast Delegate
            //calculateDelegate += Subraction;//Multicast Delegate
            //calculateDelegate += Divsion;//Multicast Delegate
            //calculateDelegate -= Add;//Multicast Delegate

            Calculate(calculateDelegate, 100, 20);
        }
        //public void Add(int num1, int num2)
        //{
        //    int result = num1 + num2;
        //    Console.WriteLine($"The sum of {num1} and {num2} is {result}");
        //}
        //public void Multiply(int num1, int num2)
        //{
        //    int result = num1 * num2;
        //    Console.WriteLine($"The product of {num1} and {num2} is {result}");
        //}
        //public void Subraction(int num1, int num2)
        //{
        //    int result = num1 - num2;
        //    Console.WriteLine($"The product of {num1} and {num2} is {result}");
        //}
        //public void Divsion(int num1, int num2)
        //{
        //    int result = num1 / num2;
        //    Console.WriteLine($"The product of {num1} and {num2} is {result}");
        //}


        public int Add(int num1, int num2)
        {
            int result = num1 + num2;
            return result;
        }
        public int Multiply(int num1, int num2)
        {
            int result = num1 * num2;
            return result;
        }


        //-----------------------------------------------------------------------------
        //public void Calculate(CalculateDelegate myDelegate, int n1, int n2) //delegate type
        //public void Calculate(Action<int, int> myDelegate, int n1, int n2)
        //{
        //    myDelegate(n1, n2);

        //}

        public void Calculate(Func<int, int, int> myDelegate, int n1, int n2)
        {
            int result = myDelegate(n1, n2);
            Console.WriteLine($"The sum of {n1} and {n2} is {result}");

        }
        static void Main(string[] args)
        {
            new Program();
        }
    }
}

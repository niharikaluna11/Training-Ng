namespace EmployeePromotionApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            // 1)	Create a C# console application which has a class with name “EmployeePromotion” that will take employee names in the order in which they are eligible for promotion. 
            //a.	Example Input: 
            //Please enter the employee names in the order of their eligibility for promotion(Please enter blank to stop)
            //Ramu
            //Bimu
            //Somu
            //Gomu
            //Vimu
            //b.Create a collection that will hold the employee names in the same order that they are inserted.
            //c.Hint – choose the correct collection that will preserve the input order(List)

            EmployeePromotion Promotion = new EmployeePromotion();

            Promotion.InputEmployee();

            Promotion.PromotionList();

            Promotion.PositionPromotion();

            Promotion.TrimExcess();

            Promotion.SortNames();

            Console.ReadKey();
        }
    }
}

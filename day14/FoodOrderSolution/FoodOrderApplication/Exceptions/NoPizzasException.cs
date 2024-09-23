
namespace FoodOrderApplication.Exceptions
{
    public class NoPizzasException : Exception
    {
        string msg;
        public NoPizzasException()
        {
            msg = "There are no more pizzas left. You ate them all!";

        }

        public override string Message => msg;

    }
}

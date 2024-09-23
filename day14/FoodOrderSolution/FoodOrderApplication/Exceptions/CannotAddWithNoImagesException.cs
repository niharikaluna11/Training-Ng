using System.Runtime.Serialization;

namespace FoodOrderApplication.Exceptions
{
    
    public class CannotAddWithNoImagesException : Exception
    {
        string msg;
        public CannotAddWithNoImagesException()
        {
            msg = "CannotAddWithNoImages";
        }
        public override string Message => msg;
    }
}
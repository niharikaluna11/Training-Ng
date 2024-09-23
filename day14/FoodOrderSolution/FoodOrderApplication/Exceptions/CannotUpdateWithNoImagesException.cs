using System.Runtime.Serialization;

namespace FoodOrderApplication.Exceptions
{
   
    internal class CannotUpdateWithNoImagesException : Exception
    {
        string msg;
        public CannotUpdateWithNoImagesException()
        {
            msg = "CannotUpdateWithNoImagesException";
        }
        public override string Message => msg;
    }
}
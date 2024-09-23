using System.Runtime.Serialization;

namespace FoodOrderApplication.Exceptions
{
    [Serializable]
    internal class NoImagesException : Exception
    {
        string msg;
        public NoImagesException()
        {
            msg = "NoImagesException";
        }
        public override string Message => msg;
    }
}
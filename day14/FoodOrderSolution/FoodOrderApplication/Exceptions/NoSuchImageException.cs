using System.Runtime.Serialization;

namespace FoodOrderApplication.Exceptions
{
    [Serializable]
    internal class NoSuchImageException : Exception
    {
        string msg;
        public NoSuchImageException()
        {
            msg = "NoSuchImageException";
        }
        public override string Message => msg;
    }
}
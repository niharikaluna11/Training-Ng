namespace LoginApplication.Exceptions
{
    public class NoUserFoundException : Exception
    {
        string msg;
        public NoUserFoundException()
        {
            msg = "No user found :(";
        }
        public override string Message => msg;
    }
}

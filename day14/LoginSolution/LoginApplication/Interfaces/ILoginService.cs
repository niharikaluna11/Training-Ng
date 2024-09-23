namespace LoginApplication.Interfaces
{
    public interface ILoginService
    {
    
        bool ValidateLogin(string username, string password);
       
    }
}

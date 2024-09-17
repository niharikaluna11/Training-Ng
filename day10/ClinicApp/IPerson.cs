using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApplication
{
    internal interface IPerson
    {
        int Id { get; set; }
        string Name { get; set; }
        string UserName { get; set; } //username for the login :)
        string PhoneNumber { get; set; }
        string Email { get; set; }
        string Password { get; set; }

        void Register(string name,string phoneNumber, string email,string uname, string password);
        void login(string uname, string password);

    }
}

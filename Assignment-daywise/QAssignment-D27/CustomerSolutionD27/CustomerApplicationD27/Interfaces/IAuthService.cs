using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApplicationD27.Interfaces
{
    internal interface IAuthService
    {

        // can register and login both 
        void Register();

        bool Login();

        //check user if exist
        bool CheckUser(string username, string password);

        // update password
        void UpdatePassword();



    }
}

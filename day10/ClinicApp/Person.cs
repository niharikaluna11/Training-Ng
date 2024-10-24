﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApplication
{
    public abstract class Person : IPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual void Register(string name, string uname,string phoneNumber, string email, string password)
        {
            // Common registration logic
            Name = name;
            PhoneNumber = phoneNumber;
            UserName = uname;
            Email = email;
            Password = password;
        }

        public virtual bool Login(string uname, string password)
        {
            // Common login logic
            return UserName == uname && Password == password;
        }
    }
}

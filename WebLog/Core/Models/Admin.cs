using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebLog.Core.Models
{
    [Table("Admin")]
    public class Admin : User
    {
        public Admin()
        {
                
        }

        public Admin(string name, string surname, string email, string password, string pesel) : base(name,surname,email,password,pesel)
        {
   
        }
    }
}
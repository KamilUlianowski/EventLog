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
    }
}
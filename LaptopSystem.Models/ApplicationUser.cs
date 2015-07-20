using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace LaptopSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        [DataType(DataType.EmailAddress)]
        [ScaffoldColumn(false)]
        public string Email { get; set; }
    }
}
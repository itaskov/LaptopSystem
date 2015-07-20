using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using LaptopSystem.Models;
using System.Data.Entity;

namespace LaptopSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("LaptopSystemIT")
        {
        }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Laptop> Laptops { get; set; }

        public IDbSet<Manufacturer> Manufacturers { get; set; }

        public IDbSet<Vote> Votes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LaptopSystem.Models
{
    public class Manufacturer
    {
        private ICollection<Laptop> laptops;

        public Manufacturer()
        {
            this.laptops = new HashSet<Laptop>();
        }
        
        [Key]
        public int Id { get; set; }

        [AllowHtml]
        [Required]
        [Index(IsUnique=true)]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "The '{0}' should be between {2} and {1} characters.")]
        public string Name { get; set; }

        public virtual ICollection<Laptop> Laptops
        {
            get { return laptops; }
            set { laptops = value; }
        }        
    }
}

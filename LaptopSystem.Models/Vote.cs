using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LaptopSystem.Models
{
    public class Vote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name="Author")]
        public string ApplicationUserId { get; set; }

        public ApplicationUser Author { get; set; }

        [Required]
        [Display(Name = "Model")]
        public int LaptopId { get; set; }

        public Laptop Laptop { get; set; }
    }
}

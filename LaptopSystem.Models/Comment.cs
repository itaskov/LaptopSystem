﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LaptopSystem.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [Required]
        public int LaptopId { get; set; }

        public virtual Laptop Laptop { get; set; }

        public string Content { get; set; }
    }
}

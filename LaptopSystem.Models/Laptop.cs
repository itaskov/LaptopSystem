using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace LaptopSystem.Models
{
    public class Laptop
    {
        private ICollection<Vote> votes;

        private ICollection<Comment> comments;

        public Laptop()
        {
            this.votes = new HashSet<Vote>();
            this.comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name="Manufacturer")]
        public int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        [Required]
        [AllowHtml]
        [UIHint("LaptopModel")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "The '{0}' should be between {2} and {1} characters.")]
        public string Model { get; set; }

        /// <summary>
        /// Measured in inches
        /// </summary>
        [Required]
        [AllowHtml]
        public double MonitorSize { get; set; }

        /// <summary>
        /// Measured in GB
        /// </summary>
        [Required]
        [AllowHtml]
        public int HardDiskSize { get; set; }

        /// <summary>
        /// Measured in GB
        /// </summary>
        [Required]
        [AllowHtml]
        public int RamMemorySize { get; set; }

        [Required]
        [AllowHtml]
        [UIHint("LaptopModel")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// In Chinese Yuan
        /// </summary>
        [Required]
        [AllowHtml]
        public decimal Price { get; set; }

        /// <summary>
        /// Measured in Kilos
        /// </summary>
        [ScaffoldColumn(false)]
        public double? Weight { get; set; }

        /// <summary>
        /// Camera, Bluethoot etc.
        /// </summary>
        [ScaffoldColumn(false)]
        public string AdditionalParts { get; set; }

        [ScaffoldColumn(false)]
        public string Description { get; set; }

        public virtual ICollection<Vote> Votes
        {
            get { return votes; }
            set { votes = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return comments; }
            set { comments = value; }
        }
    }
}

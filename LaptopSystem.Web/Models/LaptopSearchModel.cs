using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaptopSystem.Web.Models
{
    public class LaptopSearchModel
    {
        public int? Page { get; set; }

        [StringLength(30, ErrorMessage = "The '{0}' should not be more than {1} characters.")]
        [Display(Name="By model")]
        [AllowHtml]
        public string ByModel { get; set; }

        public int? ByManufacturer { get; set; }

        public decimal? ByPrice { get; set; }

        public IEnumerable<LaptopViewModel> Laptops { get; set; }

        public IEnumerable<SelectListItem> Manufacturers { get; set; }
    }
}
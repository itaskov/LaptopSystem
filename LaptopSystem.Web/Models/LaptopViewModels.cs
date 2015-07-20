using LaptopSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Microsoft.AspNet.Identity;

namespace LaptopSystem.Web.Models
{
    public class LaptopViewModel
    {
        public static Expression<Func<Laptop, LaptopViewModel>> FromLaptop
        {
            get
            {
                return laptop => new LaptopViewModel
                {
                    Id = laptop.Id,
                    ImageUrl = laptop.ImageUrl,
                    Manufacturer = laptop.Manufacturer.Name,
                    Model = laptop.Model,
                    Price = laptop.Price
                };
            }
        }

        public int Id { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        //public LaptopSearchModel LaptopSearchModel { get; set; }
    }

    public class LaptopDetailsViewModel
    {

        public static Expression<Func<Laptop, LaptopDetailsViewModel>> FromLaptop
        {
            get
            {
                string userId = HttpContext.Current.User.Identity.GetUserId();

                return laptop => new LaptopDetailsViewModel
                {
                    Id = laptop.Id,
                    Manufacturer = laptop.Manufacturer.Name,
                    Comments = laptop.Comments
                            .Select(c => new CommentViewModel { Author = c.Author.UserName, Content = c.Content }),
                    Model = laptop.Model,
                    MonitorSize = laptop.MonitorSize,
                    HardDiskSize = laptop.HardDiskSize,
                    RamMemorySize = laptop.RamMemorySize,
                    ImageUrl = laptop.ImageUrl,
                    Price = laptop.Price,
                    Weight = laptop.Weight,
                    AdditionalParts = laptop.AdditionalParts,
                    Description = laptop.Description,
                    UserCanVote = !laptop.Votes.Any(v => v.ApplicationUserId == userId),
                    Votes = laptop.Votes.Where(v => v.LaptopId == laptop.Id).Count()
                };
            }
        }

        public int Id { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public double MonitorSize { get; set; }

        public int HardDiskSize { get; set; }

        public int RamMemorySize { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public double? Weight { get; set; }

        public string AdditionalParts { get; set; }

        public string Description { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public bool UserCanVote { get; set; }

        public int Votes { get; set; }
    }
}

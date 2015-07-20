namespace LaptopSystem.Data.Migrations
{
    using LaptopSystem.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(LaptopSystem.Data.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //context.Configuration.LazyLoadingEnabled = true;

            // Manufacturers, Laptops
            context.Manufacturers.AddOrUpdate(
                man => man.Name,
                new Manufacturer
                {
                    Name = "Assus",
                    Laptops = new List<Laptop>
                    {
                        new Laptop
                        {
                            HardDiskSize = 500,
                            ImageUrl = @"~/images/laptopImage.jpg",
                            Model = "Eye PC",
                            MonitorSize = 15.6,
                            Price = 1600,
                            RamMemorySize = 6,
                        },
                        new Laptop
                        {
                            HardDiskSize = 750,
                            ImageUrl = @"~/images/laptopImage.jpg",
                            Model = "Eye PC2",
                            MonitorSize = 15.6,
                            Price = 1820,
                            RamMemorySize = 8,
                        },
                        new Laptop
                        {
                            HardDiskSize = 750,
                            ImageUrl = @"~/images/laptopImage.jpg",
                            Model = "VivoBook",
                            MonitorSize = 15.6,
                            Price = 2200,
                            RamMemorySize = 8,
                        },
                        new Laptop
                        {
                            HardDiskSize = 750,
                            ImageUrl = @"~/images/laptopImage.jpg",
                            Model = "VivoBook 2",
                            MonitorSize = 17.4,
                            Price = 2400,
                            RamMemorySize = 8,
                        },
                        new Laptop
                        {
                            HardDiskSize = 750,
                            ImageUrl = @"~/images/laptopImage.jpg",
                            Model = "X200",
                            MonitorSize = 17.4,
                            Price = 2400,
                            RamMemorySize = 8,
                        }
                    }
                },
                new Manufacturer
                {
                    Name = "HP",
                    Laptops = new List<Laptop>
                    {
                        new Laptop
                        {
                            HardDiskSize = 500,
                            ImageUrl = @"~/images/laptopImage.jpg",
                            Model = "EliteBook",
                            MonitorSize = 15.6,
                            Price = 1600,
                            RamMemorySize = 6,
                        },
                        new Laptop
                        {
                            HardDiskSize = 750,
                            ImageUrl = @"~/images/laptopImage.jpg",
                            Model = "EliteBook 2",
                            MonitorSize = 15.6,
                            Price = 1820,
                            RamMemorySize = 8,
                        },
                        new Laptop
                        {
                            HardDiskSize = 750,
                            ImageUrl = @"~/images/laptopImage.jpg",
                            Model = "ProBook",
                            MonitorSize = 15.6,
                            Price = 2200,
                            RamMemorySize = 8,
                        },
                        new Laptop
                        {
                            HardDiskSize = 750,
                            ImageUrl = @"~/images/laptopImage.jpg",
                            Model = "ProBook 2",
                            MonitorSize = 17.4,
                            Price = 2400,
                            RamMemorySize = 8,
                        },
                        new Laptop
                        {
                            HardDiskSize = 750,
                            ImageUrl = @"~/images/laptopImage.jpg",
                            Model = "Spectre",
                            MonitorSize = 17.4,
                            Price = 2400,
                            RamMemorySize = 8,
                        }
                    }
                }
                );

            // Users and Roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Users"))
            {
                roleManager.Create(new IdentityRole("Users"));
            }

            var user = new ApplicationUser { UserName = "itaskov", Email = "itaskov@gmail.com", };


            if (userManager.FindByName("itaskov") == null)
            {
                var result = userManager.Create(user, "123456");

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Users");
                }

            }

            if (!roleManager.RoleExists("Administrators"))
            {
                roleManager.Create(new IdentityRole("Administrators"));
            }

            user = new ApplicationUser { UserName = "admin", Email = "admin@gmail.com", };


            if (userManager.FindByName("admin") == null)
            {
                var result = userManager.Create(user, "123456");

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Administrators");
                }

            }

            // Votes
            if (context.Votes.Count() == 0)
            {
                var userName = userManager.FindByName("itaskov");
                var rnd = new Random();
                if (userName != null)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        context.Votes.Add(
                                new Vote
                                {
                                    ApplicationUserId = userName.Id,
                                    LaptopId = rnd.Next(1, 11)
                                }
                                );
                    }
                }

                userName = userManager.FindByName("admin");
                if (userName != null)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        context.Votes.Add(
                                new Vote
                                {
                                    ApplicationUserId = userName.Id,
                                    LaptopId = rnd.Next(1, 11)
                                }
                                );
                    }
                }
            }

            // Comments
            if (context.Comments.Count() == 0)
            {
                var userName = userManager.FindByName("itaskov");
                var rnd = new Random();
                if (userName != null)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        context.Comments.Add(
                                new Comment
                                {
                                    ApplicationUserId = userName.Id,
                                    LaptopId = rnd.Next(1, 11),
                                    Content = "This model is good " + i.ToString()
                                }
                                );
                    }
                }

                userName = userManager.FindByName("admin");
                if (userName != null)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        context.Comments.Add(
                                new Comment
                                {
                                    ApplicationUserId = userName.Id,
                                    LaptopId = rnd.Next(1, 11),
                                    Content = "This model is good " + i.ToString()
                                }
                                );
                    }
                }
            }
        }
    }
}

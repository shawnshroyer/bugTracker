namespace bugTracker.Migrations
{
    using bugTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<bugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(bugTracker.Models.ApplicationDbContext context)
        {
            //Create Roles
            var roleManger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Administrator"))
            {
                roleManger.Create(new IdentityRole { Name = "Administrator" });
            }
            if (!context.Roles.Any(r => r.Name == "Project Manager"))
            {
                roleManger.Create(new IdentityRole { Name = "Project Manager" });
            }
            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManger.Create(new IdentityRole { Name = "Developer" });
            }
            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManger.Create(new IdentityRole { Name = "Submitter" });
            }

            //Create Users
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "ShawnShroyer@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "ShawnShroyer@mailinator.com",
                    Email = "ShawnShroyer@mailinator.com",
                    FirstName = "Shawn",
                    LastName = "Shroyer",
                    DisplayName = "Shawn Shroyer",
                    Avatar = ""
                }, "testUser123");
            }

            if (!context.Users.Any(u => u.Email == "JasonTwichell@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "JasonTwichell@mailinator.com",
                    Email = "JasonTwichell@mailinator.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "Twitch",
                    Avatar = ""
                }, "Abc&123!");
            }
 
            if (!context.Users.Any(u => u.Email == "pm@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "pm@mailinator.com",
                    Email = "pm@mailinator.com",
                    FirstName = "Project",
                    LastName = "Manager",
                    DisplayName = "PM Supreme",
                    Avatar = ""
                }, "*pmUser123");
            }

            if (!context.Users.Any(u => u.Email == "developer@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "developer@mailinator.com",
                    Email = "developer@mailinator.com",
                    FirstName = "Lonely",
                    LastName = "Developer",
                    DisplayName = "Developer Supreme",
                    Avatar = ""
                }, "*devUser123");
            }

            if (!context.Users.Any(u => u.Email == "submitter@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "submitter@mailinator.com",
                    Email = "submitter@mailinator.com",
                    FirstName = "Sub",
                    LastName = "Mitter",
                    DisplayName = "Submit",
                    Avatar = ""
                }, "*subUser123");
            }

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            // Assign a Role to a user
            var userId = userManager.FindByEmail("ShawnShroyer@mailinator.com").Id;
            userManager.AddToRole(userId, "Administrator");

            userId = userManager.FindByEmail("JasonTwichell@mailinator.com").Id;
            userManager.AddToRole(userId, "Administrator");

            userId = userManager.FindByEmail("pm@mailinator.com").Id;
            userManager.AddToRole(userId, "Project Manager");

            userId = userManager.FindByEmail("developer@mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            userId = userManager.FindByEmail("submitter@mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");

            //TODO: add these items for seeding
            //TicketTypeId
            //TicketPriorityId
            //TicketStatusId

            //if (!context.TicketTypes.Any(r => r.Type == ""))
            //{
            //    roleManger.Create(new IdentityRole { Name = "" });
            //}


            if (!context.Users.Any(u => u.Email == "DemoAdmin@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoAdmin@mailinator.com",
                    Email = "DemoAdmin@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Admin",
                    DisplayName = "Admin Supreme",
                    Avatar = ""
                }, "*demoAdmin123");
            }

            if (!context.Users.Any(u => u.Email == "DemoPM@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoPM@mailinator.com",
                    Email = "DemoPM@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Manager",
                    DisplayName = "PM Supreme",
                    Avatar = ""
                }, "*demoPM123");
            }
      
            if (!context.Users.Any(u => u.Email == "DemoDeveloper@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoDeveloper@mailinator.com",
                    Email = "DemoDeveloper@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Developer",
                    DisplayName = "Developer Supreme",
                    Avatar = ""
                }, "*demoDeveloper123");
            }

            if (!context.Users.Any(u => u.Email == "DemoSubmitter@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoSubmitter@mailinator.com",
                    Email = "DemoSubmitter@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Submitter",
                    DisplayName = "Submitter",
                    Avatar = ""
                }, "*demoSubmitter123");
            }

            userId = userManager.FindByEmail("DemoAdmin@mailinator.com").Id;
            userManager.AddToRole(userId, "Administrator");

            userId = userManager.FindByEmail("DemoPM@mailinator.com").Id;
            userManager.AddToRole(userId, "Project Manager");

            userId = userManager.FindByEmail("DemoDeveloper@mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            userId = userManager.FindByEmail("DemoSubmitter@mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");
        }
    }
}

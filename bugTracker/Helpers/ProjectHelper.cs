using bugTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace bugTracker.Helpers
{
    public class ProjectsHelper
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper URHelper = new UserRolesHelper();

        public bool IsUserOnProject(string userId, int projectId)
        {
            var project = db.Projects.Find(projectId);
            var flag = project.Users.Any(u => u.Id == userId);

            return (flag);
        }

        public ICollection<Project> ListUserProjects(string userId)
        {
            ApplicationUser user = db.Users.Find(userId);
            var projects = user.Projects.ToList();

            return (projects);
        }

        public void AddUserToProject(string userId, int projectId)
        {
            if (!IsUserOnProject(userId, projectId))
            {
                Project proj = db.Projects.Find(projectId);
                var newUser = db.Users.Find(userId);

                proj.Users.Add(newUser);
                db.SaveChanges();
            }
        }

        public void RemoveUserFromProject(string userId, int projectId)
        {
            if (IsUserOnProject(userId, projectId))
            {
                Project proj = db.Projects.Find(projectId);
                var delUser = db.Users.Find(userId);
                proj.Users.Remove(delUser);

                db.Entry(proj).State = EntityState.Modified; // just saves this obj instance.
                db.SaveChanges();
            }
        } 

        public ICollection<ApplicationUser> UsersOnProject(int projectId)
        {
            return db.Projects.Find(projectId).Users;
        }

        public ICollection<ApplicationUser> UsersNotOnProject(int projectId)
        {
            return db.Users.Where(u => u.Projects.All(p => p.Id != projectId)).ToList();
        }

        public string GetProjectManager(int projectId)
        {
            var projUsers = UsersOnProject(projectId);
            foreach (var user in projUsers)
            {
                if (URHelper.IsUserInRole(user.Id, "Project Manager"))
                {
                    return $"{user.LastName}, {user.FirstName}";
                }
            }
            return "Unassigned";
        }

        public ICollection<ApplicationUser> UsersNotOnProjOrInRole(int projectId, string role)
        {
            var userList = UsersNotOnProject(projectId).ToList();

            foreach (var usr in URHelper.UsersInRole(role))
            {
                var count = userList.RemoveAll(u => u.Id == usr.Id);
            }

            return userList;
        }

        public ICollection<ApplicationUser> UsersInProjNotInRole(int projectId, string role)
        {
            var userList = UsersOnProject(projectId).ToList();

            foreach (var usr in URHelper.UsersInRole(role))
            {
                var count = userList.RemoveAll(u => u.Id == usr.Id);
            }

            return userList;
        }

        public string PMforTicket(int id)
        {
            foreach (var user in UsersOnProject(id))
            {
                if (IsUserOnProject(user.Id, id) && URHelper.IsUserInRole(user.Id, "Project Manager"))
                {
                    return $"{user.FirstName} {user.LastName}";
                }
            }

            return "Not Assigned";
        }

        public string PMIdforTicket(int id)
        {
            foreach (var user in UsersOnProject(id))
            {
                if (IsUserOnProject(user.Id, id) && URHelper.IsUserInRole(user.Id, "Project Manager"))
                {
                    return user.Id;
                }
            }

            return "Not Assigned";
        }
    }
}

            //foreach (var pm in userHelper.UsersInRole("Project Manager"))
            //{
            //    if(projectHelper.IsUserOnProject(pm.Id, id))
            //    {
            //        ViewBag.CurrentPm = pm.UserName;
            //    }
            //}
using bugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bugTracker.Helpers
{
    public class DashBoardHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProjectsHelper projectsHelper = new ProjectsHelper();
        private UserRolesHelper userRolesHelper = new UserRolesHelper();

        public int TicketCount()
        {
            return db.Tickets.Count();
        }

        public int ProjectCount()
        {
            return db.Projects.Count();
        }

        public int ProjectCountForUser(string userId)
        {
            return projectsHelper.ListUserProjects(userId).Count();
        }

        public int TicketCountForUser(string userId)
        {
            int count = 0;
            if (userRolesHelper.IsUserInRole(userId, "Developer"))
            {
                count = db.Tickets.Where(t => t.AssignedToUserId == userId).Count();
            }

            if (userRolesHelper.IsUserInRole(userId, "Project Manager"))
            {
                foreach (var project in projectsHelper.ListUserProjects(userId))
                {
                    count += db.Tickets.Where(t => t.ProjectId == project.Id).Count();
                }
            }

            if (userRolesHelper.IsUserInRole(userId, "Submitter"))
            {
                count = db.Tickets.Where(t => t.OwnerUserId == userId).Count();
            }

            return count;
        }

        public ICollection<bugTracker.Models.Ticket> MostRecentTickets()
        {
            return db.Tickets.OrderByDescending(t => t.Created).Take(10).ToList();
        }

        public ICollection<bugTracker.Models.Project> MostRecentProjects()
        {
            return db.Projects.OrderByDescending(p => p.Created).Take(10).ToList();
        }
    }
}
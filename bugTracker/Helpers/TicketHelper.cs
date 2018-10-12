using bugTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

//using System.Data;
//using System.Net;
//using System.Web.Mvc;
//using bugTracker.Extensions;
//using bugTracker.Helpers;


//  ViewBag.CurrentDeveloper = projectHelper.
//  ViewBag.CurrentOwner = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
//  ViewBag.CurrentProject = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
//  ViewBag.CurrentPriority = new SelectList(db.TicketPriorities, "Id", "Priority", ticket.TicketPriorityId);
//  ViewBag.CurrentStatus = new SelectList(db.TicketStatus, "Id", "Status", ticket.TicketStatusId);
//  ViewBag.CurrentType = new SelectList(db.TicketTypes, "Id", "Type", ticket.TicketTypeId);



namespace bugTracker.Helpers
{
    public class TicketHelper
    {
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper UserHelper = new UserRolesHelper();

        //public string Ticket()
        //{

        //    var tickets = db.Tickets.Include(t => t.AssignedToUser).Include(t => t.OwnerUser).Include(t => t.Project).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Include(t => t.TicketType);
        //}
    }
}
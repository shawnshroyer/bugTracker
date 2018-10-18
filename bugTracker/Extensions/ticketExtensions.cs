using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using bugTracker.Models;
using Microsoft.AspNet.Identity;

namespace bugTracker.Extensions
{
    public static class TicketExtensions
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static void RecordHistory (this Ticket ticket, Ticket oldTicket)
        {
            var propertyList = WebConfigurationManager.AppSettings["TrackedTicketProperties"].Split(',');

            foreach (PropertyInfo property in ticket.GetType().GetProperties())
            {
                if (!propertyList.Contains(property.Name))
                    continue;

                var value = property.GetValue(ticket, null) ?? "";
                var oldValue = property.GetValue(oldTicket, null) ?? "";

                if (value.ToString() != oldValue.ToString())
                {
                    var ticketHistory = new TicketHistory
                    {
                        Changed = DateTimeOffset.Now,
                        Property = property.Name,
                        NewValue = GetValueFromKey(property.Name, value),
                        OldValue = GetValueFromKey(property.Name, oldValue),
                        TicketId = ticket.Id,
                        UserId = HttpContext.Current.User.Identity.GetUserId()
                    };

                    db.TicketHistories.Add(ticketHistory);
                }
            }
            db.SaveChanges();
        }

        private static string GetValueFromKey(string keyName, object key)
        {
            var returnValue = "";

            if (string.IsNullOrEmpty(key.ToString()))
                return returnValue;

            switch (keyName)
            {
                case "ProjectId":
                    returnValue = db.Projects.Find(key).Name;
                    break;
                case "TicketTypeId":
                    returnValue = db.TicketTypes.Find(key).Type;
                    break;
                case "TicketPriorityId":
                    returnValue = db.TicketPriorities.Find(key).Priority;
                    break;
                case "TicketStatusId":
                    returnValue = db.TicketStatus.Find(key).Status;
                    break;
                case "OwnerUserId":
                case "AssignedToUserId":
                    returnValue = db.Users.Find(key).FirstName + " " + db.Users.Find(key).LastName;
                    break;
                default:
                    returnValue = key.ToString();
                    break;
            }

            return returnValue;
        }
    }
}
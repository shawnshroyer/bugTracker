using bugTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.IO;

namespace bugTracker.Helpers
{
    public class TicketHelper
    {
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper UserHelper = new UserRolesHelper();
        private ProjectsHelper ProjectHelper = new ProjectsHelper();

        public string TicketImage(string file)
        {
        //var tickets = db.Tickets.Include(t => t.AssignedToUser).Include(t => t.OwnerUser).Include(t => t.Project).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Include(t => t.TicketType);
        //Image files found at https://www.iconfinder.com/iconsets/file-extension-names-vol-5-1

            var ext = Path.GetExtension(file);
            switch (ext.ToLower())
            {
                case ".doc":
                    return "/images/icons/doc.png";
                case ".docx":
                    return "/images/icons/docx.png";
                case ".xls":
                    return "/images/icons/xls.png";
                case ".xlsx":
                    return "/images/icons/xlsx.png";
                case ".ppt":
                case ".pptx":
                    return "/images/icons/ppt.png";
                case ".txt":
                    return "/images/icons/txt.png";
                case ".vsd":
                case ".vsdx":
                    return "/images/icons/visual.png";
                case ".pdf":
                    return "/images/icons/pdf.png";
                case ".jpg":
                case ".jpeg":
                case ".bmp":
                case ".png":
                case ".gif":
                case ".tiff":
                    return file;
                default:
                    return "/images/icons/default.png";
            }

        }

        public bool IsUserTicket(string userId, int ticketId, int projectId)
        {
            bool result = false;
            string pmId = ProjectHelper.PMIdforTicket(projectId);

            if (pmId == "Not Assigned")
            {
                result = db.Tickets.All(t => t.Id == ticketId && (t.OwnerUserId == userId || t.AssignedToUserId == userId));
            } else
            {
                return true;
            }

            return result;
        }
    }
}
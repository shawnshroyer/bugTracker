using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bugTracker.Models
{
    public class TicketAttachment
    {
        //Primary Key
        public int Id { get; set; }

        //Properties
        public string Title { get; set; }
        public String Description { get; set; }
        public string FilePath { get; set; }
        public string FileUrl { get; set; }
        public DateTimeOffset Created { get; set; }

        //Foreign Keys
        public int TicketId { get; set; }
        public string UserId { get; set; }

        //Navigation - From Parent
        public virtual Ticket Ticket  { get; set; }
        public virtual  ApplicationUser User { get; set; }

    }
}
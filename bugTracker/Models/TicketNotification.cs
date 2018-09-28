using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bugTracker.Models
{
    public class TicketNotification
    {
        //Primary Key
        public int Id { get; set; }

        //Properties
        public DateTimeOffset Sent { get; set; }
        public String Subject { get; set; }
        public string Body { get; set; }
        public string Status { get; set; }
        public bool IsRead { get; set; }

        //Foreign Keys
        public int TicketId { get; set; }
        public string UserId { get; set; }

        //Navigation - From Parent
        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
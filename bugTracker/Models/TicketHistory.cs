using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bugTracker.Models
{
    public class TicketHistory
    {
        //Primary Key
        public int Id { get; set; }

        //Properties
        public String Property { get; set; }
        public String OldValue { get; set; }
        public String NewValue { get; set; }
        public DateTimeOffset Changed { get; set; }

        //Foreign Keys
        public int TicketId { get; set; }
        public string UserId { get; set; }

        //Navigation - From Parent
        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
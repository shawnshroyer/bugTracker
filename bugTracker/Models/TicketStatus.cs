using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bugTracker.Models
{
    public class TicketStatus
    {
        //Primary Key
        public int Id { get; set; }

        //Properties
        public String Status { get; set; }

        //Navigation - To Children
        public virtual ICollection<Ticket> Tickets { get; set; }

        public TicketStatus()
        {
            Tickets = new HashSet<Ticket>();
        }
    }
}
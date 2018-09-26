using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bugTracker.Models
{
    public class TicketPriority
    {
        //Primary Key
        public int Id { get; set; }

        //Properties
        public String Priority { get; set; }

        //Navigation - To Children
        public virtual ICollection<Ticket> Tickets { get; set; }

        public TicketPriority()
        {
            Tickets = new HashSet<Ticket>();
        }
    }
}

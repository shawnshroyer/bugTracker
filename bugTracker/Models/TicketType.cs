using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bugTracker.Models
{
    public class TicketType
    {
        //Primary Key
        public int Id { get; set; }

        //Properties
        public String Type { get; set; }

        //Navigation - To Children
        public virtual ICollection<Ticket> Tickets { get; set; }

        public TicketType()
        {
            Tickets = new HashSet<Ticket>();
        }
    }
}
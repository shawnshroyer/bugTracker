using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bugTracker.Models
{
    public class Project
    {
        //Primary Key
        public int Id { get; set; }

        //Properties
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Details { get; set; }

        public DateTimeOffset Created { get; set; }

        //Foreign Keys

        //Navigation - To Children
        public virtual ICollection<Ticket> Tickets { get; set; }

        //Virtual Nav for the many to many table with ApplicationUser
        public virtual ICollection<ApplicationUser> Users { get; set; }

        public Project()
        {
            Tickets = new HashSet<Ticket>();
            Users = new HashSet<ApplicationUser>();
        }
    }
}
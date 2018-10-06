using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bugTracker.ViewModels
{
    public class ProfileUpdateViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
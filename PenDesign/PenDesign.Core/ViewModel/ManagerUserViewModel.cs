using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PenDesign.Core.Model;


namespace PenDesign.Core.ViewModel
{
    public class ManagerUserViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SecurityStamp { get; set; }
        public string Email { get; set; }
        public UserInfo UserInfo { get; set; }
        //public IEnumerable<IdentityRole> Roles { get; set; }
        public List<IdentityRole> Roles { get; set; }
    }
}
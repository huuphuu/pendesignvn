using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PenDesign.Core.Models;
using PenDesign.Data.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Mvc;

namespace PenDesign.Data
{
    public class UserFactory: Controller
    {
        private IDataContext _dataContext;
        private readonly IDatabaseFactory _databaseFactory;
        private UserManager<ApplicationUser> _userManager;

        protected IDatabaseFactory DatabaseFactory
        {
            get { return _databaseFactory; }
        }

        protected IDataContext DataContext
        {
            get { return _dataContext ?? (_dataContext = _databaseFactory.Get()); }
        }

        public UserFactory(IDatabaseFactory databaseFactory)
        {
            this._databaseFactory = databaseFactory;
            var applicationDbContext = new ApplicationDbContext();
            var UserStore = new UserStore<ApplicationUser>(applicationDbContext);
            this._userManager = new UserManager<ApplicationUser>(UserStore);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        }

        public string GetUserId(string name)
        {
            return _userManager.FindByName(name).Id;
        }
    }
}

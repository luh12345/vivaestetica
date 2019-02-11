using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.Repository
{
    public class UserContext : IUserContext
    {
        private readonly IdentityDbContext<TB_CLIENTE> Identity;
        private readonly UserStore<TB_CLIENTE> UserStore;
        private readonly UserManager<TB_CLIENTE> UserManager;

        public UserContext()
        {
            Identity = new IdentityDbContext<TB_CLIENTE>("VivaEsteticaConnection");
            UserStore = new UserStore<TB_CLIENTE>(Identity);
            UserManager = new UserManager<TB_CLIENTE>(UserStore);
        }

        public async Task<IEnumerable<string>> AddUser(TB_CLIENTE user, string password)
        {
            IdentityResult result = await UserManager.CreateAsync(user, password);
            return result.Errors;
        }
    }
}
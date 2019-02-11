using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngolaPrev.VivaEstetica.MVC.Repository
{
    public interface IUserContext
    {
        Task<IEnumerable<string>> AddUser(TB_CLIENTE user, string password);
    }
}
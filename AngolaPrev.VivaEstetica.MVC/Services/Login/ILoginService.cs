using AngolaPrev.VivaEstetica.MVC.Models.Login;

namespace AngolaPrev.VivaEstetica.MVC.Services.Login
{
    public interface ILoginService
    {
        void Login(LoginViewModel model);
        void Logout();
    }
}
﻿using AngolaPrev.VivaEstetica.MVC.Models.Clientes;
using AngolaPrev.VivaEstetica.MVC.Models.Login;

namespace AngolaPrev.VivaEstetica.MVC.Services.User
{
    public interface IUserService
    {
        void Register(ClienteViewModel model);
        void Login(LoginViewModel model);
        void Logout();
    }
}
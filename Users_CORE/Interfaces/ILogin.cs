using System;
using System.Collections.Generic;
using System.Text;

namespace Users_CORE.Interfaces
{
    public interface ILogin : IDisposable
    {
        Models.UserModel Login(Models.LoginMinModel user);
    }
}

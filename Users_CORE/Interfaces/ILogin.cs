using System;
using System.Collections.Generic;
using System.Text;

namespace Users_CORE.Interfaces
{
    public interface ILogin : IDisposable
    {
        Models.LoginModel Login(Models.LoginMinModel user);
    }
}

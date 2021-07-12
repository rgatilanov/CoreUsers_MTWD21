using System;
using System.Collections.Generic;
using System.Text;

namespace Users_CORE.Interfaces
{
    public interface IUser: IDisposable
    {
        List<Models.UserModel> GetUsers();
        Models.UserModel GetUser(int ID);
        void AddUser(Models.UserModel model);
        void UpdateUser(Models.UserModel model);
        void DeleteUser(int ID);
    }
}

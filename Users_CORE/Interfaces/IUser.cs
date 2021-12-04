using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Users_CORE.Interfaces
{
    public interface IUser: IDisposable
    {
        List<Models.UserModel> GetUsers();
        List<Models.UserModel> GetUsers(string SQLstatement, CommandType commandType);
        Models.UserModel GetUser(int ID);
        long AddUser(Models.UserModel model);
        bool UpdateUser(Models.UserModel model);
        void DeleteUser(int ID);
    }
}

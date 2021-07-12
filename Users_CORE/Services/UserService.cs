﻿using CORE.Connection.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Users_CORE.Interfaces;
using Users_CORE.Models;

namespace Users_CORE.Services
{
    public class UserService: IUser,IDisposable
    {
        private bool disposedValue;
        private IConnectionDB<UserModel> _conn;
        private List<Tuple<string, object, int>> _parameters = new List<Tuple<string, object, int>>();

        public UserService(IConnectionDB<UserModel> conn)
        {
            _conn = conn;
        }

        public List<Models.UserModel> GetUsers()
        {
            throw new Exception();
        }
        public Models.UserModel GetUser(int ID)
        {
            try
            {
                UserModel UsuarioResp = null;

                _parameters.Add(new Tuple<string, object, int>("@Id", ID, 0));

                _conn.PrepararProcedimiento("dbo.[USERS.Get_Id]", _parameters);

                DataTableReader DTRResultados = _conn.EjecutarTableReader();
                while (DTRResultados.Read())
                {
                    UsuarioResp = new UserModel()
                    {
                        Identificador = ID,
                        Name = DTRResultados["Name"].ToString(),
                        LastName = DTRResultados["LastName"].ToString(),
                        Nick = DTRResultados["Nick"].ToString(),
                        CreateDate = DateTime.Parse(DTRResultados["CreateDate"].ToString()),

                    };
                }

                return UsuarioResp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _parameters.Clear();
            }
        }
        public void AddUser(Models.UserModel model)
        {
            throw new Exception();
        } 
        public void UpdateUser(Models.UserModel model)
        {
            throw new Exception();
        } 
        public void DeleteUser(int ID)
        {
            throw new Exception();
        } 

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _conn.Dispose();// TODO: eliminar el estado administrado (objetos administrados)
                }

                // TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
                // TODO: establecer los campos grandes como NULL
                disposedValue = true;
            }
        }

        // // TODO: reemplazar el finalizador solo si "Dispose(bool disposing)" tiene código para liberar los recursos no administrados
        // ~MinervaService()
        // {
        //     // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
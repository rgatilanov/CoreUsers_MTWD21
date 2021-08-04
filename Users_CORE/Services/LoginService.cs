﻿using CORE.Connection.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Users_CORE.Interfaces;
using Users_CORE.Models;

namespace Users_CORE.Services
{
    public class LoginService: ILogin,IDisposable
    {
        private bool disposedValue;
        private IConnectionDB<LoginModel> _conn;
        private List<Tuple<string, object, int>> _parameters = new List<Tuple<string, object, int>>();
        public LoginService(IConnectionDB<LoginModel> conn)
        {
            _conn = conn;
        }

        public Models.LoginModel Login(Models.LoginMinModel user)
        {
            try
            {
                LoginModel model = new LoginModel();
                _parameters.Add(new Tuple<string, object, int>("@p_login_json", JsonConvert.SerializeObject(user), 12));
                _conn.PrepararProcedimiento("dbo.[USERS.Login]", _parameters);

                DataTableReader DTRResultados = _conn.EjecutarTableReader();
                while (DTRResultados.Read())
                {
                    var Json = DTRResultados["Usuario"].ToString();
                    if (Json != string.Empty)
                    {
                        JArray arr = JArray.Parse(Json);
                        foreach (JObject jsonOperaciones in arr.Children<JObject>())
                        {
                            //user = JsonConvert.DeserializeObject<User>(jsonOperaciones);
                            model = new LoginModel()
                            {
                                Id = Convert.ToInt32(jsonOperaciones["Id"].ToString()),
                                Name = jsonOperaciones["Name"].ToString(),
                                LastName = jsonOperaciones["LastName"].ToString(),
                            };

                        }
                    }
                }

                return model;
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

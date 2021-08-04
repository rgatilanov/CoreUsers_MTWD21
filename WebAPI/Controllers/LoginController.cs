using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users_CORE.Interfaces;
using Users_CORE.Models;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly ILogin _login;
        private readonly IConfiguration _configuration;
        public LoginController(ILogin login, IConfiguration configuration)
        {
            _login = login;
            _configuration = configuration;
        }

        //https://localhost:5001/Login
        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult<LoginMinModel> Post(LoginMinModel login)
        {
            if (string.IsNullOrEmpty(login.Nick))
                throw new NullReferenceException("Nick vacío, el campo es necesario");
            if (string.IsNullOrEmpty(login.Pass))
                throw new NullReferenceException("Password vacío, el campo es necesario");

            LoginModel model = new LoginModel();
            using (ILogin User = Users_CORE.Services.FactorizerService.Login(Users_CORE.Models.EServer.LOCAL))
            {
                model = User.Login(login);
            }
            model.Token = TokenGenerator.GenerateTokenJwt(model.Name, model.Id, _configuration);
            return Ok(model); 
        }
    }
}

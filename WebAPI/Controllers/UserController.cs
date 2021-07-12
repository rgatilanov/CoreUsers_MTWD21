using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users_CORE.Interfaces;

namespace WebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }

        [HttpGet]
        public ActionResult<Users_CORE.Models.UserModel> GetUser(int ID)
        {
            if (ID == 0)
                return BadRequest("Ingrese un ID válido");

            Users_CORE.Models.UserModel model = new Users_CORE.Models.UserModel();
            using (IUser User = Users_CORE.Services.FactorizerService.Inicializar(Users_CORE.Models.EServer.LOCAL))
            {
                model = User.GetUser(ID);
            }

            return model;
        }
    }
}

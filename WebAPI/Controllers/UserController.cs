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

        ///https://localhost:5001/api/User/GetUsers
        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Users_CORE.Models.UserModel> GetUsers()
        {
            List<Users_CORE.Models.UserModel> model = new List<Users_CORE.Models.UserModel>();
            using (IUser User = Users_CORE.Services.FactorizerService.Inicializar(Users_CORE.Models.EServer.LOCAL))
            {
                model = User.GetUsers();
            }

            return model;
        }

        //https://localhost:5001/api/User/GetUser?ID=2
        [HttpGet]
        [Route("GetUser")]
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

        //https://localhost:5001/api/User/AddUser
        [HttpPost]
        [Route("[action]")]
        public ActionResult AddUser(Users_CORE.Models.UserModel user)
        {
            if (user == null)
                return BadRequest("Ingrese información de usuario");

            long model = 0;
            using (IUser User = Users_CORE.Services.FactorizerService.Inicializar(Users_CORE.Models.EServer.LOCAL))
            {
                model = User.AddUser(user);
            }

            return model > 0 ? Ok() : BadRequest("Error al insertar");
        }

        //https://localhost:5001/api/User/UpdateUser
        [HttpPost]
        [Route("[action]")]
        public ActionResult UpdateUser(Users_CORE.Models.UserModel user)
        {
            if (user.Identificador == 0)
                return BadRequest("Ingrese un ID válido");

            bool model = false;
            using (IUser User = Users_CORE.Services.FactorizerService.Inicializar(Users_CORE.Models.EServer.LOCAL))
            {
                model = User.UpdateUser(user);
            }

            return model == true ? Ok() : BadRequest("Error al actualizar");
        }

        //https://localhost:5001/api/user/deleteuser?ID=2
        [HttpDelete]
        [Route("[action]")]
        public ActionResult DeleteUser(int ID)
        {
            if (ID == 0)
                return BadRequest("Ingrese un ID válido");

            using (IUser User = Users_CORE.Services.FactorizerService.Inicializar(Users_CORE.Models.EServer.LOCAL))
            {
                User.DeleteUser(ID);
            }

            return Ok();
        }
    }
}

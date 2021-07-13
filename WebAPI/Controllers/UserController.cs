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
    }
}

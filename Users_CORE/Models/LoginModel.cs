using System;
using System.Collections.Generic;
using System.Text;

namespace Users_CORE.Models
{
    public class LoginModel: BaseModel
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string Token { get; set; }
    }

    public class LoginMinModel
    {
        public string Nick { get; set; }
        public string Pass { get; set; }
    }
}

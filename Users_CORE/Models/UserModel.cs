using System;
using System.Collections.Generic;
using System.Text;

namespace Users_CORE.Models
{
    public class UserModel : BaseModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime CreateDate { get; set; }
        public Boolean Status { get; set; }
    }
}

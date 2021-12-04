using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users_CORE.Models
{
    public enum EServer : int
    {
        UDEFINED = 0,
        CLOUD = 1,
        LOCAL_SQLSERVER = 2,
        LOCAL_POSTGRESQL = 3
    }
}

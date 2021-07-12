﻿using CORE.Connection;
using CORE.Connection.Interfaces;
using CORE.Connection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_CORE.Tools;

namespace Users_CORE.Configuration
{
    public class BridgeDBConnection<T>
    {
        public static IConnectionDB<T> Create(string ConnectionString, DbEnum DB)
        {
            return Factorizer<T>.Create(EncryptTool.Decrypt(ConnectionString), DB);
        }
    }
}

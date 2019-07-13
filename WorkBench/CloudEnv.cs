using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WorkBench
{
    class CloudEnv
    {
        public string envPath;
        public DbType dbType;
        public String dbHost;
        public String dbPort;
        public string dbName;
        public string dbUserName;
        public string dbPassword;

    }
    public enum DbType
    {
        SQLServer = 0,
        Oracle = 1,
        PgSQL = 2,
        MySQL = 3,
        DM = 4,
        Unknown = 255
    }
}

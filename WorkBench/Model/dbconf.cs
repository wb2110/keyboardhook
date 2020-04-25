using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkBench.entity
{
    class dbconf
    {
        public String dbHost;
        public String dbPort;
        public string dbName;
        public string dbUserName;
        public string dbPassword;
        public DbType dbType;
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

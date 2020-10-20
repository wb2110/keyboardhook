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

        public static CloudEnv GetDefault()
        {
            var env = new CloudEnv();
            env.envPath = @"E:\gscloud";
            env.dbHost = @"z.gscloud.top";
            env.dbType = DbType.PgSQL;
            env.dbName = "b4";
            env.dbPassword = "aaaaaa";
            env.dbUserName = "b4";
            env.dbPort = "5432";
            return env;
        }
        public static CloudEnv FromDict(Dictionary<String,object> dict) {

            var env = new CloudEnv();
            env.envPath = Environment.GetEnvironmentVariable("EnvPath");
            env.dbHost = dict["dbHost"].ToString();
            env.dbType = GetDbType(dict["dbType"].ToString());
            env.dbName = dict["dbName"].ToString();
            env.dbPassword = dict["dbPassword"].ToString();
            env.dbUserName = dict["dbUserName"].ToString();
            env.dbPort = dict["dbPort"].ToString();
            return env;
        }

        public static string GetDbStr(int enumID)
        {
            return  Enum.GetName(typeof(DbType), enumID) ;
        }

        /// <summary>
        /// 获取枚举Index(Id)(根据枚举字符串)
        /// </summary>
        public static int GetDbIndex(string enumName)
        {
            return Convert.ToInt32(Enum.Parse(typeof(DbType), enumName));
        }
        public static DbType GetDbType(int enumID)
        {
            return (DbType)Enum.ToObject(typeof(DbType), enumID);
        }
        public static DbType GetDbType(string enumName)
        {
            return GetDbType(GetDbIndex(enumName));
        }

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

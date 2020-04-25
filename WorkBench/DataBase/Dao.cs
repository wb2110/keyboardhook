
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using WorkBench;

namespace WorkBench.DataBase { 
    static class Dao
    {
        private static string dbFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"\conf.db");
        private static string cs = "Data Source=" + dbFile; // "Data Source=/path/to/file.db";
        private static readonly SQLiteHelper db=new SQLiteHelper(cs);

        public static  void Save(CloudEnv env)
        {
            var id = env.dbType.ToString() + env.dbHost + env.dbPort + env.dbName;
            var list = select("select * from DBCONF where id='" + id + "'");
            var dict = env.ToDictionary();
            dict.Add("id", id);
            dict.Add("updateDate", DateTime.Now);
            dict.Remove("envPath");
            if (list.Count > 0)
            {
                var cond = new Dictionary<string, object>();
                cond.Add("id", id);
                db.Update("DBCONF", dict, cond);
            }
            else
            {
                db.Insert("DBCONF", dict);     
            }
        }
        public static int ExecuteNoneQuery(string sql) {
            return db.Execute(sql);
        }
        public static void CreateTable() {
            var table = new SQLiteTable("DBCONF");
            var cols = new SQLiteColumnList();
            cols.Add(new SQLiteColumn("id",ColType.Text,true,false,true,""));
            cols.Add(new SQLiteColumn("dbType"));
            cols.Add(new SQLiteColumn("dbHost"));
            cols.Add(new SQLiteColumn("dbPort"));
            cols.Add(new SQLiteColumn("dbName"));
            cols.Add(new SQLiteColumn("dbUserName"));
            cols.Add(new SQLiteColumn("dbPassword"));
            cols.Add(new SQLiteColumn("updateDate",ColType.DateTime));
            table.SetCols(cols);
            db.CreateTable(table);

            table = new SQLiteTable("PATHCONF");
            cols = new SQLiteColumnList();
            cols.Add(new SQLiteColumn("code"));
            cols.Add(new SQLiteColumn("name"));
            cols.Add(new SQLiteColumn("value"));
            table.SetCols(cols);
            db.CreateTable(table);
        }
        public static List<Dictionary<string,object>> select(string sql)
        {
            var dt = db.Select(sql);
            return GetDict(dt);
        }
        public static CloudEnv GetDefaultEnv()
        {
            var list = select("select * from DBCONF order by updateDate desc");
            if (list.Count <= 0)
            {
               return CloudEnv.GetDefault();
            }
            return CloudEnv.FromDict(list[0]);
        }
        public static CloudEnv GetEnvByHostAndDbType(String host,int dbtype,ref int index)
        {
            var list = select("select * from DBCONF where dbHost='"+host+"' and dbType='"+ dbtype + "' order by updateDate desc");
            if (list.Count <= 0 && index == 0)
            {
                return null;
            }
            if (list.Count <= 0)
            {
                index++;
                return GetEnvByHostAndDbType(host, dbtype, ref index);
            }
            if (index >= list.Count)
            {
                index =0;
                var env = CloudEnv.FromDict(list[index]);
                index = -1;
                return env;
            }
            return CloudEnv.FromDict(list[index]);
        }
        public static void DeleteEnv(CloudEnv env) {
            var id = env.dbType.ToString() + env.dbHost + env.dbPort + env.dbName;
            ExecuteNoneQuery("delete from DBCONF where id='" + id + "'");
        }

        private static List<Dictionary<string, object>> GetDict(DataTable dt)
        {
            return dt.AsEnumerable().Select(
                row => dt.Columns.Cast<DataColumn>().ToDictionary(
                    column => column.ColumnName,
                    column => row[column]
                )).ToList();
        }

    }
}

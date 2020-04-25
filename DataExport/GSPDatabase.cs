using System;
using Genersoft.Platform.Core.DataAccess;
using Genersoft.Platform.Core.DataAccess.Configuration;
using Genersoft.Platform.Core.DataAccess.PostgreSQL;

namespace DataExport
{
	// Token: 0x0200000A RID: 10
	public class GSPDatabase
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00004580 File Offset: 0x00002780
		private GSPDbConfigData GetDBConfigdata(string Provider, string Source, GSPDbType DbType, string Catalog, string UserId, string Password)
		{
			GSPDbConfigData gspdbConfigData = null;
			bool flag = DbType == GSPDbType.Oracle;
			if (flag)
			{
				gspdbConfigData = new OracleDbConfigData();
				gspdbConfigData.Provider = Provider;
				gspdbConfigData.Source = Source;
				gspdbConfigData.DbType = DbType;
				gspdbConfigData.UserId = UserId;
				gspdbConfigData.Password = Password;
				gspdbConfigData.CommandTimeout = 1200;
			}
			else
			{
				bool flag2 = DbType == GSPDbType.SQLServer;
				if (flag2)
				{
					gspdbConfigData = new SqlDbConfigData();
					gspdbConfigData.Catalog = Catalog;
					gspdbConfigData.Source = Source;
					gspdbConfigData.DbType = DbType;
					gspdbConfigData.UserId = UserId;
					gspdbConfigData.Password = Password;
				}
				else
				{
					bool flag3 = DbType == GSPDbType.PostgreSQL;
					if (flag3)
					{
						gspdbConfigData = new PostgreSQLConfigData();
						gspdbConfigData.Catalog = Catalog;
						gspdbConfigData.Source = Source;
						gspdbConfigData.DbType = DbType;
						gspdbConfigData.UserId = UserId;
						gspdbConfigData.Password = Password;
					}
				}
			}
			return gspdbConfigData;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00004658 File Offset: 0x00002858
		public IGSPDatabase GetOracleDatabase(string serverIP, string serverPort, string serverName, string userName, string passWord)
		{
			string source = string.Concat(new string[]
			{
				serverIP,
				":",
				serverPort,
				"/",
				serverName
			});
			string provider = "Oracle.DataAccess.OracleClient";
			GSPDbConfigData dbconfigdata = this.GetDBConfigdata(provider, source, GSPDbType.Oracle, null, userName, passWord);
			return Database.GetDatabase(dbconfigdata);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000046B0 File Offset: 0x000028B0
		public IGSPDatabase GetSqlDatabase(string serverName, string catalog, string userName, string passWord)
		{
			GSPDbConfigData dbconfigdata = this.GetDBConfigdata(null, serverName, GSPDbType.SQLServer, catalog, userName, passWord);
			return Database.GetDatabase(dbconfigdata);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000046D8 File Offset: 0x000028D8
		public IGSPDatabase GetPostgreSQLDatabase(string serverName, string serverPort, string catalog, string userName, string passWord)
		{
			string source = serverName + ":" + serverPort;
			GSPDbConfigData dbconfigdata = this.GetDBConfigdata(null, source, GSPDbType.PostgreSQL, catalog, userName, passWord);
			return Database.GetDatabase(dbconfigdata);
		}
	}
}

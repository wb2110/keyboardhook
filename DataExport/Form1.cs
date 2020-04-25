using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Genersoft.Platform.Core.DataAccess;
using Inspur.Gsp.Sys.ConfigData.Api;
using Newtonsoft.Json;

namespace DataExport
{
	// Token: 0x02000005 RID: 5
	public partial class Form1 : Form
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020C2 File Offset: 0x000002C2
		public Form1()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020E4 File Offset: 0x000002E4
		private void button1_Click(object sender, EventArgs e)
		{
			List<DboInfo> list = new List<DboInfo>();
			for (int i = 0; i < this.gridView1.DataRowCount; i++)
			{
				DataRow dataRow = this.gridView1.GetDataRow(i);
				bool flag = Convert.ToBoolean(dataRow["DboSel"]);
				if (flag)
				{
					list.Add(new DboInfo
					{
						DboId = dataRow["DboId"].ToString(),
						DboCode = dataRow["DboCode"].ToString(),
						DboName = dataRow["DboName"].ToString()
					});
				}
			}
			bool flag2 = list.Count == 0;
			if (flag2)
			{
				MessageBox.Show("请选择Dbo");
			}
			else
			{
				string path = string.Empty;
				FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
				bool flag3 = folderBrowserDialog.ShowDialog() == DialogResult.OK;
				if (flag3)
				{
					path = folderBrowserDialog.SelectedPath;
					foreach (DboInfo dboInfo in list)
					{
						try
						{
							string sqlStatement = "select  *  from " + dboInfo.DboCode;
							DataSet dataSet = this.database.ExecuteDataSet(sqlStatement);
							DataTable dataTable = dataSet.Tables[0];
							string path2 = Path.Combine(path, dboInfo.DboCode + ".data");
							bool flag4 = File.Exists(path2);
							if (flag4)
							{
								File.Delete(path2);
							}
							foreach (object obj in dataSet.Tables[0].Rows)
							{
								DataRow dataRow2 = (DataRow)obj;
								List<ConfigDataColumn> list2 = new List<ConfigDataColumn>();
								foreach (object obj2 in dataTable.Columns)
								{
									DataColumn dataColumn = (DataColumn)obj2;
									list2.Add(new ConfigDataColumn
									{
										ColName = dataColumn.ColumnName,
										ColValue = dataRow2[dataColumn.ColumnName]
									});
								}
								ConfigDataRow configDataRow = new ConfigDataRow();
								configDataRow.DboId = dboInfo.DboId;
								configDataRow.Data = list2;
								string str = new JavaScriptSerializer
								{
									MaxJsonLength = int.MaxValue
								}.Serialize(configDataRow);
								string item = this.ConvertJsonString(str);
								File.AppendAllLines(path2, new List<string>
								{
									item,
									"GO"
								});
							}
						}
						catch (Exception ex)
						{
							File.AppendAllText(Path.Combine(Application.StartupPath, "exception.txt"), ex.Message);
						}
					}
					MessageBox.Show("数据导出成功");
				}
				else
				{
					MessageBox.Show("请选择文件导出路径");
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002468 File Offset: 0x00000668
		private void Form1_Load(object sender, EventArgs e)
		{
			//FormLogin formLogin = new FormLogin();
			//bool flag = formLogin.ShowDialog() == DialogResult.Cancel;
			//if (flag)
			//{
			//	base.Close();
			//}
			//this.database = formLogin.database;
			//this.BindDbo();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000024A4 File Offset: 0x000006A4
		public void BindDbo()
		{
			string sqlStatement = "select id,code,name from GSPDatabaseObject";
			DataSet dataSet = this.database.ExecuteDataSet(sqlStatement);
			DataTable dataTable = new DataTable();
			DataColumn dataColumn = new DataColumn("DboSel", typeof(bool));
			DataColumn dataColumn2 = new DataColumn("DboCode", typeof(string));
			DataColumn dataColumn3 = new DataColumn("DboName", typeof(string));
			DataColumn dataColumn4 = new DataColumn("DboId", typeof(string));
			dataTable.Columns.AddRange(new DataColumn[]
			{
				dataColumn,
				dataColumn2,
				dataColumn3,
				dataColumn4
			});
			foreach (object obj in dataSet.Tables[0].Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["DboSel"] = false;
				dataRow2["DboCode"] = Convert.ToString(dataRow["code"]);
				dataRow2["DboName"] = Convert.ToString(dataRow["name"]);
				dataRow2["DboId"] = Convert.ToString(dataRow["id"]);
				dataTable.Rows.Add(dataRow2);
			}
			this.gridControl1.DataSource = dataTable;
			this.gridControl1.RefreshDataSource();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002648 File Offset: 0x00000848
		private void checkDbo_CheckedChanged(object sender, EventArgs e)
		{
			bool flag = this.gridView1.DataRowCount <= 0;
			if (!flag)
			{
				for (int i = 0; i < this.gridView1.DataRowCount; i++)
				{
					this.gridView1.GetDataRow(i)["DboSel"] = this.checkDbo.Checked;
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000026AE File Offset: 0x000008AE
		private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
		{
			this.CustomDrawRowIndicator(sender, e);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000026BC File Offset: 0x000008BC
		private void CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
		{
			bool flag = e.Info.IsRowIndicator && e.RowHandle >= 0;
			if (flag)
			{
				e.Info.DisplayText = (e.RowHandle + 1).ToString().Trim();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000270C File Offset: 0x0000090C
		private string ConvertJsonString(string str)
		{
			JsonSerializer jsonSerializer = new JsonSerializer();
			TextReader reader = new StringReader(str);
			JsonTextReader reader2 = new JsonTextReader(reader);
			object obj = jsonSerializer.Deserialize(reader2);
			bool flag = obj != null;
			string result;
			if (flag)
			{
				StringWriter stringWriter = new StringWriter();
				JsonTextWriter jsonWriter = new JsonTextWriter(stringWriter)
				{
					Formatting = Formatting.Indented,
					Indentation = 4,
					IndentChar = ' '
				};
				jsonSerializer.Serialize(jsonWriter, obj);
				result = stringWriter.ToString();
			}
			else
			{
				result = str;
			}
			return result;
		}

		// Token: 0x04000006 RID: 6
		private IGSPDatabase database = null;
	}
}

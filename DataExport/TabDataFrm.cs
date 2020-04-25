using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
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
	// Token: 0x0200000C RID: 12
	public partial class TabDataFrm : XtraForm
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00004728 File Offset: 0x00002928
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000475A File Offset: 0x0000295A
		public Dictionary<string, DataTable> ExpTableDataCache
		{
			get
			{
				bool flag = this.expTableDataCache == null;
				if (flag)
				{
					this.expTableDataCache = new Dictionary<string, DataTable>();
				}
				return this.expTableDataCache;
			}
			set
			{
				this.expTableDataCache = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00004764 File Offset: 0x00002964
		public Dictionary<string, List<TableAssociationInfo>> TableAllAssociation
		{
			get
			{
				bool flag = this.tableAllAssociation == null;
				if (flag)
				{
					this.tableAllAssociation = TableAssociationUtil.GetAllAssociation();
				}
				return this.tableAllAssociation;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00004794 File Offset: 0x00002994
		public TabDataFrm()
		{
			this.InitializeComponent();
			this.selfRowRightGridView.IndicatorWidth = 40;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000047C8 File Offset: 0x000029C8
		private void item_EditValueChanging(object sender, ChangingEventArgs e)
		{
			CheckEdit checkEdit = (CheckEdit)sender;
			GridView gridView = this.selfRowRightGridView;
			DataRow focusedDataRow = gridView.GetFocusedDataRow();
			bool flag = focusedDataRow != null;
			if (flag)
			{
				bool flag2 = (bool)focusedDataRow["SELECTTABROWLIMIT"];
				bool flag3 = !checkEdit.Checked && flag2;
				if (flag3)
				{
					MessageBox.Show("该数据已经被限制导出，不能被选中", "限制数据提示");
					e.Cancel = true;
				}
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00004834 File Offset: 0x00002A34
		private void TabDataFrm_Load(object sender, EventArgs e)
		{
			//this.selfRowRightGridControl.DataSource = null;
			//this.selfRowLeftGridView.FocusedRowHandle = 100;
			//FormLogin formLogin = new FormLogin();
			//bool flag = formLogin.ShowDialog() == DialogResult.Cancel;
			//if (flag)
			//{
			//	base.Close();
			//}
			//this.database = formLogin.database;
			//this.BindDbo();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000488C File Offset: 0x00002A8C
		public void BindDbo()
		{
			string sqlStatement = "select id,code,name from GSPDatabaseObject";
			DataSet dataSet = this.database.ExecuteDataSet(sqlStatement);
			this.dboTab = new DataTable();
			DataColumn dataColumn = new DataColumn("DboSel", typeof(bool));
			DataColumn dataColumn2 = new DataColumn("DboCode", typeof(string));
			DataColumn dataColumn3 = new DataColumn("DboName", typeof(string));
			DataColumn dataColumn4 = new DataColumn("DboId", typeof(string));
			DataColumn dataColumn5 = new DataColumn("SelectedCount", typeof(int));
			this.dboTab.Columns.AddRange(new DataColumn[]
			{
				dataColumn,
				dataColumn2,
				dataColumn3,
				dataColumn4,
				dataColumn5
			});
			string text = string.Empty;
			foreach (object obj in dataSet.Tables[0].Rows)
			{
				DataRow dataRow = (DataRow)obj;
				text = Convert.ToString(dataRow["code"]);
				bool flag = !this.IsExitsTableDatas(Convert.ToString(dataRow["code"]), this.database);
				if (flag)
				{
					bool flag2 = !this.IsExitsTableDatas(Convert.ToString(dataRow["code"]) + DateTime.Now.Year, this.database);
					if (flag2)
					{
						continue;
					}
					text += DateTime.Now.Year;
				}
				DataRow dataRow2 = this.dboTab.NewRow();
				dataRow2["DboSel"] = false;
				dataRow2["DboCode"] = text;
				dataRow2["DboName"] = Convert.ToString(dataRow["name"]);
				dataRow2["DboId"] = Convert.ToString(dataRow["id"]);
				dataRow2["SelectedCount"] = 0;
				this.dboTab.Rows.Add(dataRow2);
			}
			this.selfRowLeftgridControl.DataSource = this.dboTab;
			this.selfRowLeftgridControl.RefreshDataSource();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004B10 File Offset: 0x00002D10
		private void RepositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
		{
			int focusedRowHandle = this.selfRowLeftGridView.FocusedRowHandle;
			bool flag = focusedRowHandle < 0;
			if (!flag)
			{
				DataRow dataRow = this.selfRowLeftGridView.GetDataRow(focusedRowHandle);
				string tableName = Convert.ToString(dataRow["DboCode"]);
				DataTable tableData = this.GetTableData(tableName);
				GridView gridView = this.selfRowLeftGridView;
				bool flag2 = (bool)dataRow["DboSel"];
				bool flag3 = flag2;
				if (flag3)
				{
					foreach (object obj in tableData.Rows)
					{
						DataRow dataRow2 = (DataRow)obj;
						bool flag4 = !(bool)dataRow2["SELECTTABROW"];
						if (!flag4)
						{
							dataRow2["SELECTTABROW"] = false;
						}
					}
					dataRow["SelectedCount"] = 0;
				}
				else
				{
					foreach (object obj2 in tableData.Rows)
					{
						DataRow dataRow3 = (DataRow)obj2;
						dataRow3["SELECTTABROW"] = true;
					}
					dataRow["SelectedCount"] = tableData.Rows.Count;
				}
				dataRow["DboSel"] = !flag2;
				this.selfRowLeftGridView.RefreshData();
				this.selfRowRightGridView.RefreshData();
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00004CC8 File Offset: 0x00002EC8
		private bool IsExitsTableDatas(string table, IGSPDatabase db)
		{
			bool result;
			try
			{
				string sqlStatement = "select count(1) from " + table;
				DataSet dataSet = this.database.ExecuteDataSet(sqlStatement);
				DataRow dataRow = dataSet.Tables[0].Rows[0];
				int num = Convert.ToInt32(dataRow[0]);
				result = (num > 0);
			}
			catch (Exception ex)
			{
				File.AppendAllText("extablelog.txt", ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00004D4C File Offset: 0x00002F4C
		private void selfRowLeftGridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			int focusedRowHandle = this.selfRowLeftGridView.FocusedRowHandle;
			bool flag = focusedRowHandle < 0;
			if (!flag)
			{
				DataRow dataRow = this.selfRowLeftGridView.GetDataRow(focusedRowHandle);
				string text = Convert.ToString(dataRow["DboCode"]);
				DataTable tableData = this.GetTableData(text);
				this.selfRowRightGridView.Columns.Clear();
				List<string> list = null;
				bool flag2 = list == null;
				if (flag2)
				{
					list = new List<string>();
					foreach (object obj in tableData.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj;
						list.Add(dataColumn.ColumnName.ToUpper());
					}
				}
				int num = 1;
				GridColumn gridColumn = new GridColumn();
				gridColumn.Caption = "选择";
				gridColumn.FieldName = "SELECTTABROW".ToUpper();
				gridColumn.Name = "SELECTTABROW".ToUpper();
				gridColumn.Visible = true;
				gridColumn.VisibleIndex = 0;
				RepositoryItemCheckEdit repositoryItemCheckEdit = new RepositoryItemCheckEdit();
				repositoryItemCheckEdit.CheckedChanged += this.SelectCount_Changed;
				gridColumn.ColumnEdit = repositoryItemCheckEdit;
				this.selfRowRightGridView.Columns.Add(gridColumn);
				int count = list.Count;
				this.selfRowRightGridView.OptionsView.ColumnAutoWidth = false;
				foreach (string text2 in list)
				{
					bool flag3 = text2.ToUpper().Equals("SELECTTABROW");
					if (!flag3)
					{
						gridColumn = new GridColumn();
						gridColumn.Caption = text2;
						gridColumn.FieldName = text2;
						gridColumn.Name = text2;
						gridColumn.Visible = true;
						gridColumn.OptionsColumn.AllowEdit = false;
						gridColumn.VisibleIndex = num;
						this.selfRowRightGridView.Columns.Add(gridColumn);
						gridColumn.Width = 100;
						num++;
					}
				}
				bool flag4 = count < 8;
				if (flag4)
				{
					this.selfRowRightGridView.OptionsView.ColumnAutoWidth = true;
				}
				tableData.DefaultView.RowFilter = null;
				this.selfRowRightGridControl.DataSource = tableData;
				this.selfRowRightGridControl.Tag = text;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00004FD0 File Offset: 0x000031D0
		private DataTable GetTableData(string tableName)
		{
			bool flag = !this.ExpTableDataCache.ContainsKey(tableName);
			if (flag)
			{
				string sqlStatement = "select  *  from " + tableName;
				DataSet dataSet = this.database.ExecuteDataSet(sqlStatement);
				DataTable dataTable = dataSet.Tables[0];
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.Boolean");
				dataColumn.ColumnName = "SELECTTABROW";
				dataColumn.DefaultValue = false;
				dataTable.Columns.Add(dataColumn);
				this.ExpTableDataCache.Add(tableName, dataTable);
			}
			return this.ExpTableDataCache[tableName];
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00005080 File Offset: 0x00003280
		private void SelectCount_Changed(object sender, EventArgs e)
		{
			string arg = Convert.ToString(this.selfRowRightGridControl.Tag);
			DataRow[] array = this.dboTab.Select(string.Format("DboCode='{0}'", arg));
			CheckEdit checkEdit = (CheckEdit)sender;
			GridView gridView = this.selfRowRightGridView;
			DataRow focusedDataRow = gridView.GetFocusedDataRow();
			bool flag = focusedDataRow != null;
			if (flag)
			{
				bool flag2 = (bool)focusedDataRow["SELECTTABROW"];
				int num = Convert.ToInt32(array[0]["SelectedCount"]);
				bool flag3 = flag2;
				if (flag3)
				{
					array[0]["SelectedCount"] = num - 1;
				}
				else
				{
					array[0]["SelectedCount"] = num + 1;
				}
				focusedDataRow["SELECTTABROW"] = !flag2;
				array[0]["DboSel"] = (Convert.ToInt32(array[0]["SelectedCount"]) > 0);
			}
			this.selfRowLeftGridView.RefreshData();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000518C File Offset: 0x0000338C
		private void AllSelectBtn_Click(object sender, EventArgs e)
		{
			this.toolStrip1.Focus();
			string arg = Convert.ToString(this.selfRowRightGridControl.Tag);
			try
			{
				int rowCount = this.selfRowRightGridView.RowCount;
				GridColumn gridColumn = this.selfRowRightGridView.Columns[0];
				DataRow[] array = this.dboTab.Select(string.Format("DboCode='{0}'", arg));
				for (int i = rowCount; i >= 0; i--)
				{
					int num = Convert.ToInt32(array[0]["SelectedCount"]);
					DataRow dataRow = this.selfRowRightGridView.GetDataRow(i);
					bool flag = dataRow != null;
					if (flag)
					{
						bool flag2 = (bool)dataRow["SELECTTABROW"];
						bool flag3 = !flag2;
						if (flag3)
						{
							array[0]["SelectedCount"] = num + 1;
							dataRow["SELECTTABROW"] = true;
						}
					}
				}
				array[0]["DboSel"] = (Convert.ToInt32(array[0]["SelectedCount"]) > 0);
			}
			catch
			{
			}
			this.selfRowLeftGridView.RefreshData();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000052D4 File Offset: 0x000034D4
		private void ContrayBtn_Click(object sender, EventArgs e)
		{
			this.toolStrip1.Focus();
			try
			{
				int rowCount = this.selfRowRightGridView.RowCount;
				GridColumn gridColumn = this.selfRowRightGridView.Columns[0];
				string arg = Convert.ToString(this.selfRowRightGridControl.Tag);
				DataRow[] array = this.dboTab.Select(string.Format("DboCode='{0}'", arg));
				for (int i = rowCount - 1; i >= 0; i--)
				{
					int num = Convert.ToInt32(array[0]["SelectedCount"]);
					DataRow dataRow = this.selfRowRightGridView.GetDataRow(i);
					bool flag = dataRow != null;
					if (flag)
					{
						bool flag2 = (bool)dataRow["SELECTTABROW"];
						bool flag3 = !flag2;
						if (flag3)
						{
							array[0]["SelectedCount"] = num + 1;
							dataRow["SELECTTABROW"] = !flag2;
						}
						else
						{
							array[0]["SelectedCount"] = num - 1;
							dataRow["SELECTTABROW"] = !flag2;
						}
					}
				}
				array[0]["DboSel"] = (Convert.ToInt32(array[0]["SelectedCount"]) > 0);
			}
			catch
			{
			}
			this.selfRowLeftGridView.RefreshData();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00005464 File Offset: 0x00003664
		private void AllNotSelectBtn_Click(object sender, EventArgs e)
		{
			this.toolStrip1.Focus();
			string arg = Convert.ToString(this.selfRowRightGridControl.Tag);
			DataRow[] array = this.dboTab.Select(string.Format("DboCode='{0}'", arg));
			try
			{
				int rowCount = this.selfRowRightGridView.RowCount;
				GridColumn gridColumn = this.selfRowRightGridView.Columns[0];
				for (int i = rowCount; i >= 0; i--)
				{
					int num = Convert.ToInt32(array[0]["SelectedCount"]);
					DataRow dataRow = this.selfRowRightGridView.GetDataRow(i);
					bool flag = dataRow != null;
					if (flag)
					{
						bool flag2 = (bool)dataRow["SELECTTABROW"];
						bool flag3 = flag2;
						if (flag3)
						{
							array[0]["SelectedCount"] = num - 1;
							dataRow["SELECTTABROW"] = false;
						}
					}
				}
				array[0]["DboSel"] = (Convert.ToInt32(array[0]["SelectedCount"]) > 0);
			}
			catch
			{
			}
			this.selfRowLeftGridView.RefreshData();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000055A8 File Offset: 0x000037A8
		private void ConfirmBtn_Click(object sender, EventArgs e)
		{
			this.toolStrip1.Focus();
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000055C6 File Offset: 0x000037C6
		private void cancelBtn_Click(object sender, EventArgs e)
		{
			this.toolStrip1.Focus();
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000055E4 File Offset: 0x000037E4
		private void selfRowRightGridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
		{
			e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
			bool isRowIndicator = e.Info.IsRowIndicator;
			if (isRowIndicator)
			{
				bool flag = e.RowHandle >= 0;
				if (flag)
				{
					e.Info.DisplayText = (e.RowHandle + 1).ToString();
				}
				else
				{
					bool flag2 = e.RowHandle < 0 && e.RowHandle > -1000;
					if (flag2)
					{
						e.Info.Appearance.BackColor = Color.AntiqueWhite;
						e.Info.DisplayText = "G" + e.RowHandle.ToString();
					}
				}
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000056A4 File Offset: 0x000038A4
		private void selfRowLeftGridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
		{
			e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
			bool isRowIndicator = e.Info.IsRowIndicator;
			if (isRowIndicator)
			{
				bool flag = e.RowHandle >= 0;
				if (flag)
				{
					e.Info.DisplayText = (e.RowHandle + 1).ToString();
				}
				else
				{
					bool flag2 = e.RowHandle < 0 && e.RowHandle > -1000;
					if (flag2)
					{
						e.Info.Appearance.BackColor = Color.AntiqueWhite;
						e.Info.DisplayText = "G" + e.RowHandle.ToString();
					}
				}
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00005764 File Offset: 0x00003964
		private void exportBtn_Click(object sender, EventArgs e)
		{
			bool flag = this.dboTab == null || this.dboTab.Rows.Count == 0;
			if (flag)
			{
				MessageBox.Show("没有可导出的数据");
			}
			else
			{
				DataRow[] array = this.dboTab.Select("DboSel=true");
				bool flag2 = array == null || array.Length == 0;
				if (flag2)
				{
					MessageBox.Show("请选择导出的数据");
				}
				else
				{
					FormDataFileType formDataFileType = new FormDataFileType();
					bool flag3 = formDataFileType.ShowDialog() != DialogResult.OK;
					if (!flag3)
					{
						string fileSaveType = formDataFileType.FileSaveType;
						string text = string.Empty;
						string text2 = string.Empty;
						bool flag4 = fileSaveType == "0";
						if (flag4)
						{
							FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
							bool flag5 = folderBrowserDialog.ShowDialog() == DialogResult.OK;
							if (!flag5)
							{
								MessageBox.Show("请选择文件导出路径");
								return;
							}
							text = folderBrowserDialog.SelectedPath;
						}
						else
						{
							SaveFileDialog saveFileDialog = new SaveFileDialog();
							saveFileDialog.DefaultExt = ".data";
							saveFileDialog.Title = "选择数据文件";
							saveFileDialog.Filter = "(*.data)|*.data";
							bool flag6 = saveFileDialog.ShowDialog() != DialogResult.OK;
							if (flag6)
							{
								return;
							}
							text2 = saveFileDialog.FileName;
							bool flag7 = File.Exists(text2);
							if (flag7)
							{
								File.Delete(text2);
							}
						}
						List<string> list = new List<string>();
						foreach (DataRow dataRow in array)
						{
							try
							{
								list = new List<string>();
								string text3 = Convert.ToString(dataRow["DboCode"]);
								string dboId = Convert.ToString(dataRow["DboId"]);
								DataTable dataTable = this.ExpTableDataCache[text3];
								string path = string.Empty;
								bool flag8 = fileSaveType == "1";
								if (flag8)
								{
									path = text2;
								}
								else
								{
									path = Path.Combine(text, text3 + ".data");
									bool flag9 = File.Exists(path);
									if (flag9)
									{
										File.Delete(path);
									}
								}
								DataRow[] array3 = dataTable.Select("SELECTTABROW=true");
								foreach (DataRow dataRow2 in array3)
								{
									List<ConfigDataColumn> list2 = new List<ConfigDataColumn>();
									foreach (object obj in dataTable.Columns)
									{
										DataColumn dataColumn = (DataColumn)obj;
										bool flag10 = dataColumn.ColumnName == "SELECTTABROW";
										if (!flag10)
										{
											ConfigDataColumn configDataColumn = new ConfigDataColumn();
											configDataColumn.ColName = dataColumn.ColumnName;
											object obj2 = dataRow2[dataColumn.ColumnName];
											bool flag11 = obj2 != null && (obj2 is byte[] || obj2 is byte[]);
											if (flag11)
											{
												string text4 = string.Empty;
												foreach (byte b in obj2 as byte[])
												{
													text4 = text4 + b.ToString() + ",";
												}
												bool flag12 = text4.EndsWith(",");
												if (flag12)
												{
													text4 = text4.Substring(0, text4.Length - 1);
												}
												obj2 = text4;
											}
											configDataColumn.ColValue = obj2;
											list2.Add(configDataColumn);
										}
									}
									string str = JsonConvert.SerializeObject(new ConfigDataRow
									{
										DboId = dboId,
										DboCode = text3,
										Data = list2
									});
									string item = this.ConvertJsonString(str);
									list.Add(item);
									list.Add("GO");
								}
								File.AppendAllLines(path, list);
								string dataFileDir = string.Empty;
								bool flag13 = fileSaveType == "1";
								if (flag13)
								{
									dataFileDir = text2;
								}
								else
								{
									dataFileDir = text;
								}
								this.AssociationDataExport(array3, text3, dataFileDir);
							}
							catch (Exception ex)
							{
								File.AppendAllText(Path.Combine(Application.StartupPath, "exception.txt"), ex.Message);
								throw ex;
							}
						}
						MessageBox.Show("数据导出成功");
					}
				}
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00005BC8 File Offset: 0x00003DC8
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

		// Token: 0x06000051 RID: 81 RVA: 0x00005C44 File Offset: 0x00003E44
		private void AssociationDataExport(DataRow[] rows, string fromTable, string dataFileDir)
		{
			bool flag = rows == null || rows.Length == 0;
			if (!flag)
			{
				bool flag2 = !this.TableAllAssociation.ContainsKey(fromTable);
				if (!flag2)
				{
					List<TableAssociationInfo> list = this.TableAllAssociation[fromTable];
					foreach (TableAssociationInfo tableAssociationInfo in list)
					{
						string whereCondition = tableAssociationInfo.WhereCondition;
						string[] conditionEles = whereCondition.Split(new char[]
						{
							','
						});
						string name = tableAssociationInfo.Name;
						DataTable tableData = this.GetTableData(name);
						DataRow[] rows2 = this.SelectAssociationTableDatas(tableData, rows, conditionEles);
						this.MakeDataFile(tableData, rows2, dataFileDir, name);
						this.AssociationDataExport(rows2, name, dataFileDir);
					}
				}
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00005D24 File Offset: 0x00003F24
		private string GetDboId(string dboCode)
		{
			DataRow[] array = this.dboTab.Select(string.Format("DboCode='{0}'", dboCode));
			bool flag = array != null && array.Length != 0;
			string result;
			if (flag)
			{
				result = Convert.ToString(array[0]["DboId"]);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00005D74 File Offset: 0x00003F74
		private void MakeDataFile(DataTable dataTable, DataRow[] rows, string dataFileDir, string dboCode)
		{
			List<string> list = new List<string>();
			string dboId = this.GetDboId(dboCode);
			foreach (DataRow dataRow in rows)
			{
				List<ConfigDataColumn> list2 = new List<ConfigDataColumn>();
				foreach (object obj in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					bool flag = dataColumn.ColumnName == "SELECTTABROW";
					if (!flag)
					{
						ConfigDataColumn configDataColumn = new ConfigDataColumn();
						configDataColumn.ColName = dataColumn.ColumnName;
						object obj2 = dataRow[dataColumn.ColumnName];
						bool flag2 = obj2 != null && (obj2 is byte[] || obj2 is byte[]);
						if (flag2)
						{
							string text = string.Empty;
							foreach (byte b in obj2 as byte[])
							{
								text = text + b.ToString() + ",";
							}
							bool flag3 = text.EndsWith(",");
							if (flag3)
							{
								text = text.Substring(0, text.Length - 1);
							}
							obj2 = text;
						}
						configDataColumn.ColValue = obj2;
						list2.Add(configDataColumn);
					}
				}
				string str = JsonConvert.SerializeObject(new ConfigDataRow
				{
					DboId = dboId,
					DboCode = dboCode,
					Data = list2
				});
				string item = this.ConvertJsonString(str);
				list.Add(item);
				list.Add("GO");
			}
			bool flag4 = File.Exists(dataFileDir);
			if (flag4)
			{
				File.AppendAllLines(dataFileDir, list);
			}
			else
			{
				string path = Path.Combine(dataFileDir, dboCode + ".data");
				File.AppendAllLines(path, list);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00005F88 File Offset: 0x00004188
		private DataRow[] SelectAssociationTableDatas(DataTable associationDataTable, DataRow[] mainTableRows, string[] conditionEles)
		{
			Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			foreach (string text in conditionEles)
			{
				string[] array = text.Split(new char[]
				{
					'='
				});
				bool flag = array.Length != 2;
				if (!flag)
				{
					dictionary2.Add(array[0], array[1]);
				}
			}
			foreach (DataRow dataRow in mainTableRows)
			{
				foreach (KeyValuePair<string, string> keyValuePair in dictionary2)
				{
					bool flag2 = dictionary.ContainsKey(keyValuePair.Key);
					if (flag2)
					{
						dictionary[keyValuePair.Key].Add(Convert.ToString(dataRow[keyValuePair.Value]));
					}
					else
					{
						List<string> list = new List<string>();
						list.Add(Convert.ToString(dataRow[keyValuePair.Value]));
						dictionary.Add(keyValuePair.Key, list);
					}
				}
			}
			List<DataRow> list2 = new List<DataRow>();
			foreach (object obj in associationDataTable.Rows)
			{
				DataRow dataRow2 = (DataRow)obj;
				foreach (KeyValuePair<string, List<string>> keyValuePair2 in dictionary)
				{
					string item = Convert.ToString(dataRow2[keyValuePair2.Key]);
					bool flag3 = keyValuePair2.Value.Contains(item);
					if (flag3)
					{
						list2.Add(dataRow2);
					}
				}
			}
			return list2.ToArray();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00006D71 File Offset: 0x00004F71
		private void RepositoryItemCheckEdit1_CheckedChanged1(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000038 RID: 56
		private Dictionary<string, DataTable> expTableDataCache;

		// Token: 0x04000039 RID: 57
		public IGSPDatabase database = null;

		// Token: 0x0400003A RID: 58
		private DataTable dboTab = null;

		// Token: 0x0400003B RID: 59
		private Dictionary<string, List<TableAssociationInfo>> tableAllAssociation;
	}
}

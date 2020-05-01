using DataExport;
using Genersoft.Platform.Core.DataAccess;
using Gma.UserActivityMonitor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using WorkBench.DataBase;
using WorkBench.Properties;


namespace WorkBench
{

    public partial class Form1 : Form
    {
        private Keys prevKey = Keys.None;
        private DateTime prevTime;

        private CloudEnv env;
        private string filePath = @"config.json";
        private string redisPath = @"redis-cli.exe";

        public Form1()
        {
            InitializeComponent();
            SetNotifier();
            Dao.CreateTable();
            HookManager.KeyDown += new KeyEventHandler(hook_KeyDown);
            LoadDefaultEnv();
        }

        private void LoadDefaultEnv()
        {
            env = Dao.GetDefaultEnv();
            var path = Environment.GetEnvironmentVariable("EnvPath");
            tb_EnvPath.Text = env.envPath;
            tb_dbName.Text = env.dbName;
            tb_username.Text = env.dbUserName;
            tb_password.Text = env.dbPassword;
            tb_dbPort.Text = env.dbPort;
            SetRadioButton(env);
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            if (rb_otherHost.Checked)
            {
                env.dbHost = tb_host.Text;
            }
            env.dbName = tb_dbName.Text;
            env.dbPort = tb_dbPort.Text;
            env.dbUserName = tb_username.Text;
            env.dbPassword = tb_password.Text;
            if (env.envPath != tb_EnvPath.Text)
            {
                env.envPath = tb_EnvPath.Text;
                SetEnvPath();
            }

            RegisterDataBase();

        }

        private void RemoveDB(CloudEnv env)
        {
            Dao.DeleteEnv(env);
        }


        private void SetEnvPath()
        {
            var cmdexecutor = new CmdExecutor("SETX", "", " ENVPATH " + env.envPath);
            cmdexecutor.Execute();
        }
        private void RegisterDataBase()
        {
            var workDir = env.envPath + @"\tools\dbsetup\";
            var cmd = workDir + "start-win.cmd";
            var cmdexecutor = new CmdExecutor(cmd, workDir, "");

            string dbType = GetRegistDbType();
            var lastLine = string.Empty;
            cmdexecutor.Execute(new string[] { "2", dbType, env.dbHost, env.dbPort, env.dbName, env.dbUserName, env.dbPassword, "n" }, (sender, e) => {
                lastLine = e.Data;
                if (e.Data != null)
                {
                    if (lastLine.Contains("注册成功"))
                    {
                        Dao.Save(env);
                    }
                    AppendTextBoxText(tb_output, e.Data);
                    AppendTextBoxText(tb_output, "\n");
                }

            }, (sender, e) => {
                AppendTextBoxText(tb_output, "命令执行完成");
            });
        }
        void AppendTextBoxText(TextBox tb, string text)
        {
            if (tb.InvokeRequired)
            {
                tb.BeginInvoke(new Action(() => {
                    tb.AppendText(text);
                }));
                return;
            }
            tb.AppendText(text);
        }


        private void Button3_Click(object sender, EventArgs e)
        {
            CmdExecutor.OpenFolder(env.envPath);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            CmdExecutor.Execute("taskkill", "/f /im java.exe", false, new EventHandler((s, ev) => {
                var workDir = env.envPath;
                var cmd = workDir + @"\startup-jstack.cmd";
                var cmdExe = new CmdExecutor(cmd, workDir, "");
                cmdExe.Execute(true);
            }));

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            CmdExecutor.Execute("taskkill", "/f /im java.exe");
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            CmdExecutor.Execute(redisPath, "flushall", true);
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (s.Length == 1)
            {
                tb_clipBoard.Text = s[0];
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;

            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            try
            {
                FileAttributes attr = File.GetAttributes(tb_clipBoard.Text);

                var env = GetUIValue();
                var workDir = env.envPath + @"\tools\projectdeploy\";
                var cmd = workDir + "projectdeploy-win.cmd";
                var cmdexecutor = new CmdExecutor(cmd, workDir, "");
                string lastLine;
                cmdexecutor.Execute(new string[] { tb_clipBoard.Text, env.envPath, ((int)env.dbType).ToString()
                                                    , env.dbHost,env.dbPort, env.dbName, env.dbUserName, env.dbPassword, "n" }, (sender1, e1) =>
                                                    {
                                                        lastLine = e1.Data;
                                                        if (e1.Data != null)
                                                        {
                                                            AppendTextBoxText(tb_output, e1.Data);
                                                            AppendTextBoxText(tb_output, "\n");
                                                        }

                                                    }, (sender1, e1) =>
                                                    {
                                                        AppendTextBoxText(tb_output, "部署结束");
                                                    });
                ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                FileAttributes attr = File.GetAttributes(tb_clipBoard.Text);
                var env = GetUIValue();

                var workDir = env.envPath + @"\tools\dbotool\";
                var cmd = workDir + "dbodeploy-win.cmd";
                var cmdexecutor = new CmdExecutor(cmd, workDir, "");
                string lastLine;

                string dbType = GetDeployDbType();
                cmdexecutor.Execute(new string[] { tb_clipBoard.Text, env.envPath, dbType
                                                    , env.dbHost,env.dbPort, env.dbName, env.dbUserName, env.dbPassword, "","n" , Environment.NewLine}, (sender1, e1) => {
                                                        lastLine = e1.Data;
                                                        if (e1.Data != null)
                                                        {
                                                            AppendTextBoxText(tb_output, e1.Data);
                                                            AppendTextBoxText(tb_output, Environment.NewLine);
                                                        }

                                                    }, (sender1, e1) => {

                                                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string GetDeployDbType()
        {
            string dbType = "1";
            if (env.dbType == DbType.PgSQL)
            {
                dbType = "1";
            }
            else if (env.dbType == DbType.SQLServer)
            {
                dbType = "2";
            }
            else if (env.dbType == DbType.Oracle)
            {
                dbType = "3";
            }
            else if (env.dbType == DbType.DM)
            {
                dbType = "4";
            }
            else if (env.dbType == DbType.MySQL)
            {
                dbType = "5";
            }
            return dbType;
        }
        //获取注册数据库的数据库类型
        private string GetRegistDbType(CloudEnv e = null)
        {
            var lenv = env;
            if (e != null)
            {
                lenv = e;
            }

            string dbType = "0";
            if (lenv.dbType == DbType.PgSQL)
            {
                dbType = "1";
            }
            else if (lenv.dbType == DbType.SQLServer)
            {
                dbType = "2";
            }
            else if (lenv.dbType == DbType.Oracle)
            {
                dbType = "3";
            }
            else if (lenv.dbType == DbType.DM)
            {
                dbType = "4";
            }
            else if (lenv.dbType == DbType.MySQL)
            {
                dbType = "0";
            }
            return dbType;
        }
        private String GetDataImportDBType(CloudEnv e) { 
              string dbType = "0";
            if (e.dbType == DbType.PgSQL)
            {
                dbType = "1";
            }
            else if (e.dbType == DbType.SQLServer)
            {
                dbType = "2";
            }
            else if (e.dbType == DbType.Oracle)
            {
                dbType = "3";
            }
            else if (e.dbType == DbType.DM)
            {
                dbType = "4";
            }
            else if (e.dbType == DbType.MySQL)
            {
                dbType = "0";
            }
            return dbType;
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            try
            {
                FileAttributes attr = File.GetAttributes(tb_clipBoard.Text);

                var env = GetUIValue();

                var workDir = env.envPath + @"\tools\dataimport\";
                var cmd = workDir + "dataimport-win.cmd";
                var cmdexecutor = new CmdExecutor(cmd, workDir, "");
                string lastLine;

                string dbType = GetDataImportDBType(env);
                var list = new List<string>();
                list.Add(dbType);
                list.Add(env.dbHost);
                list.Add(env.dbPort);
                list.Add(env.dbName);
                list.Add(env.dbUserName);
                list.Add(env.dbPassword);
               

                list.Add(Environment.NewLine);
                list.Add(tb_clipBoard.Text);
                list.Add("n");
                list.Add(Environment.NewLine);

                cmdexecutor.Execute(list.ToArray(), (sender1, e1) => {
                    lastLine = e1.Data;
                    if (e1.Data != null)
                    {
                        AppendTextBoxText(tb_output, e1.Data);
                        AppendTextBoxText(tb_output, Environment.NewLine);
                    }

                }, (sender1, e1) => {
                    Console.WriteLine(e1);
                });
                Dao.Save(env);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            CmdExecutor.OpenFolder(@"C:\Users\wangbo03\AppData\Roaming\npm");
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            CmdExecutor.OpenFolder(@"E:\projects");
        }
        /// <summary>
        /// 快速部署
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button10_Click(object sender, EventArgs e)
        {
            AppendTextBoxText(tb_output, "正在关闭服务\n");
            CmdExecutor.Execute("taskkill", "/f /im dotnet.exe", false, new EventHandler((s, ev) => {
                AppendTextBoxText(tb_output, "服务已关闭\n");
                Thread.Sleep(1000);
                var path = tb_clipBoard.Text;
                var pathList = new List<string>();
                GetAllAppFolder(pathList, path);
                foreach (var item in pathList)
                {
                    AppendTextBoxText(tb_output, "正在部署:" + item);
                    AppendTextBoxText(tb_output, "\n");
                    DirectoryCopy(item, env.envPath + @"\apps", true);
                }
                AppendTextBoxText(tb_output, "部署完成");

                var workDir = env.envPath;
                var cmd = workDir + @"\startup-win.cmd";
                var cmdExe = new CmdExecutor(cmd, workDir, "");
                cmdExe.Execute(true);
            }));

        }
        private void GetAllAppFolder(List<string> paths, string path)
        {
            foreach (var item in Directory.GetDirectories(path))
            {
                if (item.Contains("apps"))
                {
                    paths.Add(item);
                    continue;
                }
                GetAllAppFolder(paths, item);
            }
        }
        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                if (File.Exists(temppath))
                {
                    var fileInfo = new FileInfo(temppath);
                    if (fileInfo.CreationTime > file.CreationTime)
                    {
                        continue;
                    }
                }
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private void Btn_compare_Click(object sender, EventArgs e)
        {
            var tempPath = Path.GetTempPath();
            var left = Path.Combine(tempPath, "左侧.txt");
            var right = Path.Combine(tempPath, "右侧.txt");

            var ltxt = tb_left.Text;
            var rtxt = tb_right.Text;

            Btn_fmtLeft_Click(null, null);
            Btn_fmtRight_Click(null, null);

            File.WriteAllText(left, tb_left.Text);
            File.WriteAllText(right, tb_right.Text);
            CmdExecutor.Execute(@"D:\Program Files\Beyond Compare 4\BCompare.exe", " " + left + " " + right + " ", true);
        }

        private void Btn_fmtLeft_Click(object sender, EventArgs e)
        {
            try
            {
                var ltxt = tb_left.Text;
                JToken token = JToken.Parse(ltxt);

                var outtxt = string.Empty;
                if (token.Type == JTokenType.String)
                {
                    try
                    {
                        JObject json = JObject.Parse((string)token);
                        outtxt = json.ToString(Formatting.Indented);
                    }
                    catch
                    {

                    }
                }
                else
                {
                    outtxt = token.ToString(Formatting.Indented);
                }
                tb_left.Text = outtxt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void Btn_fmtRight_Click(object sender, EventArgs e)
        {
            try
            {
                var ltxt = tb_right.Text;
                JToken token = JToken.Parse(ltxt);

                var outtxt = string.Empty;
                if (token.Type == JTokenType.String)
                {
                    try
                    {
                        JObject json = JObject.Parse((string)token);
                        outtxt = json.ToString(Formatting.Indented);
                    }
                    catch
                    {

                    }
                }
                else
                {
                    outtxt = token.ToString(Formatting.Indented);
                }
                tb_right.Text = outtxt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private int dbindex = 0;
        private void Button14_Click(object sender, EventArgs e)
        {
            var cb_dbType = groupBox1.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);
            var dbType = GetDBTypeByName(cb_dbType.Text);

            var cb_dbHost = grp_host.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);
            var dbHost = GetDBHostByName(cb_dbHost.Text);

            var next = Dao.GetEnvByHostAndDbType(env.dbHost, Convert.ToInt32(dbType),ref dbindex);
            dbindex++;
            
            SetUIValue(next);
        }
        private CloudEnv GetUIValue()
        {
            var uiEnv = new CloudEnv();

            var cb_dbType = groupBox1.Controls.OfType<RadioButton>()
                                     .FirstOrDefault(r => r.Checked);
            var dbType = GetDBTypeByName(cb_dbType.Text);

            var cb_dbHost = grp_host.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);
            var dbHost = GetDBHostByName(cb_dbHost.Text);

            uiEnv.dbType = dbType;
            uiEnv.dbHost = dbHost;
            uiEnv.dbPort = tb_dbPort.Text;
            uiEnv.dbName = tb_dbName.Text;
            uiEnv.dbUserName = tb_username.Text;
            uiEnv.dbPassword = tb_password.Text;
            uiEnv.envPath = tb_EnvPath.Text;

            return uiEnv;
        }
        private void SetUIValue(CloudEnv env) {
            tb_EnvPath.Text = env.envPath;
            if (env.dbHost == "10.24.21.1")
            {
                rb_21.Checked = true;
            }
            else if (env.dbHost == "10.24.21.35")
            {
                rb_35.Checked = true;
            }
            else if (env.dbHost == "127.0.0.1")
            {
                rb_localhost.Checked = true;
            }
            else
            {
                tb_host.Text = env.dbHost;
            }



            tb_dbPort.Text = env.dbPort;
            tb_username.Text = env.dbUserName;
            tb_password.Text = env.dbPassword;
            tb_dbName.Text = env.dbName;

        }
        private DbType GetDBTypeByName(string controlText)
        {
            switch (controlText)
            {
                case "PG":
                    return DbType.PgSQL;
                case "SQL":
                    return DbType.SQLServer;
                case "ORACLE":
                    return DbType.Oracle;
                case "DM":
                    return DbType.DM;
                default:
                    return DbType.PgSQL;
            }
        }
        private string GetDBHostByName(string controlText)
        {
            if (controlText.Equals("其他"))
            {
                return tb_host.Text;
            }
            return controlText;
        }

        private void Button13_Click(object sender, EventArgs e)
        {
           

        }

        private void Btn_exportData_Click(object sender, EventArgs e)
        {
            var thread = new Thread(new ThreadStart(() =>
            {
                IGSPDatabase dataBase;
                GSPDatabase gspdatabase = new GSPDatabase();

                var env = GetUIValue();
                switch (env.dbType)
                {
                    case DbType.Oracle:
                        dataBase = gspdatabase.GetOracleDatabase(env.dbHost, env.dbPort, env.dbName, env.dbUserName, env.dbPassword);
                        break;
                    case DbType.PgSQL:
                        dataBase = gspdatabase.GetPostgreSQLDatabase(env.dbHost, env.dbPort, env.dbName, env.dbUserName, env.dbPassword);
                        break;
                    case DbType.SQLServer:
                        dataBase = gspdatabase.GetSqlDatabase(env.dbHost, env.dbName, env.dbUserName, env.dbPassword);
                        break;
                    default:
                        dataBase = gspdatabase.GetSqlDatabase(env.dbHost, env.dbName, env.dbUserName, env.dbPassword);
                        break;
                }

                bool bOpened = true;
                try
                {
                    dataBase.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    btn_exportData.BeginInvoke(new Action(() => {
                        btn_exportData.Enabled = true;
                    }));
                    bOpened = false;

                }
                finally
                {

                }
                if (bOpened)
                {
                    try
                    {
                        var exportForm = new TabDataFrm();
                        exportForm.database = dataBase;
                        exportForm.BindDbo();
                        exportForm.Disposed += ExportForm_Disposed;
                        //Application.Run(exportForm);
                        exportForm.ShowDialog();
                        ExportForm_Disposed(null, null);


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        btn_exportData.BeginInvoke(new Action(() => {
                            btn_exportData.Enabled = true;
                        }));
                    }

                }

            }));
            thread.IsBackground = false;
            thread.SetApartmentState(ApartmentState.STA); //重点
            thread.Start();


            btn_exportData.Enabled = false;


        }

        private void ExportForm_Disposed(object sender, EventArgs e)
        {
            btn_exportData.BeginInvoke(new Action(() => {
                btn_exportData.Enabled = true;
            }));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            var keyCode = e.KeyCode.ToString();
            if (e.KeyData == Keys.Escape)
            {
                ChangeState();
            }
        }

        //新建数据库实例
        private void Button16_Click(object sender, EventArgs e)
        {
            env = GetUIValue();
            Dao.Save(env);
            //var workDir = env.envPath + @"\tools\dbsetup\";
            //var cmd = workDir + "start-win.cmd";
            //var cmdexecutor = new CmdExecutor(cmd, workDir, "");

            //var env1 = GetUIValue();
            //string dbType = GetRegistDbType(env1);
            //var lastLine = string.Empty;
            //cmdexecutor.Execute(new string[] { "1", dbType, env1.dbHost, env1.dbPort, env1.dbName, env1.dbUserName, env1.dbPassword, "n" }, (s, ev) => {
            //    lastLine = ev.Data;
            //    if (ev.Data != null)
            //    {
            //        if (lastLine.Contains("新建数据库实例成功"))
            //        {
            //            SaveNode(ConfNode.Env, JToken.FromObject(env1));
            //            AddDBList();
            //        }
            //        AppendTextBoxText(tb_output, ev.Data);
            //        AppendTextBoxText(tb_output, "\n");
            //    }

            //}, (s, ev) => {
            //    AppendTextBoxText(tb_output, "命令执行完成");
            //});
        }
        

        private void button17_Click(object sender, EventArgs e)
        {
            var list = new List<string>();
            list.Add(" ");
            list.Add("!");
            list.Add("\"");
            list.Add("#");
            list.Add("$");
            list.Add("%");
            list.Add("&");
            list.Add("'");
            list.Add("(");
            list.Add(")");
            list.Add("*");
            list.Add("+");
            list.Add(",");
            list.Add("-");
            list.Add(".");
            list.Add("/");
            list.Add(":");
            list.Add(";");
            list.Add("<");
            list.Add("=");
            list.Add(">");
            list.Add("?");
            list.Add("{");
            list.Add("|");
            list.Add("}");
            list.Add("~");
            list.Add("[");
           // list.Add(@"\");
            list.Add("]");
            list.Add("^");
            list.Add("_");
            list.Add("@");
            list.Add("`");
            var list1 = new List<string>();
            list1.Add(@"\x20");
            list1.Add(@"\\x21");
            list1.Add(@"\x22");
            list1.Add(@"\x23");
            list1.Add(@"\x24");
            list1.Add(@"\x25");
            list1.Add(@"\x26");
            list1.Add(@"\x27");
            list1.Add(@"\x28");
            list1.Add(@"\x29");
            list1.Add(@"\x2A");
            list1.Add(@"\x2B");
            list1.Add(@"\x2C");
            list1.Add(@"\x2D");
            list1.Add(@"\x2E");
            list1.Add(@"\x2F");
            list1.Add(@"\x3A");
            list1.Add(@"\x3B");
            list1.Add(@"\x3C");
            list1.Add(@"=");   //=
            list1.Add(@"\x3E");
            list1.Add(@"\x3F");
            list1.Add(@"\x7B");
            list1.Add(@"\x7C");
            list1.Add(@"\x7D");
            list1.Add(@"\x7E");
            list1.Add(@"\[");                   //list1.Add(@"\x5B");
            //  list1.Add(@"\x5C");
            list1.Add(@"\]");  //list1.Add(@"\x5D");
            list1.Add(@"\x5E");
            list1.Add(@"\x5F");
            list1.Add(@"\x40");
            list1.Add(@"\x60");
            var src = tb_left.Text;
            for(var i=0;i<list.Count;++i)
            {
                src = src.Replace(list[i], list1[i]);
            }
            tb_right.Text = src;
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.Dispose();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var env = GetUIValue();
            RemoveDB(env);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            CmdExecutor.Execute("taskkill", "/f /im dotnet.exe", false, new EventHandler((s, ev) => {
                var workDir = env.envPath;
                var cmd = workDir + @"\startup-nstack.cmd";
                var cmdExe = new CmdExecutor(cmd, workDir, "");
                cmdExe.Execute(true);
            }));
        }

        private void button19_Click(object sender, EventArgs e)
        {
            CmdExecutor.Execute("taskkill", "/f /im dotnet.exe");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            CmdExecutor.OpenFolder(@"\\10.100.1.163\产品服务器\待测库\FI Cloud\CICD");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            var param = @"D:\GO\BCCrack.bat";
            CmdExecutor.Execute(param,"");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            CmdExecutor.OpenFolder(@"\\10.24.21.1\FIShare");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            CmdExecutor.OpenFolder(@"\\10.24.21.35\FIShare");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            CmdExecutor.OpenFolder(@"\\10.24.22.38\FIShare");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            CmdExecutor.OpenFolder(@"\\10.24.22.39\FIShare");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            CmdExecutor.OpenFolder(@"\\10.24.22.40\FIShare");
        }
    }
}

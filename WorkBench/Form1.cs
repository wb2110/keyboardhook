using Gma.UserActivityMonitor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using WorkBench.Properties;


namespace WorkBench
{

    public partial class Form1 : Form
    {
        private Keys prevKey=Keys.None;
        private DateTime prevTime;

        private CloudEnv env ;
        private JObject jConfig;
        private string filePath = @"config.json";
        private string redisPath = @"F:\GSPCLOUD\Redis\redis-cli.exe";

         public Form1()
        {
            InitializeComponent();
            SetNotifier();
            HookManager.KeyDown += new KeyEventHandler(hook_KeyDown);
            LoadConfig();
            SetEnv(env);
        }

        private void LoadConfig()
        {
            JObject jobEnv;
            if (File.Exists(filePath))
            {
                jConfig = JObject.Parse(File.ReadAllText(filePath));
                jobEnv = jConfig[ConfNode.Env].ToObject<JObject>();
                env = jobEnv.ToObject<CloudEnv>();
                return;
            }
           
            File.WriteAllText(filePath, GetDefaultConfig().ToString());
        }
        private void SaveNode(string nodeName, JToken node)
        {
            JObject config = JObject.Parse(File.ReadAllText(filePath));
            config[nodeName] = node;
            File.WriteAllText(filePath, config.ToString());
        }
        private JToken GetDefaultConfig()
        {
            env = new CloudEnv();
            env.envPath = @"E:\gsp+bf_cloud_1906_x86_64_build2019060502";
            env.dbHost = @"10.24.21.1";
            env.dbType = DbType.PgSQL;
            env.dbName = "r3";
            env.dbPassword = "Test6530";
            env.dbUserName = "r3";
            env.dbPort = "5432";
            var config = new JObject();
            var jobEnv = JObject.FromObject(env);
            config[ConfNode.Env] = jobEnv;
            return config;
        }

        private void SetEnv(CloudEnv env)
        {
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
            env.dbUserName = tb_username.Text;
            env.dbPassword = tb_password.Text;
            if (env.envPath != tb_EnvPath.Text)
            {
                env.envPath = tb_EnvPath.Text;
                SetEnvPath();
            }
           
            RegisterDataBase();
            SaveNode(ConfNode.Env, JToken.FromObject(env));
        }

        private void AddDBList()
        {
            var config = JObject.Parse(File.ReadAllText(filePath));
            if (config.ContainsKey(ConfNode.DBList) == false)
            {
                var list = new JArray();
                list.Add(JToken.FromObject(env));
                //config[ConfNode.DBList] = list;
                SaveNode(ConfNode.DBList, list);
                return;
            }
            var dbList = config[ConfNode.DBList].ToObject<List<CloudEnv>>();
            var find = dbList.Find(x => x.dbHost == env.dbHost && x.dbType == env.dbType && x.dbPort == env.dbPort);
            if (find == null)
            {
                var arr = config[ConfNode.DBList] as JArray;
                arr.Add(JToken.FromObject(env));
                SaveNode(ConfNode.DBList, arr);
            }
           
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
            var cmdexecutor = new CmdExecutor(cmd,workDir,"");

            string dbType = ConvertDbType();
            var lastLine = string.Empty;
            cmdexecutor.Execute(new string[] { "2", dbType, env.dbHost, env.dbPort, env.dbName, env.dbUserName, env.dbPassword ,"n"},(sender,e)=> {
                lastLine = e.Data;
                if (e.Data != null)
                {
                   if (lastLine.Contains("注册成功"))
                   {
                        AddDBList();
                    }
                    AppendTextBoxText(tb_output, e.Data);
                    AppendTextBoxText(tb_output, "\n");
                }
               
            }, (sender, e) => {
                Console.WriteLine(lastLine);
            });
        }
        void AppendTextBoxText(TextBox tb, string text)
        {
            if (tb.InvokeRequired)
            {
                tb.BeginInvoke(new Action(()=> {
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
            CmdExecutor.Execute("taskkill", "/f /im dotnet.exe");
            var workDir = env.envPath;
            var cmd = workDir + @"\startup-win.cmd";
            var cmdExe = new CmdExecutor(cmd, workDir,"");
            cmdExe.Execute(true);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            CmdExecutor.Execute("taskkill", "/f /im dotnet.exe");
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            CmdExecutor.Execute(redisPath, "flushall",true);
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

                var workDir = env.envPath + @"\tools\projectdeploy\";
                var cmd = workDir + "projectdeploy-win.cmd";
                var cmdexecutor = new CmdExecutor(cmd, workDir, "");
                string lastLine;
                cmdexecutor.Execute(new string[] { tb_clipBoard.Text, env.envPath, env.dbType.GetTypeCode().ToString()
                                                    , env.dbHost,env.dbPort, env.dbName, env.dbUserName, env.dbPassword, "n" }, (sender1, e1) => {
                    lastLine = e1.Data;
                    if (e1.Data != null)
                    {
                          Console.WriteLine(e1.Data);
                    }

                }, (sender1, e1) => {

                });
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                FileAttributes attr = File.GetAttributes(tb_clipBoard.Text);

                var workDir = env.envPath + @"\tools\dbotool\";
                var cmd = workDir + "dbodeploy-win.cmd";
                var cmdexecutor = new CmdExecutor(cmd, workDir, "");
                string lastLine;

                string dbType = ConvertDbType();
                cmdexecutor.Execute(new string[] { tb_clipBoard.Text, env.envPath, dbType
                                                    , env.dbHost,env.dbPort, env.dbName, env.dbUserName, env.dbPassword, "","n" }, (sender1, e1) => {
                                                        lastLine = e1.Data;
                                                        if (e1.Data != null)
                                                        {
                                                            AppendTextBoxText(tb_output, e1.Data);
                                                            AppendTextBoxText(tb_output, "\n");
                                                        }

                                                    }, (sender1, e1) => {

                                                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string ConvertDbType()
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
            return dbType;
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            try
            {
                FileAttributes attr = File.GetAttributes(tb_clipBoard.Text);

                var workDir = env.envPath + @"\tools\dataimport\";
                var cmd = workDir + "dataimport-win.cmd";
                var cmdexecutor = new CmdExecutor(cmd, workDir, "");
                string lastLine;

                string dbType = ConvertDbType();
                cmdexecutor.Execute(new string[] {  dbType,env.dbHost,env.dbPort,env.dbName, env.dbUserName, env.dbPassword,tb_clipBoard.Text,"n","" }, (sender1, e1) => {
                                                        lastLine = e1.Data;
                                                        if (e1.Data != null)
                                                        {
                                                            AppendTextBoxText(tb_output, e1.Data);
                                                            AppendTextBoxText(tb_output, "\n");
                                                        }

                                                    }, (sender1, e1) => {

                                                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using WorkBench.DataBase;
using WorkBench.Properties;

namespace WorkBench
{
    public partial class Form1 : Form
    {
        private static Throttle throttle = new Throttle(500);
        void hook_KeyDown(object sender, KeyEventArgs e)
        {
            if (prevKey == Keys.LControlKey)
            {
                if (e.KeyCode == Keys.LControlKey)
                {
                    var interval = DateTime.Now - prevTime;
                    if (interval.Milliseconds < 250 && interval.Milliseconds > 50)
                    {
                        prevKey = Keys.None;
                        prevTime = DateTime.Now.AddMinutes(-2.0);
                        //this.Invoke((MethodInvoker)delegate { throttle.start(new Action(ChangeState)); });
                        ChangeState();
                    }
                }
            }
                if (prevKey == Keys.LMenu)
                {
                    //if (e.KeyCode == Keys.LMenu)
                    //{
                    //    var interval = DateTime.Now - prevTime;
                    //    if (interval.Milliseconds < 300 && interval.Milliseconds > 50)
                    //    {
                    //        prevKey = Keys.None;
                    //        Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
                    //                         @"http://localhost:5000");
                    //        return;
                    //    }
                    //}
                }
                if (prevKey == Keys.LShiftKey)
                {
                    //if (e.KeyCode == Keys.LShiftKey)
                    //{
                    //    var interval = DateTime.Now - prevTime;
                    //    if (interval.Milliseconds < 300 && interval.Milliseconds > 50)
                    //    {
                    //        prevKey = Keys.None;
                    //        Process.Start(@"D:\Program Files (x86)\Notepad++\NotePad++.exe");
                    //        return;
                    //    }
                    //}
                }
                prevKey = e.KeyCode;
                prevTime = DateTime.Now;
                var timer = new System.Timers.Timer();
            timer.Interval = 250;
            timer.Elapsed += (s, ee) =>
            {
                prevKey = Keys.None;
                prevTime = DateTime.Now.AddMinutes(-2.0);
                timer.Dispose();
            };
            timer.Start();

        }
            public void ChangeState()
            {
                if (this.Visible == false)
                {
                    ShowNormal();
                }
                else
                {
                    GotoTray();
                }
            }

            private void Form1_Resize(object sender, EventArgs e)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    GotoTray();
                }
            }

            private void Form1_FormClosing(object sender, FormClosingEventArgs e)
            {
                GotoTray();
                e.Cancel = true;
            }
            private void GotoTray()
            {
            this.Invoke((MethodInvoker)delegate
            {
                this.Hide();
                this.notifyIcon1.Visible = true;
            });
               
                //this.notifyIcon1.ShowBalloonTip(500, "提示", "呵呵", ToolTipIcon.Info);
            }
            private void ShowNormal()
            {
            this.Invoke((MethodInvoker)delegate
            {
                this.Hide();
                this.Show();
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.WindowState = FormWindowState.Normal;
                }
                if (this.Visible == false)
                {
                    this.Visible = true;
                }
                this.TopMost = true;

                string clipboardText = Clipboard.GetText(TextDataFormat.Text);
                var img = Clipboard.GetImage();
                if (img != null)
                {
                    pictureBox1.Image = img;
                }
                var list = new List<string>();
                new Thread(new ThreadStart(() =>
                {
                    try
                    {
                        FileAttributes attr = File.GetAttributes(clipboardText);
                        tb_fileList.BeginInvoke(new Action(() =>
                        {
                            tb_clipBoard.Text = clipboardText;
                            tb_fileList.Clear();
                        }));
                        foreach (var item in Directory.GetDirectories(clipboardText))
                        {
                            var path = Path.GetFileName(item);
                            list.Add(path);
                        }
                        list.Add(Environment.NewLine);
                        foreach (var item in Directory.GetFiles(clipboardText))
                        {
                            var file = Path.GetFileName(item);
                            list.Add(file);
                        }

                        tb_fileList.BeginInvoke(new Action(() =>
                        {
                            tb_fileList.AppendText(string.Join(Environment.NewLine, list));
                        }));


                    }
                    catch
                    {

                    }


                })).Start();
            });
            
            }
            private void SetNotifier()
            {
                this.notifyIcon1.Icon = Resources.tray;
                this.notifyIcon1.Text = "WorkBench";
                this.notifyIcon1.DoubleClick += new EventHandler((sender, e) =>
                {
                    this.Hide();
                    this.Visible = true;
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                    this.Focus();

                });
            }

            private void Button1_Click(object sender, EventArgs e)
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    tb_EnvPath.Text = folderBrowserDialog1.SelectedPath;
                }
            }



            private void Rb_pg_CheckedChanged(object sender, EventArgs e)
            {
                var rb = (RadioButton)sender;
                if (rb.Checked == false)
                {
                    return;
                }
                env.dbType = DbType.PgSQL;
                env.dbPort = "5432";
                tb_dbPort.Text = "5432";
                SetDefaultValue();
            }

            private void Rb_sql_CheckedChanged(object sender, EventArgs e)
            {
                var rb = (RadioButton)sender;
                if (rb.Checked == false)
                {
                    return;
                }
                env.dbType = DbType.SQLServer;
                env.dbPort = "1433";
                tb_dbPort.Text = "1433";
                SetDefaultValue();
            }

            private void Rb_dm_CheckedChanged(object sender, EventArgs e)
            {
                var rb = (RadioButton)sender;
                if (rb.Checked == false)
                {
                    return;
                }
                env.dbType = DbType.DM;
                env.dbPort = "5236";
                tb_dbPort.Text = "5236";
                SetDefaultValue();
            }

            private void Rb_ora_CheckedChanged(object sender, EventArgs e)
            {
                var rb = (RadioButton)sender;
                if (rb.Checked == false)
                {
                    return;
                }
                env.dbType = DbType.Oracle;
                env.dbPort = "1521";
                tb_dbPort.Text = "1521";
                SetDefaultValue();
            }

            private void Rb_21_CheckedChanged(object sender, EventArgs e)
            {
                var rb = (RadioButton)sender;
                if (rb.Checked == false)
                {
                    return;
                }
               // panel_other.Visible = false;
                env.dbHost = "x.gscloud.top";
                SetDefaultValue();
            }

            private void Rb_35_CheckedChanged(object sender, EventArgs e)
            {
                var rb = (RadioButton)sender;
                if (rb.Checked == false)
                {
                    return;
                }
                //panel_other.Visible = false;
                env.dbHost = "z.gscloud.top";
                SetDefaultValue();
            }

            private void Rb_localhost_CheckedChanged(object sender, EventArgs e)
            {
                var rb = (RadioButton)sender;
                if (rb.Checked == false)
                {
                    return;
                }
                //panel_other.Visible = false;
                env.dbHost = "127.0.0.1";
                SetDefaultValue();
            }

            private void Rb_otherHost_CheckedChanged(object sender, EventArgs e)
            {
                var rb = (RadioButton)sender;
                if (rb.Checked == false)
                {
                    return;
                }
              //  panel_other.Visible = true;
                SetDefaultValue();
            }
            private void SetRadioButton(CloudEnv env)
            {
                //dbHost
                if (env.dbHost == "x.gscloud.top")
                {
                    rb_21.Select();
                }
                else if (env.dbHost == "z.gscloud.top")
                {
                    rb_35.Select();
                }
                else if (env.dbHost == "127.0.0.1")
                {
                    rb_localhost.Select();
                }
                else
                {
                    rb_otherHost.Select();
                    tb_host.Text = env.dbHost;
                }
                //dbType
                if (env.dbType == DbType.DM)
                {
                    rb_dm.Select();
                }
                else if (env.dbType == DbType.Oracle)
                {
                    rb_ora.Select();
                }
                else if (env.dbType == DbType.SQLServer)
                {
                    rb_sql.Select();
                }
                else
                {
                    rb_pg.Select();
                }
            }
            private void SetDefaultValue()
            {
                var temp = Dao.GetEnvByHostAndDbType(env.dbHost, Convert.ToInt32(env.dbType), ref dbindex);
                if (temp == null)
                {
                    return;
                }
                env = temp;
           //     SetUIValue(env);
                //tb_dbName.Text = random.dbName;
                //tb_dbPort.Text = random.dbPort;
                //tb_username.Text = random.dbUserName;
                //tb_password.Text = random.dbPassword;

            }
        }

    }

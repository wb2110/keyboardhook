using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WorkBench.Properties;

namespace WorkBench
{
    public partial class Form1 : Form
    {
        void hook_KeyDown(object sender, KeyEventArgs e)
        {
            if (prevKey == Keys.LControlKey)
            {
                if (e.KeyCode == Keys.LControlKey)
                {
                    var interval = DateTime.Now - prevTime;
                    if (interval.Milliseconds < 300 && interval.Milliseconds > 50)
                    {
                        prevKey = Keys.None;
                        ChageState();
                        return;
                    }
                }
            }
            prevKey = e.KeyCode;
            prevTime = DateTime.Now;
        }
        private void ChageState()
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
            this.Hide();
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.ShowBalloonTip(500, "提示", "呵呵", ToolTipIcon.Info);
        }
        private void ShowNormal()
        {
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
            try
            {
                FileAttributes attr = File.GetAttributes(clipboardText);
                tb_clipBoard.Text = clipboardText;
            }
            catch
            {

            }
            
           

        }
        private void SetNotifier()
        {
            this.notifyIcon1.Icon = Resources.tray;
            this.notifyIcon1.Text = "WorkBench";
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
            env.dbType = DbType.PgSQL;
            env.dbPort = "5432";
            tb_dbPort.Text = "5432";
        }

        private void Rb_sql_CheckedChanged(object sender, EventArgs e)
        {
            env.dbType = DbType.SQLServer;
            env.dbPort = "1433";
            tb_dbPort.Text = "1433";
        }

        private void Rb_dm_CheckedChanged(object sender, EventArgs e)
        {
            env.dbType = DbType.DM;
            env.dbPort = "5236";
            tb_dbPort.Text = "5236";
        }

        private void Rb_ora_CheckedChanged(object sender, EventArgs e)
        {
            env.dbType = DbType.Oracle;
            env.dbPort = "1521";
            tb_dbPort.Text = "1521";
        }

        private void Rb_21_CheckedChanged(object sender, EventArgs e)
        {
            env.dbHost = "10.24.21.1";
        }

        private void Rb_35_CheckedChanged(object sender, EventArgs e)
        {
            env.dbHost = "10.24.21.35";
        }

        private void Rb_localhost_CheckedChanged(object sender, EventArgs e)
        {
            env.dbHost = "127.0.0.1";
        }

        private void Rb_otherHost_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void SetRadioButton(CloudEnv env)
        {
            //dbHost
            if (env.dbHost == "10.24.21.1")
            {
                rb_21.Select();
            }
            else if (env.dbHost == "10.24.21.35")
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
    }

}

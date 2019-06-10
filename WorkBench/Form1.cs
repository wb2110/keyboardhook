using Gma.UserActivityMonitor;
using System;
using System.Drawing;
using System.Windows.Forms;
using WorkBench.Properties;

namespace WorkBench
{

    public partial class Form1 : Form
    {
        private Keys prevKey=Keys.None;
        private DateTime prevTime;
        public Form1()
        {
            InitializeComponent();
            SetNotifier();
            HookManager.KeyDown += new KeyEventHandler(hook_KeyDown);
        }
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
            if (this.Visible==false)
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
            if(this.WindowState== FormWindowState.Minimized)
            {
                GotoTray();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            GotoTray();
            e.Cancel = true;
        }
        private void GotoTray() {
            this.Hide();
            this.notifyIcon1.Visible = true;
           // this.notifyIcon1.ShowBalloonTip(500, "提示", "呵呵", ToolTipIcon.Info);
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
        }
        private void SetNotifier() {
            this.notifyIcon1.Icon = Resources.tray;
            this.notifyIcon1.Text = "WorkBench";
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}

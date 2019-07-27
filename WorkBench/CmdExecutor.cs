using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WorkBench
{
    class CmdExecutor
    {
        private string cmd;
        private string param;
        private string workDir;
        Process process1 = new Process();
        public CmdExecutor(string cmd,string workDir,string param)
        {
            this.param = param;
            this.cmd = cmd;
            this.workDir = workDir;
        }
        public void Execute(bool showWindow=false)
        {
            process1.StartInfo.WorkingDirectory = workDir;
            process1.StartInfo.UseShellExecute = showWindow;
            process1.StartInfo.RedirectStandardInput = false;
            process1.StartInfo.FileName = cmd;
            process1.StartInfo.Arguments = param;

            process1.Start();
        }
        public void Execute(string[] stdinParams, DataReceivedEventHandler output=null, EventHandler exitHandler = null)
        {
            process1.StartInfo.WorkingDirectory = workDir;
            process1.StartInfo.UseShellExecute = false;
            process1.StartInfo.RedirectStandardInput = true;
            process1.StartInfo.RedirectStandardOutput = true;
            process1.StartInfo.RedirectStandardError = true;
            process1.StartInfo.CreateNoWindow = true;
            process1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process1.StartInfo.FileName = cmd;
            process1.StartInfo.Arguments = param;
            process1.EnableRaisingEvents = true;
            if (output != null)
            {
                process1.OutputDataReceived += output;
            }
            if (exitHandler != null)
            {
                process1.Exited += exitHandler;
            }
            
            process1.Start();
            process1.BeginOutputReadLine();
            foreach(var item in stdinParams)
            {
                process1.StandardInput.WriteLine(item);
            }

        }
        public void Kill()
        {
            process1.Kill();
        }
        public static void Execute(string cmd, string param, bool showWindow = false, EventHandler exitCallBack =null)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = cmd,
                Arguments = param,
                WindowStyle= showWindow? ProcessWindowStyle.Normal: ProcessWindowStyle.Hidden
            };
            if (exitCallBack != null)
            {
                process.EnableRaisingEvents = true;
                process.Exited += exitCallBack;
            }
            process.StartInfo = startInfo;
            process.Start();
        }

        public static void OpenFolder(string path)
        {
            //Process process1 = new Process();
            //process1.StartInfo.FileName = "explorer";
            //process1.StartInfo.Arguments = path;
            //process1.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            //process1.Start();
            //ProcessStartInfo startInfo = new ProcessStartInfo
            //{
            //    Arguments = path,
            //    FileName = "explorer.exe"
            // };

            // Process.Start(startInfo);
            Process.Start(path);
            
        }

    }
}

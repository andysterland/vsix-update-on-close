using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ConsoleScheduler
{
    public class Watcher
    {
        public event EventHandler AllProcessesExited;
        private readonly string processName;

        public Watcher() : this("devenv")
        {

        }

        public Watcher(string ProcessName)
        {
            processName = ProcessName;
        }

        public void StartAsync()
        {
            WatchForProcess();
        }

        public void Start()
        {
            Process processToWatch;
            while ((processToWatch = GetProcessToWatch()) != null)
            {
                processToWatch.WaitForExit();
            } 
        }

        private void WatchForProcess()
        {
            var watchedProcess = GetProcessToWatch();

            if (watchedProcess != null)
            {
                watchedProcess.EnableRaisingEvents = true;
                watchedProcess.Exited += WatchedProcess_Exited;
            }
            else
            {
                OnAllProcessExited();
            }
        }

        private Process GetProcessToWatch()
        {
            var watchedProcesses = Process.GetProcessesByName(processName);


            if (watchedProcesses != null && watchedProcesses.Length > 0)
            {
                for(int i = 0; i < watchedProcesses.Length; i++)
                {
                    Console.WriteLine($"Watching {watchedProcesses[i].ProcessName} (PID: {watchedProcesses[i].Id})");
                }
                return watchedProcesses[0];
            }
            else
            {
                OnAllProcessExited();
            }

            return null;
        }

        private void OnAllProcessExited()
        {
            AllProcessesExited?.Invoke(this, EventArgs.Empty);
        }

        private void WatchedProcess_Exited(object sender, EventArgs e)
        {
            WatchForProcess();
        }
    }
}

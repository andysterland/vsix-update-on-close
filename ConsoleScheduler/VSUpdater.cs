using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ConsoleScheduler
{
    class VSUpdater
    {
        private const string PathToInstaller = @"C:\Program Files (x86)\Microsoft Visual Studio\Installer\vs_installer.exe";
        private const string UpdateCommand = "update --passive --norestart --installPath \"{0}\"";

        private readonly string _updateArgs; 
        public VSUpdater(string PathToDevEnv)
        {
            _updateArgs = string.Format(UpdateCommand, PathToDevEnv);
        }

        public void Update()
        {
            var proc = new Process();
            proc.StartInfo.FileName = PathToInstaller;
            proc.StartInfo.Arguments = _updateArgs;
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
        }
    }
}

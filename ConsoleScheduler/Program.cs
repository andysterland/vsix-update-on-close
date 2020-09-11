using System;
using System.IO;

namespace ConsoleScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 1)
            {
                Console.WriteLine($"Unexpected number of arguments {args.Length}.");
                Console.WriteLine($"End.");
                return;
            }

            var vsLocation = args[0];

            if (!File.Exists(vsLocation))
            {
                Console.WriteLine($"No file at {vsLocation}.");
                Console.WriteLine($"End.");
                return;
            }

            var vsToWatch = Path.GetFileNameWithoutExtension(vsLocation);
                            
            var watcher = new Watcher(vsToWatch);
            Console.WriteLine($"Waiting for all {vsToWatch} processes to exit. The Visual Studio update will start once all processes exit. To cancel the update close this window.");
            watcher.Start();
            Console.WriteLine($"Starting update for: {vsLocation}");
            var vsUpdater = new VSUpdater(vsLocation);
            vsUpdater.Update();
            Console.WriteLine($"Completed update for: {vsLocation}");
        }

        private static void Watcher_AllProcessesExited(object sender, EventArgs e)
        {
            Console.WriteLine($"All processes exited.");
        }
    }
}

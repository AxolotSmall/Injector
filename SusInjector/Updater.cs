using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows;

namespace HorionInjector
{
    partial class MainWindow
    {

        private void Update()
        {
            var path = Assembly.GetExecutingAssembly().Location;

            try
            {
                Directory.GetAccessControl(Path.GetDirectoryName(path));
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Uh oh! The updater has no permission to access the injectors directory!");
                return;
            }

            File.Move(path, Path.ChangeExtension(path, "old"));
            new WebClient().DownloadFile("https://horion.download/bin/HorionInjector.exe", path);
            
            MessageBox.Show("Updater is done! The injector will now restart.");
            Process.Start(path);
            Application.Current.Shutdown();
        }
    }
}
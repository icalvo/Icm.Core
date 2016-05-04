﻿using System.Windows;

namespace Gekka.Roslyn.TranslateViewer
{
    public partial class App : Application
    {
        Worker data;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            data = new Worker();

            MainWindow w = new MainWindow();
            w.DataContext = data;
            w.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            data.Dispose();
            base.OnExit(e);
        }
    }
}

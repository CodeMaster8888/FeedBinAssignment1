using Database.Context;
using Microsoft.Extensions.DependencyInjection;
using Services.Managers;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AssignementApi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IFeedBinManager manager = new FeedBinManager();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            List<FeedBin> FeedBins = new List<FeedBin>();

            FeedBins.Add(manager.CreateFeedBin(1, 20, "Mix"));
            FeedBins.Add(manager.CreateFeedBin(2, 20, "Meat"));
            FeedBins.Add(manager.CreateFeedBin(3, 20, "Fruit"));

            Thread thread1 = new Thread(() => {
                var mainWindow = new MainWindow(FeedBins);
                mainWindow.Show();
                Dispatcher.Run();
            });

            Thread thread2 = new Thread(() => {
                var supervisorWindow = new SupervisorWindow(FeedBins);
                supervisorWindow.Show();
                Dispatcher.Run();
            });

            thread1.SetApartmentState(ApartmentState.STA);
            thread2.SetApartmentState(ApartmentState.STA);

            thread1.IsBackground = true;
            thread2.IsBackground = true;

            thread1.Start();
            thread2.Start();
        }
    }
}

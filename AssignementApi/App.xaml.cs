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

            var mainWindow = new MainWindow(FeedBins);

            var supervisorWindow = new SupervisorWindow(FeedBins);

            mainWindow.Show();
            supervisorWindow.Show();
        }
    }
}

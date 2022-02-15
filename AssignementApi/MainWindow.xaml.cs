using Services.Managers;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssignementApi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public FeedBin FeedBin1 { get; set; }
        private readonly IFeedBinManager manager = new FeedBinManager();

        public MainWindow()
        {
            InitializeComponent();
            FeedBin1 = manager.CreateFeedBin(1, 20, "Mix");
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FlushProduct_Click(object sender, RoutedEventArgs e)
        {
            manager.Flush(FeedBin1);
        }

        private void RemoveProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InspectProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductNameValue.Content = FeedBin1.ProductName;
            CurrentVolumeValue.Content = FeedBin1.CurrentVolume;
            MaxVolumeValue.Content = FeedBin1.MaxVolume;
            PopUp1.IsOpen = true;
        }
    }
}
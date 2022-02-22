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
        private FeedBin FeedBin1 { get; set; }
        private FeedBin FeedBin2 { get; set; }
        private FeedBin FeedBin3 { get; set; }
        private readonly IFeedBinManager manager = new FeedBinManager();

        public MainWindow(List<FeedBin> feedBins)
        {
            InitializeComponent();
            FeedBin1 = feedBins.Single(x => x.BinNumber == 1);
            FeedBin2 = feedBins.Single(x => x.BinNumber == 2);
            FeedBin3 = feedBins.Single(x => x.BinNumber == 3);
        }

        private FeedBin GetRadioButtonChoice()
        {
            if (FeedBinChoice3.IsChecked == true)
            {
                return FeedBin3;
            }else if (FeedBinChoice2.IsChecked == true)
            {
                return FeedBin2;
            }else
            {
                return FeedBin1;
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddVolumePopup.IsOpen = true;
        }

        private void FlushProduct_Click(object sender, RoutedEventArgs e)
        {
            var feedBin = GetRadioButtonChoice();
            manager.Flush(feedBin);
        }

        private void RemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            RemoveVolumePopup.IsOpen = true;
        }

        private void InspectProduct_Click(object sender, RoutedEventArgs e)
        {
            var feedBin = GetRadioButtonChoice();
            ProductNameValue.Content = feedBin.ProductName;
            CurrentVolumeValue.Content = feedBin.CurrentVolume;
            MaxVolumeValue.Content = feedBin.MaxVolume;
            InspectPopup.IsOpen = true;
        }

        private void AddPopupVolume(object sender, RoutedEventArgs e)
        {
            var feedBin = GetRadioButtonChoice();
            if (Double.TryParse(VolumeToAdd.Text, out double volumeToAdd))
            {
                manager.AddProduct(feedBin, volumeToAdd);
                AddVolumePopup.IsOpen = false;
            }
        }

        private void RemovePopupVolume(object sender, RoutedEventArgs e)
        {
            var feedBin = GetRadioButtonChoice();
            if (Double.TryParse(VolumeToRemove.Text, out double volumeToRemove))
            {
                manager.RemoveProduct(feedBin, volumeToRemove);
                RemoveVolumePopup.IsOpen = false;
            }
        }

        private void AddPopUpClose(object sender, RoutedEventArgs e)
        {
            AddVolumePopup.IsOpen = false;
        }

        private void RemovePopUpClose(object sender, RoutedEventArgs e)
        {
            RemoveVolumePopup.IsOpen = false;
        }

        private void SetProductPopUpClose(object sender, RoutedEventArgs e)
        {
            SetProductPopup.IsOpen = false;
        }

        private void SetProduct_Click(object sender, RoutedEventArgs e)
        {
            SetProductPopup.IsOpen = true;
        }

        private void SetProductPopupValue(object sender, RoutedEventArgs e)
        {
            var feedBin = GetRadioButtonChoice();
            var productChange = ProductNameChange.Text;
            manager.SetProductName(feedBin,productChange);
            SetProductPopup.IsOpen=false;
        }
    }
}

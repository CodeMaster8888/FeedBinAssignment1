using Database.Models;
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
using System.Windows.Shapes;

namespace AssignementApi
{
    /// <summary>
    /// Interaction logic for SupervisorWindow.xaml
    /// </summary>
    public partial class SupervisorWindow : Window
    {
        public IEnumerable<Batch> Batches { get; set; }
        public List<ReportBatch> ReportBatches { get; set; }

        private readonly ISupervisorManager supervisorManager;
        private List<FeedBin> FeedBins { get; set; }
        

        public SupervisorWindow(List<FeedBin> feedBins)
        {
            InitializeComponent();

            supervisorManager = new SupervisorManager();
            FeedBins = feedBins;

            SetBatches();

            foreach (var batch in Batches)
            {
                BatchListAmount.Items.Add(batch.Amount);
                BatchListFirstIngredient.Items.Add(batch.Recipe.FirstIngredient.ProductName);
                BatchListSecondIngredient.Items.Add(batch.Recipe.SecondIngredient.ProductName);
                BatchListName.Items.Add(batch.Recipe.Name);
            }
        }

        private void SetBatches() 
        {
            Batches = new List<Batch>
            {
                new Batch
                {
                    Amount = 10,
                    Id = 1,
                    Recipe = new Recipe
                    {
                        FirstIngredient = new Ingredient{ ProductName = "Meat"},
                        SecondIngredient = new Ingredient{ProductName = "Fruit"},
                        FirstPercentage = 0.5,
                        SecondPercentage = 0.5,
                        Name = "MeatMix"
                    }
                },
                new Batch
                {
                    Amount = 40,
                    Id = 2,
                    Recipe = new Recipe
                    {
                        FirstIngredient = new Ingredient{ ProductName = "Meat"},
                        SecondIngredient = new Ingredient{ProductName = "Vegetables"},
                        FirstPercentage = 0.8,
                        SecondPercentage = 0.2,
                        Name = "FruitMix"
                    }
                },
            };
        }

        private void CheckBatch_Click(object sender, RoutedEventArgs e)
        {
            var result = supervisorManager.CheckBatch(FeedBins, Batches.First());

            if (result == false)
            {
                CheckbatchFalsePopup.IsOpen = true;
            }
            else
            {
                CheckbatchTruePopup.IsOpen = true;
            }
        }

        private void ReportBatch_Click(object sender, RoutedEventArgs e)
        {
            ReportBatches = supervisorManager.ReportBatch(FeedBins, Batches.Last());

            if(ReportBatches.Count == 2)
            {
                ReportFeedBin1ReasonValue.Content = ReportBatches.First().Reason;
                if (ReportBatches.First().FeedBin != null)
                {
                    ReportFeedBin1Value.Content = ReportBatches.First().FeedBin.BinNumber;
                    ReportFeedBin1Value.Visibility = Visibility.Visible;
                    ReportFeedbin1Label.Visibility = Visibility.Visible;
                }
                ReportFeedBin2ReasonValue.Content = ReportBatches.Last().Reason;
                if (ReportBatches.Last().FeedBin != null)
                {
                    ReportFeedBin2Value.Content = ReportBatches.Last().FeedBin.BinNumber;
                    ReportFeedbin2Label.Visibility = Visibility.Visible;
                    ReportFeedBin2Value.Visibility = Visibility.Visible;
                }

                ReportFeedBin1ReasonValue.Visibility = Visibility.Visible;

                ReportFeedBin2ReasonLabel.Visibility = Visibility.Visible;
                ReportFeedBin2ReasonValue.Visibility = Visibility.Visible;

                ReportbatchTruePopup.IsOpen = true;
            }
            else if (ReportBatches.Count == 1)
            {
                ReportFeedBin1ReasonValue.Content = ReportBatches.First().Reason;
                if (ReportBatches.First().FeedBin != null)
                {
                    ReportFeedBin1Value.Content = ReportBatches.First().FeedBin.BinNumber;
                    ReportFeedBin1Value.Visibility = Visibility.Visible;
                    ReportFeedbin1Label.Visibility = Visibility.Visible;
                }
                ReportFeedBin1ReasonValue.Visibility = Visibility.Visible;

                ReportbatchTruePopup.IsOpen = true;
            }
            else
            {
                ReportbatchFalsePopup.IsOpen = true;
            }
        }
        private void MakeBatch_Click(object sender, RoutedEventArgs e)
        {
            supervisorManager.MakeBatch(FeedBins, Batches.First());

            MakebatchTruePopup.IsOpen = true;
        }
    }
}

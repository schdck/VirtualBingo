using MahApps.Metro.Controls;
using System.Windows.Controls;

namespace VirtualBingo.UI.Player.Views.Windows
{
    /// <summary>
    /// Interaction logic for AnswersWindow.xaml
    /// </summary>
    public partial class AnswersWindow : MetroWindow
    {
        public AnswersWindow()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dGrid = sender as DataGrid;

            dGrid?.ScrollIntoView(dGrid.SelectedItem ?? dGrid.Items[0]);
        }
    }
}

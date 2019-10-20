using SharpBoyEmulator.Models;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;

namespace SharpBoyEmulator.GUI
{
    /// <summary>
    /// Logique d'interaction pour MemoryViewerWindow.xaml
    /// </summary>
    public partial class MemoryViewerWindow : Window
    {
        private readonly ISharpBoyBusinessLogic businessLogic;
        private IMemoryCell[] cells;


        public MemoryViewerWindow()
        {
            businessLogic = ((App)Application.Current).BusinessLogic;
            InitializeComponent();
        }

        private void AddressTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter && AddressTextBox.Text != string.Empty)
            {
                GoToAddress(AddressTextBox.Text);
            }
        }

        private void MemoryDomainComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (MemoryDomainComboBox.SelectedIndex)
            {
                case 0: PopulateListView(0, 0xFFFF); break;
                case 1: PopulateListView(0, 0x7FFF); break;
                case 2: PopulateListView(0x8000, 0x9FFF); break;
                case 3: PopulateListView(0xA000, 0xBFFF); break;
                case 4: PopulateListView(0xC000, 0xDFFF); break;
                case 5: PopulateListView(0xE000, 0xFDFF); break;
                case 6: PopulateListView(0xFE00, 0xFE9F); break;
                case 7: PopulateListView(0xFF00, 0xFF4B); break;
                case 8: PopulateListView(0xFF80, 0xFFFE); break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateListView(0x0000, 0xFFFF);
        }





        private void PopulateListView(int startIndex, int endIndex)
        {
            if (MemoryListView == null)
                return;

            cells = businessLogic.GetMemoryCells(startIndex, endIndex);
            MemoryListView.ItemsSource = cells;
        }

        private void GoToAddress(string hexAddress)
        {
            if(int.TryParse(hexAddress, NumberStyles.HexNumber, null , out int address))
            {
                MemoryListView.SelectedItem = cells.First(c => c.Address == address);
                MemoryListView.ScrollIntoView(MemoryListView.SelectedItem);
            }
            else MessageBox.Show("L'adresse 0x" + address.ToString("X4") + " n'a pas été trouvé !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

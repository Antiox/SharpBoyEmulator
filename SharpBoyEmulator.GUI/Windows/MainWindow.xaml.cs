using Microsoft.Win32;
using System;
using System.Windows;
using SharpBoyEmulator.Models;

namespace SharpBoyEmulator.GUI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ISharpBoyBusinessLogic businessLogic;


        public MainWindow()
        {
            InitializeComponent();
            DisableControls();
            businessLogic = ((App)Application.Current).BusinessLogic;
        }

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var romFileDialog = new OpenFileDialog
            {
                InitialDirectory = System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory),
                Filter = "Game Boy game|*.gb;*.gbc"
            };

            if (romFileDialog.ShowDialog(this) == true)
            {
                businessLogic.LoadCartridge(romFileDialog.FileName);
                EnableControls();
                businessLogic.StartEmulator();
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown(0);
        }

        private void DisassembleMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new DisassembleWindow().Show();
        }

        private void RomHeaderMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new RomHeaderWindow().Show();
        }

        private void EjectMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DisableControls();
            businessLogic.ResetEmulator();
        }

        private void MemoryViewerMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new MemoryViewerWindow().Show();
        }




        private void DisableControls()
        {
            ToolsMenuItem.IsEnabled = false;
            GameMenuItem.IsEnabled = false;
            EjectMenuItem.IsEnabled = false;
        }

        private void EnableControls()
        {
            ToolsMenuItem.IsEnabled = true;
            GameMenuItem.IsEnabled = true;
            EjectMenuItem.IsEnabled = true;
        }
    }
}

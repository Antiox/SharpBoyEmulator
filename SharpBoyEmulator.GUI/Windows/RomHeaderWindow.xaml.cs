using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SharpBoyEmulator.Models;

namespace SharpBoyEmulator.GUI
{
    /// <summary>
    /// Logique d'interaction pour RomHeaderWindow.xaml
    /// </summary>
    public partial class RomHeaderWindow : Window
    {
        private readonly ISharpBoyBusinessLogic businessLogic;

        public RomHeaderWindow()
        {
            InitializeComponent();
            businessLogic = ((App)Application.Current).BusinessLogic;
        }


        private void RomheaderWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Initialize();
        }

        private void Initialize()
        {
            var romHeader = new RomHeader(null);

            TitleTextBox.Text = romHeader?.Title;
            CompatibilityTextBox.Text = romHeader?.ModeGBC;
            EditorTextBox.Text = romHeader?.Editor;
            SuperGBModeTextBox.Text = romHeader?.ModeSGB;
            CartridgeTypeTextBox.Text = romHeader?.TypeCartouche;
            ROMSizeTextBox.Text = $"{romHeader?.ROMSize.ToString("N0")} o";
            RAMSizeTextBox.Text = $"{romHeader?.RAMSize.ToString("N0")} o";
            DestionationTextBox.Text = romHeader?.Destination;
            VersionTextBox.Text = romHeader?.Version.ToString();
            HeaderChecksumTextBox.Text = $"0x{romHeader?.HeaderChecksum.ToString("X2")}";
            GlobalChecksumTextBox.Text = $"0x{romHeader?.GlobalChecksum.ToString("X2")}";
        }
    }
}

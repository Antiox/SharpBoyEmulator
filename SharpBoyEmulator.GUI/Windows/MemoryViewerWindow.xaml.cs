﻿using SharpBoyEmulator.Interfaces;
using SharpBoyEmulator.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SharpBoyEmulator.GUI
{
    /// <summary>
    /// Logique d'interaction pour MemoryViewerWindow.xaml
    /// </summary>
    public partial class MemoryViewerWindow : Window
    {
        private readonly ISharpBoyBusinessLogic businessLogic;



        public MemoryViewerWindow()
        {
            businessLogic = ((App)Application.Current).BusinessLogic;
            InitializeComponent();
        }

        private void AddressTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

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
                case 3: PopulateListView(0xC000, 0xDFFF); break;
                case 4: PopulateListView(0xFE00, 0xFE9F); break;
                case 5: PopulateListView(0xFF00, 0xFF4B); break;
                case 6: PopulateListView(0xFF80, 0xFFFE); break;
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

            MemoryListView.ItemsSource = businessLogic.GetMemoryCells(startIndex, endIndex);
        }

        private void GoToAddress(string hexAddress)
        {
            if(int.TryParse(hexAddress, NumberStyles.HexNumber, null , out int address))
            {
            }
            else MessageBox.Show("L'adresse 0x" + address.ToString("X4") + " n'a pas été trouvé !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
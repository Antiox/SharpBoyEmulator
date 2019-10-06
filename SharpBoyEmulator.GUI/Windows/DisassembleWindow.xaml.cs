﻿using SharpBoyEmulator.Models;
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

namespace SharpBoyEmulator.GUI
{
    /// <summary>
    /// Logique d'interaction pour DisassembleWindow.xaml
    /// </summary>
    public partial class DisassembleWindow : Window
    {
        private readonly ISharpBoyBusinessLogic businessLogic;
        private List<IInstruction> _cells;
        private IRegisters _registers;


        public DisassembleWindow()
        {
            businessLogic = ((App)Application.Current).BusinessLogic;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshRegisters();
            PopulateListView();
        }

        private void StartStopButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ExecuteInstructionButton_Click(object sender, RoutedEventArgs e)
        {
            businessLogic.Step();
            RefreshRegisters();
            PopulateListView();
        }

        private void AddressTextBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void RefreshInstructionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void autoRefreshCheckbox_Checked(object sender, RoutedEventArgs e)
        {

        }


        private void PopulateListView()
        {
            _cells = businessLogic.GetAllInstructions();
            InstructionsListView.ItemsSource = _cells;
            InstructionsListView.SelectedItem = _cells.First(i => i.Address == _registers.PC);
            InstructionsListView.ScrollIntoView(InstructionsListView.SelectedItem);
        }
        private void RefreshRegisters()
        {
            _registers = businessLogic.GetRegisters();
            afRegisterLabel.Content = $"0x{_registers.AF.ToString("X4")}";
            bcRegisterLabel.Content = $"0x{_registers.BC.ToString("X4")}";
            deRegisterLabel.Content = $"0x{_registers.DE.ToString("X4")}";
            hlRegisterLabel.Content = $"0x{_registers.HL.ToString("X4")}";
            spRegisterLabel.Content = $"0x{_registers.SP.ToString("X4")}";
            pcRegisterLabel.Content = $"0x{_registers.PC.ToString("X4")}";
            zFlagCheckbox.IsChecked = _registers.ZFlag == 1;
            nFlagCheckbox.IsChecked = _registers.NFlag == 1;
            hFlagCheckbox.IsChecked = _registers.HFlag == 1;
            cFlagCheckbox.IsChecked = _registers.CFlag == 1;
            imeCheckbox.IsChecked = _registers.IME;
        }
    }
}

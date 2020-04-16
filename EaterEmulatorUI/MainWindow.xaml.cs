using EaterEmulator;
using EaterEmulator.Operations;
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

namespace EaterEmulatorUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Emulator Emulator { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Emulator = new Emulator();
            Emulator.Clock.RisingEdge += OnRisingEdge;
            Emulator.Clock.FallingEdge += OnFallingEdge;

            outputLabel.SetBinding(
                Label.ContentProperty,
                new Binding("Output.Value")
                {
                    Source = Emulator
                });

            aRegisterLabel.SetBinding(
               Label.ContentProperty,
               new Binding("A.Value")
               {
                   Source = Emulator
               });

            bRegisterLabel.SetBinding(
               Label.ContentProperty,
               new Binding("B.Value")
               {
                   Source = Emulator
               });
        }

        private void OnRisingEdge(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.clockPulseLightDim.Visibility = Visibility.Hidden;
                this.clockPulseLightLit.Visibility = Visibility.Visible;
            });
        }

        private void OnFallingEdge(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.clockPulseLightDim.Visibility = Visibility.Visible;
                this.clockPulseLightLit.Visibility = Visibility.Hidden;
            });
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Emulator.Clock.Stop();
            Emulator.Reset();

            Emulator.RAM.Store(0x0, LDI.OP_CODE + 0);
            Emulator.RAM.Store(0x1, STA.OP_CODE + 0xD);
            Emulator.RAM.Store(0x2, OUT.OP_CODE);
            Emulator.RAM.Store(0x3, LDI.OP_CODE + 1);
            Emulator.RAM.Store(0x4, STA.OP_CODE + 0xE);
            Emulator.RAM.Store(0x5, OUT.OP_CODE);
            Emulator.RAM.Store(0x6, ADD.OP_CODE + 0xD);
            Emulator.RAM.Store(0x7, JC.OP_CODE + 0x0);
            Emulator.RAM.Store(0x8, STA.OP_CODE + 0xF);
            Emulator.RAM.Store(0x9, LDA.OP_CODE + 0xE);
            Emulator.RAM.Store(0xA, STA.OP_CODE + 0xD);
            Emulator.RAM.Store(0xB, LDA.OP_CODE + 0xF);
            Emulator.RAM.Store(0xC, JMP.OP_CODE + 0x4);
            Emulator.RAM.Store(0xD, 0);
            Emulator.RAM.Store(0xE, 0);
            Emulator.RAM.Store(0xF, 0);

            Emulator.Clock.Start();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Emulator.Reset();
        }
    }
}

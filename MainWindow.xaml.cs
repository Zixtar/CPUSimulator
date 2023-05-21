using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Threading;
using static CPUSimulator.Globals;


namespace CPUSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<int> lengths = new List<int>();
        List<int> indexes = new List<int>();
        Simulator simulator;
        List<string> instructionList = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        public void OpenDialog()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (openFileDialog.FileName.ToUpper().Contains(".ASM"))
                {
                    textBox.Text = openFileDialog.FileName;
                }
                else
                {
                    textBox.Text = "Va rugam selectati un fisier cu extensia .asm";
                }
            }
        }
        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenDialog();
        }

        private void assembleButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Length == 0) return;

            Assembler assembler = new Assembler();

            instructionList = File.ReadAllLines(textBox.Text).ToList();

            foreach (var instruction in instructionList)
            {
                resultTextBox.Text += instruction + '\r';
            }

            resultTextBox.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            resultTextBox.IsEnabled = true;
            resultTextBox.IsReadOnly = true;

            var microinstructionList = File.ReadAllLines(@"The Holy Grail\Microprogram.txt").Select(x=>x.Trim()).ToList();
            indexes.Add(0);

            foreach (var microinstruction in microinstructionList)
            {
                microprogramTextBox.Text += microinstruction + '\r';
                lengths.Add(microinstruction.Length);
                indexes.Add(indexes[indexes.Count - 1] + microinstruction.Length);
            }

            microprogramTextBox.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            microprogramTextBox.IsEnabled = true;
            microprogramTextBox.IsReadOnly = true;
            microprogramTextBox.Focus();
            microprogramTextBox.SelectionStart = 0;
            microprogramTextBox.SelectionLength = lengths[0];
            microprogramTextBox.SelectionBrush = Brushes.Yellow;
            microprogramTextBox.Focusable = false;
            var machineCode = assembler.ParseInstructionList(instructionList);

            simulator = new Simulator(machineCode);
            simulator.Start();

            assembleButton.Visibility = Visibility.Hidden;
            runButton.Visibility = Visibility.Visible;

        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (simulator == null) return;
            microprogramTextBox.Focusable = true;
            microprogramTextBox.Focus();
            if (!(simulator.Sequencer.stare == 2 || simulator.Sequencer.stare == 3))
            {
                microprogramTextBox.SelectionStart = indexes[MAR] + MAR;
                microprogramTextBox.SelectionLength = lengths[MAR];
            }
            microprogramTextBox.SelectionBrush = Brushes.Yellow;
            microprogramTextBox.Focusable = false;
            simulator.DoLoop();
        }

        private void runButton_Click(object sender, RoutedEventArgs e)
        {
            Thread runThread = new Thread(Run);
            runThread.Start();  
        }

        private void Run()
        {
            while (BPO)
            {
                simulator.DoLoop();
            }
        }
    }
}

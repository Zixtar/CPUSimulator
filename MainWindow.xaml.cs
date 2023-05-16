using System;
using System.Collections.Generic;
using System.IO;
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
using static CPUSimulator.Globals;

namespace CPUSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button browseButton, assembleButton, nextButton;
        TextBox textBox, resultTextBox, microprogramTextBox;
        List<int> lengths = new List<int>();
        List<int> indexes = new List<int>();
        string fileName;
        Simulator simulator;
        public MainWindow()
        {
            InitializeComponent();
            CreateControls();
        }

        public void CreateControls() //fereasca dumnezeu nu mai scrie UI in cod, pune-l in XAML
        {
            browseButton = new Button();
            browseButton.SetValue(Grid.RowProperty, 1);
            browseButton.SetValue(Grid.ColumnProperty, 3);
            browseButton.Click += browseButton_OnClick;
            browseButton.Content = "Browse";
            nextButton = new Button();
            nextButton.SetValue(Grid.RowProperty, 1);
            nextButton.SetValue(Grid.ColumnProperty, 7);
            nextButton.Click += nextButton_OnClick;
            nextButton.Content = "Next";
            assembleButton = new Button();
            assembleButton.SetValue(Grid.RowProperty, 1);
            assembleButton.SetValue(Grid.ColumnProperty, 5);
            assembleButton.Click += assembleButton_OnClick;
            assembleButton.Content = "Start assembler";
            textBox = new TextBox();
            textBox.SetValue(Grid.RowProperty, 1);
            textBox.SetValue(Grid.ColumnProperty, 1);
            textBox.IsEnabled = false;
            resultTextBox = new TextBox();
            resultTextBox.SetValue(Grid.RowProperty, 3);
            resultTextBox.SetValue(Grid.ColumnProperty, 1);
            resultTextBox.IsEnabled = false;
            microprogramTextBox = new TextBox();
            microprogramTextBox.SetValue(Grid.RowProperty, 3);
            microprogramTextBox.SetValue(Grid.ColumnProperty, 3);
            microprogramTextBox.IsEnabled = false;
            Grid.SetColumnSpan(microprogramTextBox, 3);
            mainGrid.Children.Add(browseButton);
            mainGrid.Children.Add(textBox);
            mainGrid.Children.Add(resultTextBox);
            mainGrid.Children.Add(assembleButton);
            mainGrid.Children.Add(microprogramTextBox);
            mainGrid.Children.Add(nextButton);
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

        public void browseButton_OnClick(object sender, EventArgs e)
        {
            OpenDialog();
        }

        public void assembleButton_OnClick(object sender, EventArgs e)
        {
            if (textBox.Text.Length == 0) return;

            Assembler assembler = new Assembler();

            var instructionList = new List<string>();
            instructionList = File.ReadAllLines(textBox.Text).ToList();


            foreach (var instruction in instructionList)
            {
                resultTextBox.Text += instruction + '\r';
            }

            resultTextBox.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            resultTextBox.IsEnabled = true;
            resultTextBox.IsReadOnly = true;

            var microinstructionList = File.ReadAllLines(@"The Holy Grail\Microprogram.txt").ToList();
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
        }

        public void nextButton_OnClick(object sender, EventArgs e)
        {
            if (simulator == null) return;
            simulator.DoLoop();

            microprogramTextBox.Focusable = true;
            microprogramTextBox.Focus();
            if (simulator.Sequencer.stare == 2 || simulator.Sequencer.stare == 3)
            {

                microprogramTextBox.SelectionStart = indexes[MAR-1] + MAR-1;
                microprogramTextBox.SelectionLength = lengths[MAR-1];
            }
            else
            {
                microprogramTextBox.SelectionStart = indexes[MAR] + MAR;
                microprogramTextBox.SelectionLength = lengths[MAR];
            }
            microprogramTextBox.SelectionBrush = Brushes.Yellow;
            microprogramTextBox.Focusable = false;
        }
    }
}

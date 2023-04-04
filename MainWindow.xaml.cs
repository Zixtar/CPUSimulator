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

namespace CPUSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button browseButton, assembleButton;
        TextBox textBox, resultTextBox;
        string fileName;
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
            Grid.SetColumnSpan(resultTextBox, 5);
            resultTextBox.IsEnabled = false;
            mainGrid.Children.Add(browseButton);
            mainGrid.Children.Add(textBox);
            mainGrid.Children.Add(resultTextBox);
            mainGrid.Children.Add(assembleButton);
        }

        public void OpenDialog()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if(openFileDialog.FileName.ToUpper().Contains(".ASM"))
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

            var machineCode = assembler.ParseInstructionList(instructionList);

            foreach(var instruction in machineCode)
            {
                resultTextBox.Text += Convert.ToString(instruction, 2) + '\r';
            }
        }
    }
}

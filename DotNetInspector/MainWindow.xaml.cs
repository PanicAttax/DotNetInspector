using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Windows.Forms;



namespace DotNetInspector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.Integration.WindowsFormsHost host;
        private System.Windows.Forms.FileDialog fileDialog;
        private AssemblyInfo assemblyInfo;
        private List<KeyValuePair<IEnumerable, TypeOfList>> backUps;
        private int position = -1;

        public MainWindow()
        {
            InitializeComponent();
            // Create host
            host = new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create OpenFileDialog
            fileDialog = new OpenFileDialog();

            // Create several buttons
            CreateButtons();
            this.SizeChanged += MainWindow_SizeChanged;
        }

        // It helps fit size of listBox to size of the window.
        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainGrid.Width = this.Width - Margin.Left * 2;
            MainGrid.Height = this.Height - Margin.Top * 2;
            listBoxMain.Width = this.Width - 200;
            listBoxMain.Height = this.Height - 60;

        }


        private void GetButton_Click(object sender, RoutedEventArgs e)
        {
            if (assemblyInfo != null)
            {
                // CheckListAndRequest will return TRUE, when something changed in ItemSource and we need a back up
                if (assemblyInfo.CheckListAndRequest(sender))
                {
                    position++;
                    backUps.Add(new KeyValuePair<IEnumerable, TypeOfList>(listBoxMain.ItemsSource, assemblyInfo.TypeNow));
                    
                }
            }
        }


        // After first click at the Choose assembly button, in case when receipt of assembly file is done successfully, initializing MainWindow class' fields.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Trying to get assembly from file and assign this to field, in case of success
                var file = fileDialog.FileName;
                var pickedAssembly = AssemblyHelper.GetAssembly(file);
                if (pickedAssembly != null)
                {
                    assemblyInfo = new AssemblyInfo(pickedAssembly, MainGrid);
                    backUps = new List<KeyValuePair<IEnumerable, TypeOfList>>();
                    labelFileName.Content = file.Substring(file.LastIndexOf("\\") + 1);
                    labelFileName.Visibility = Visibility.Visible;
                    listBoxMain.ItemsSource = new object[0];
                    backUps.Add(new KeyValuePair<IEnumerable, TypeOfList>(listBoxMain.ItemsSource,TypeOfList.Types));
                }
            }
        }
         
        // Creating few buttons by code, set location for it relatively "ChooseAssembly" button, that used like a reference.
        void CreateButtons()
        {
            var presetButton = buttonChooseAssembly;
            var newButtons = new System.Windows.Controls.Button[6];
            //var aliases = new string[6] {"Types",""}
            for (int i = 0; i < newButtons.Length; i++)
            {
                newButtons[i] = new System.Windows.Controls.Button();
                newButtons[i].Width = presetButton.Width;
                newButtons[i].HorizontalAlignment = presetButton.HorizontalAlignment;
                newButtons[i].VerticalAlignment = presetButton.VerticalAlignment;
                newButtons[i].Margin = new Thickness(
                    presetButton.Margin.Left,
                    presetButton.Margin.Top * (i + 2) + presetButton.Height * (i + 1) + labelFileName.Height,
                    presetButton.Margin.Right,
                    presetButton.Margin.Bottom);
                newButtons[i].Content = "Get " + (TypeOfList)i;
                newButtons[i].Name = $"buttonGet" + (TypeOfList)i;
                //newButtons[i].Visibility = Visibility.Hidden;
                newButtons[i].Click += GetButton_Click;
            }

            foreach (System.Windows.Controls.Button button in newButtons)
            {
                this.MainGrid.Children.Add(button);
            }
        }

        // "Back" button click handler. Check if there is no any elements in backup collection. In this case we will do nothing.
        // Otherwise we send key and value from collection from specified position, and reduce position by on in the end.
        private void ButtonBack_OnClick(object sender, RoutedEventArgs e)
        {
            if (position < 0) return;
            assemblyInfo.Back(backUps[position].Key, backUps[position].Value);
            backUps.RemoveAt(backUps.Count - 1);
            position--;
        }
    }
}

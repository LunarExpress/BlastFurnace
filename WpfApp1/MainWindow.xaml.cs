using BlastFurnace;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> ToChangeList = new List<string>();
        string TemplatePath = "";
        MainUtility program = new MainUtility();
        int count = 0;
        Run TemplateRun;
        Run MultipleRun;
        DataGrid DG;
        public List<ImageDataSource> ImageData { get; set; }
        bool TempReady = false;
        bool MultiReady = false;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += init;
            DataContext = this;
        }
        void init(object sender, RoutedEventArgs e)
        {
            TemplateRun = (Run)FindName("Template");
            MultipleRun = (Run)FindName("Multiple");
            DG = (DataGrid)FindName("DG1");
        }
        private void OnImportTemplateClicked(object sender, RoutedEventArgs e)
        {
            var OpenTemplateDialog = new OpenFileDialog();
            if (OpenTemplateDialog.ShowDialog() == true)
            {
                if (program.CheckTemplate(OpenTemplateDialog.FileName))
                {
                    TemplateRun.Text = "Status:OK";
                    TemplateRun.Foreground = Brushes.Green;
                    TemplatePath = OpenTemplateDialog.FileName;
                    TempReady = true;
                }
                else
                {
                    TemplateRun.Text = "Status:Error";
                    TemplateRun.Foreground = Brushes.Red;
                    TempReady = false;
                }
            }
        }
        private void OnImportMultipleClicked(object sender, RoutedEventArgs e)
        {
            var OpenMultipleDialog = new OpenFileDialog();
            OpenMultipleDialog.DefaultExt = ".txt";
            OpenMultipleDialog.Multiselect = true;
            OpenMultipleDialog.ShowDialog();
            List<string> CheckName = new List<string>(OpenMultipleDialog.FileNames);
            if (program.CheckMultiple(CheckName))
            {
                MultipleRun.Text = "Status:OK";
                MultipleRun.Foreground = Brushes.Green;
                ToChangeList = CheckName;
                MultiReady = true;
            }
            else
            {
                MultipleRun.Text = "Status:Error";
                MultipleRun.Foreground = Brushes.Red;
                MultiReady = false;
                MainUtility.Debuglog("材料文件不是有效的SDF字体！");
            }
            //foreach(var item in ToChangeList)
            //Program.Debuglog(item);
        }
        private void Merge(object sender, RoutedEventArgs e)
        {
            count++;
            if (TempReady && MultiReady) 
            {
                program.Main(TemplatePath, ToChangeList);
                RefreshDataGrid();
            }
            else
                MainUtility.Debuglog("两种物料尚未准备就绪！");
            return;
        }

        void RefreshDataGrid() 
        {
            ImageData = program.GetDataList();
            DG.ItemsSource = ImageData;
            DG.Items.Refresh();
            return;
        }
    }
}

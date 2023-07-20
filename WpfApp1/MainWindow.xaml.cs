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
using FolderBrowserForWPF;

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
        SdfDataBase db = new();
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
            GenerateComboBox();
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

        public void UseThis(object sender, RoutedEventArgs e)
        {
            ComboBox cb = (ComboBox)FindName("VersionBox");
            if (cb.SelectedIndex == -1) 
            {
                TemplateRun.Text = "Status:Empty";
                TemplateRun.Foreground = Brushes.Red;
                TempReady = false;
                return;
            }
            string selectValue = cb.SelectedValue.ToString();
            if (program.CheckTemplate(selectValue))
            {
                TemplateRun.Text = "Status:OK";
                TemplateRun.Foreground = Brushes.Green;
                TemplatePath = (string)cb.SelectedValue;
                TempReady = true;
            }
            else
            {
                TemplateRun.Text = "Status:Error";
                TemplateRun.Foreground = Brushes.Red;
                TempReady = false;
            }
        }

        public void SetDataBase(object sender,RoutedEventArgs e) 
        {
           Dialog dialog = new Dialog();
           string FolderPath="";
            if (dialog.ShowDialog()==true) 
            {
                FolderPath = dialog.FileName;
            }
            CreateRegistry(FolderPath);
            GenerateComboBox();

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

        public void GenerateComboBox() 
        {
            ComboBox comboBox = (ComboBox)FindName("VersionBox");
            if (db.UIVersionView() == null)
                return;
            List<SdfData> FileList = db.UIVersionView();
            comboBox.DataContext = this;
            comboBox.ItemsSource = FileList;
            comboBox.DisplayMemberPath = "FolderName";
            comboBox.SelectedValuePath = "TextPath";
        }

        void CreateRegistry(string paths) 
        {
            if (paths == "")
                throw new System.Exception("地址不存在");
            // 写入注册表
            Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("BlastFurnace", false);
            var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("BlastFurnace");
            key.SetValue("Path",paths);
        }
    }
}

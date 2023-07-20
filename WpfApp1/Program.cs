using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
using System.Windows.Controls;
using BlastFurnace;

namespace WpfApp1
{
    internal class MainUtility
    {
        FileStream OriginalFileStream;
        StreamReader? OriginalReadStream;
        List<string> OriginalStringArr = new();
        List<int> OriginalPathid = new();
        List<ImageDataSource> ImageDataList = new();
        public void Main(string TemplatePath,List<string> ToChangeList)
        {
            OriginalFileStream = new FileStream(TemplatePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            OriginalReadStream = new StreamReader(OriginalFileStream);
            //OriginalReadStream = new StreamReader(OriginalPath);
            

            OriginalStringArr = new List<string>(FilestreamToStringArr(OriginalReadStream));
            foreach (string OriginalString in OriginalStringArr)
            {
                if (OriginalString.Contains("PathID"))
                {
                    int pathid = int.Parse(Regex.Replace(OriginalString, "[^0-9]", ""));
                    OriginalPathid.Add(pathid);
                }
            }//写入pathid数组
            foreach (string ToChangeStr in ToChangeList)
            {
                FileStream ToChangeFileStream = new FileStream(ToChangeStr, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter ChangeStreamWriter = new StreamWriter(ToChangeFileStream);
                StreamReader ChangeStreamReader = new StreamReader(ToChangeFileStream);
                string FileName = Path.GetFileNameWithoutExtension(ToChangeFileStream.Name);
                string ThisLine;
                List<int> ToChangePathid = ReadPathID(ChangeStreamReader);
                ImageDataSource ImageSource = new ImageDataSource(FileName, ToChangePathid[4]);
                ImageDataList.Add(ImageSource);
                List<string> OriginalList = TransformReaderToList(OriginalReadStream);
                List<string> ChangedList = WriteInOriginal(OriginalList, ToChangePathid);//写入Pathid
                CleanAndFlush(ChangeStreamWriter, ChangedList);//清除ToChange的内容。直接粘贴写入后的Original
                //对于每个Tochange目标，读取pathid数组后写入original文本，再使用original替换掉目标。
            }
            OriginalFileStream.Close();
            Debuglog("Completed");
        }

        public List<ImageDataSource> GetDataList() 
        {
            return ImageDataList;
        }
        List<string> WriteInOriginal(List<string> Original,List<int>ToChangePathid) 
        {
            int index = 0;
            for (int i=0;i<Original.Count;i++) 
            {
                if (Original[i].Contains("PathID")) 
                {
                    if (index >= 5)
                        break;
                    Original[i] = Regex.Replace(Original[i], @"PathID\s*=\s*\d+", "PathID = " + ToChangePathid[index]);
                    index++;
                } 
            }
            return Original;
        }
        void CleanAndFlush(StreamWriter ToClean,List<string> ToCopy) 
        {
            // Clear the file stream by setting its length to 0
            ToClean.BaseStream.SetLength(0);
            ToCopy.Remove(ToCopy[ToCopy.Count - 1]);
            // Write the string array to the file stream
                foreach (var line in ToCopy)
                {
                    ToClean.WriteLine(line);
                }
            ToClean.Flush();
            ToClean.Close();
            
        }

        List<string> TransformReaderToList(StreamReader reader)
        {
            List<string> result = new List<string>(FilestreamToStringArr(reader));
            return result;
        }
        List<int> ReadPathID(StreamReader reader)
        {
            List<string> ToChangeText = TransformReaderToList(reader);
            List<int> result = new List<int>();
            foreach (string ToChangeRows in ToChangeText)
            {
                if (ToChangeRows.Contains("PathID"))
                {
                    int pathid = int.Parse(RegexExtract(ToChangeRows));
                    result.Add(pathid);
                }

            }//写入pathid数组
            return result;
        }
        string RegexExtract(string inputString) 
        {
            string pattern = @"m_PathID\s*=\s*(\d+)";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(inputString);
            if (match.Success)
            {
                string pathidValue = match.Groups[1].Value;
                return pathidValue;
            }
            else
                return "-7750";
        }

        public static void Debuglog(string path) 
        {
            Notifier notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(1),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
            notifier.ShowInformation(path);
        }
        string[] FilestreamToStringArr(StreamReader Origin) 
        {
            ReturnToTop(Origin);
            string[] lines;
            string text = Origin.ReadToEnd();
            lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            return lines;
        }
        void ReturnToTop(StreamReader obj) 
        {
           FileStream fileStream = (FileStream)obj.BaseStream;
           fileStream.Seek(0, SeekOrigin.Begin);
                // 继续使用 StreamReader 对象进行读取操作
        }
        public bool CheckMultiple(List<string> multi) 
        {
            if(0 == multi.Count) return false;
            foreach (string ToChangeStr in multi)
            {
                FileStream ToChangeFileStream = new FileStream(ToChangeStr, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamReader CheckReader = new StreamReader(ToChangeFileStream);
                List<string> arr = TransformReaderToList(CheckReader);
                ToChangeFileStream.Close();
                CheckReader.Close();
                if (arr.Count < 16 || !arr[15].Contains("m_SourceFontFileGUID")) 
                    return false;
            }
                return true;
        }
        public bool CheckTemplate(string temp) 
        {
            FileStream TemplateFileStream = new FileStream(temp, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader TemplateReader = new(TemplateFileStream);
            List<string> arr = TransformReaderToList(TemplateReader);
            TemplateFileStream.Close();
            TemplateReader.Close();
            if (arr.Count < 16 || !arr[15].Contains("m_SourceFontFileGUID"))
                return false;
            else
                return true;
        }
        
    }
}

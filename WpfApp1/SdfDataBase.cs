using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BlastFurnace
{
    internal class SdfDataBase
    {
        //从UI传入版本号
        //找到版本号对应的path并传出
        public List<SdfData> UIVersionView() 
        {
            string folderPath;
            var key = Registry.CurrentUser.OpenSubKey("BlastFurnace");
            if (key == null)
                return null;
            else
                folderPath = (string)key.GetValue("Path");
            string[] files = System.IO.Directory.GetDirectories(folderPath, "*", System.IO.SearchOption.TopDirectoryOnly);
            List<SdfData> SdfDb = new List<SdfData>();
            for (int i = 0; i < files.Length; i++) 
            {
                SdfDb.Add(new SdfData(files[i]));
            }
            return SdfDb;
        }


    }
}

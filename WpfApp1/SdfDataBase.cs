using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlastFurnace
{
    internal class SdfDataBase
    {
        //从UI传入版本号
        //找到版本号对应的path并传出
        public List<SdfData> UIVersionView() 
        {
            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SDFResources");
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

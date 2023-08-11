using System.IO;

namespace BlastFurnace
{
    internal class SdfData
    {
        public string FolderName { get; set; }
        public string FolderPath { get; set; }
        public string TextPath { get; set; }
        public SdfData(string FilePath) 
        {
            this.FolderPath = FilePath;
            this.FolderName = Path.GetFileName(FilePath);
            string[] txt = Directory.GetFiles(FolderPath, "*.txt", System.IO.SearchOption.TopDirectoryOnly);
            if (txt.Length >= 1)
                TextPath = txt[0];
            else
                return;
        }
    }
}

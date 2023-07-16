
namespace BlastFurnace
{
    public class ImageDataSource
    {
        public string name { get; set; }
        public int pathid { get; set; }

        public ImageDataSource(string name, int pathid)
        {
            this.name = name;
            this.pathid = pathid;
        }


    }

}

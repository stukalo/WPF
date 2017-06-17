using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace API
{
    public class DS_Bmp: IDS
    {
        private string _file;

        public DS_Bmp(string file)
        {
            _file = file;
        }

        public void Save(BmpBitmapEncoder encoder)
        {
            FileStream fs = File.Open(_file, FileMode.OpenOrCreate);
            encoder.Save(fs);
            fs.Close();
        }

        public Bitmap Load()
        {
            throw new NotImplementedException();
        }
    }
}

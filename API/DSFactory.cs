using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
        public class DSFactory
        {
            public static IDS GetInstance(string file)
            {
                string ext = Path.GetExtension(file);
                switch (ext)
                {
                    case "bmp":
                        return new DS_Bmp(file);
                    default:
                        return new DS_Bmp(file);
                }
            }
        }
    
}

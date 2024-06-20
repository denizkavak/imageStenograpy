using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace stenograpy
{
    class cnvrtForeach
    {
        public Form1 frm = new Form1();
        public void ToJPG(string folder)
        {


            for (int i = 0; i < 2; i++)
            {
                string extension = System.IO.Path.GetExtension(folder);
                if (extension == ".jpg" || extension==".JPG")
                {
                    string name = System.IO.Path.GetFileNameWithoutExtension(folder);
                    string path = System.IO.Path.GetDirectoryName(folder);
                    Image png = Image.FromFile(folder);

                    png.Save(path + @"\" + name + ".png", System.Drawing.Imaging.ImageFormat.Png);
                    png.Dispose();
                   
                }
            }
            
        }
    }
}

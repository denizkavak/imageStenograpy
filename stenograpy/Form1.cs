using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
namespace stenograpy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public String dosya1;
        public String ryol;
        public OpenFileDialog dosya = new OpenFileDialog();
        public SaveFileDialog saveFile = new SaveFileDialog();
        public Bitmap img;
        public String DosyaYolu;
        public int[,]resimb;
        public int[,] resimg;
        public double key,ortyp,karaktersayisi,bdeger,bdeger1,fark;
        public int mod,modheight;
        public int w, h;
        public string durum;
        Bitmap EncryptedImage;

        private void button4_Click(object sender, EventArgs e)
        {
            if (panel3.Visible == true)
            {
                panel3.Visible = false;
            }
            else
            {
                panel3.Visible = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.TransparencyKey = (BackColor);
        }
        int Move1;
        int Mouse_X;
        int Mouse_Y;
        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            Move1 = 0;
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            Move1 = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move1 == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        
        }

        private byte getByte(byte[] bits)
        {
            String bitString = "";
            for (int i = 0; i < 8; i++)
                bitString += bits[i];
            byte newpix = Convert.ToByte(bitString, 2);
            int dePix = (int)newpix;
            return (byte)dePix;
        } 
        private byte[] getBits(byte simplepixel)
        {
            int pixel = 0;
            pixel = (int)simplepixel;
            BitArray bits = new BitArray(new byte[] { (byte)pixel });
            bool[] boolarray = new bool[bits.Count];
            bits.CopyTo(boolarray, 0);
            byte[] bitsArray = boolarray.Select(bit => (byte)(bit ? 1 : 0)).ToArray();
            Array.Reverse(bitsArray);
            return bitsArray;
        }
        private byte[] getStringBits(string data)
        {
            byte[] text = System.Text.ASCIIEncoding.ASCII.GetBytes(data);
            BitArray bits = new BitArray(text);
            bool[] boolarray = new bool[bits.Count];
            bits.CopyTo(boolarray, 0);
            byte[] bitsArray = boolarray.Select(bit => (byte)(bit ? 1 : 0)).ToArray();
            Array.Reverse(bitsArray);
            return bitsArray;
        }
        public byte extract(Color pixel)
        {
            byte[] RedBits = getBits((byte)pixel.R);
            byte[] GreenBits = getBits((byte)pixel.G);
            byte[] BlueBits = getBits((byte)pixel.B);
            byte[] BitsToDecrypt = new byte[8];

            BitsToDecrypt[0] = RedBits[5];
            BitsToDecrypt[1] = GreenBits[5];
            BitsToDecrypt[2] = RedBits[6];
            BitsToDecrypt[3] = RedBits[7];
            BitsToDecrypt[4] = GreenBits[6];
            BitsToDecrypt[5] = GreenBits[7];
            BitsToDecrypt[6] = BlueBits[6];
            BitsToDecrypt[7] = BlueBits[7];

            return getByte(BitsToDecrypt);
        }
        /*This method is used to embed data into pixels*/
        public Color embed(Color pixel, byte[] bits)
        {

            byte[] RedBits = getBits((byte)pixel.R);
            byte[] GreenBits = getBits((byte)pixel.G);
            byte[] BlueBits = getBits((byte)pixel.B);

            /*LSB substition of RGB values is done as following:
            Red: Last 3 bits, Green: Last 3 bits, Blue: Last 2 bits
            This process lets us embed 1 byte in each pixel*/

            #region BitChange
            RedBits[5] = bits[0];
            GreenBits[5] = bits[1];
            RedBits[6] = bits[2];
            RedBits[7] = bits[3];
            GreenBits[6] = bits[4];
            GreenBits[7] = bits[5];
            BlueBits[6] = bits[6];
            BlueBits[7] = bits[7];

            byte newRed = getByte(RedBits);
            byte newGreen = getByte(GreenBits);
            byte newBlue = getByte(BlueBits);

            return Color.FromArgb(newRed, newGreen, newBlue);

        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            dosya.Filter = "Resim Dosyası |*.jpg;*.nef;*.png| Video|*.avi| Tüm Dosyalar |*.*";
            dosya.Title = "Lütfen Dosya Seçiniz .";
            dosya.ShowDialog();
            DosyaYolu = dosya.FileName;
            pictureBox2.ImageLocation = DosyaYolu;
            dosya1 = pictureBox2.ImageLocation;
            ryol = pictureBox2.ImageLocation;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Restart();
           
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            durum = "lsb";
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            durum = "fp";
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            durum = "dhy";
        }

        private void Button6_Click_1(object sender, EventArgs e)
        {
            bilgi b = new bilgi();
            this.Hide();
            b.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (panel2.Visible == true)
            {
                panel2.Visible = false;
            }
            else
            {
                panel2.Visible = true;
            }
        }
        
        public int a1,b1;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            dosya.Filter = "Resim Dosyası |*.jpg;*.nef;*.png| Video|*.avi| Tüm Dosyalar |*.*";
            dosya.Title = "Lütfen Dosya Seçiniz .";
            dosya.ShowDialog();
            DosyaYolu = dosya.FileName;
            pictureBox1.ImageLocation = DosyaYolu;
            dosya1 = pictureBox1.ImageLocation;
            ryol = pictureBox1.ImageLocation;
          
        }
        public String a;
        MD5Sifreleme MD5 = new MD5Sifreleme();
        private void button2_Click(object sender, EventArgs e)
        {
            if (durum == "lsb")
            {
                string textbox = mesaj.Text;
                char[] textarray = mesaj.Text.ToArray();

               img = new Bitmap(dosya1);
                

                /* Encoding process */
                #region Encoding

                //Embed type of data into last 3 pixels.
                #region type_embed
                // "tt1" is the code to define hidden data is a text message. (type:text:1)
                string[] type = new string[] { "t", "t", "1" };

                for (int j = 0; j < 3; j++)
                {
                    Color pixel =img.GetPixel(img.Width - j - 1,img.Height - 1);
                    pixel = embed(pixel, getStringBits(type[j]));
                   img.SetPixel(img.Width - j - 1,img.Height - 1, pixel);
                }

                #endregion

                // Embed length of message into 13 pixels in reverse [3:15]
                #region length_embed

                string a = Convert.ToString((mesaj.Text).Length);
                a = a.PadLeft(13, '0'); //Zero-padding
                char[] b = a.ToArray();

                for (int j = 3; j < 16; j++)
                {
                    string aString = Convert.ToString(b[j - 3]);
                    Color pixel =img.GetPixel(img.Width - j - 1,img.Height - 1);
                    pixel = embed(pixel, getStringBits(aString));
                   img.SetPixel(img.Width - j - 1,img.Height - 1, pixel);
                }

                #endregion

                int k = 0;
                

                for (int i = 0; i <img.Height; i++)
                {
                    for (int j = 0; j <img.Width; j++)
                    {
                        if (k < textbox.Length)
                        {
                            string msg = Convert.ToString(textarray[i + j]);
                            Color pixel =img.GetPixel(j, i);
                            pixel = embed(pixel, getStringBits(msg));
                           img.SetPixel(j, i, pixel);
                           
                            k++;
                        }

                    }
                }


                //mesaj.Text = mesaj.Text.Substring(0, mesaj.TextLength - 3);
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "Resim Dosyası |*.jpg;*.nef;*.png| Video|*.avi| Tüm Dosyalar |*.*";
                saveFile.Title = "Dosyanın Kaydedileceği Yeri Seçiniz ";
                saveFile.ShowDialog();
                dosya1 = saveFile.FileName.ToString();

                img.Save(dosya1);
                #endregion


            }
            else if (durum == "fp")
            {
                Bitmap img = new Bitmap(dosya1);
                mesaj.Text =  mesaj.Text + "a|~";
                ////for (int a = 0; a < mesaj.Text.Length; a++)
                //{
                //    char letter = Convert.ToChar(mesaj.Text.Substring(a, 1));
                //    int value = Convert.ToInt32(letter);
                //    richTextBox1.Text += value + "\n";


                //}//
                for (int i = 0; i < img.Width; i++)
                {
                    for (int j = 0; j < img.Height; j++)
                    {
                        Color pixel = img.GetPixel(i, j);

                        if (i < 1 && j < mesaj.Text.Length)
                        {
                            char letter = Convert.ToChar(mesaj.Text.Substring(j, 1));
                            int value = Convert.ToInt32(letter);
                            Console.WriteLine("Letter : " + letter + "  value :" + value);
                            img.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, value));
                        }
                        if (i == img.Width - 1 && j == img.Height - 1)
                        {
                            img.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, mesaj.TextLength));
                        }
                    }
                }
                //mesaj.Text = mesaj.Text.Substring(0, mesaj.TextLength - 3);
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "Resim Dosyası |*.jpg;*.nef;*.png| Video|*.avi| Tüm Dosyalar |*.*";
                saveFile.Title = "Dosyanın Kaydedileceği Yeri Seçiniz ";
                saveFile.ShowDialog();
                dosya1 = saveFile.FileName.ToString();

                img.Save(dosya1);
            }
            else if (durum == "dhy")
            {
                img = new Bitmap(dosya1);
                a = MD5Sifreleme.Encrypt(mesaj.Text);

                Clipboard.SetText(a);

                //ŞİFRELEME
                a = a + "a|~";
                w = img.Width;
                h = img.Height;
                w = w - 1;
                h = h - 1;
                mod = h % 6;
                if (mod == 0) { modheight = 0; }
                else { h = h - mod; }
                int c = w * h;
                resimb = new int[w, h];
                resimg = new int[w, h];
                Double toplam, ort;
                string extension = System.IO.Path.GetExtension(dosya1);
                if (extension == ".jpg" || extension == ".JPG")
                {
                    string name = System.IO.Path.GetFileNameWithoutExtension(dosya1);
                    string path = System.IO.Path.GetDirectoryName(dosya1);
                    cnvrtForeach cevir = new cnvrtForeach();
                    cevir.ToJPG(dosya1);
                    DosyaYolu = (path + @"\" + name + ".png");
                    pictureBox1.Image = Image.FromFile(DosyaYolu);
                    dosya1 = pictureBox1.ImageLocation;
                    ryol = pictureBox1.ImageLocation;
                    img = new Bitmap(DosyaYolu);
                }
                ////for (int a = 0; a < mesaj.Text.Length; a++)
                //{
                //    char karakter = Convert.ToChar(mesaj.Text.Substring(a, 1));
                //    int asc1 = Convert.ToInt32(karakter);
                //    richTextBox1.Text += asc1 + "\n";
                toplam = 0;
                //}
                //Diğer for'a kadar key kısmı
                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j = j + 3)
                    {
                        Color pixel = img.GetPixel(i, j);
                        Color color = img.GetPixel(i, j + 1);
                        bdeger = pixel.B;
                        bdeger1 = color.B;
                        fark = bdeger - bdeger1;
                        fark = Math.Abs(fark);
                        toplam = toplam + fark;
                        //if (i == img.Width -1 && j == img.Height -1)
                        //{
                        //    img.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, a.Length));
                        //}
                    }
                }
                fark = 0;
                ort = toplam / c;
                ort = Math.Ceiling(ort);
                key = ort;
                ortyp = 0;
                int zet, eksi, zet1, jet, jet1;
                ortyp = 0;
                eksi = 1;
                int sınır = 0;
                //Sınır Bulma
                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j = j + 3)
                    {
                        Color pixel = img.GetPixel(i, j);
                        Color color = img.GetPixel(i, j + 1);
                        bdeger = pixel.B;
                        bdeger1 = color.B;
                        fark = bdeger - bdeger1;
                        fark = Math.Abs(fark);
                        if (fark == key)
                        {
                            sınır = sınır + 1;
                        }
                    }
                }
                label3.Text = "Sınır :" + sınır;
                //Bozma For'u
                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j = j + 3)
                    {
                        Color pixel = img.GetPixel(i, j);
                        Color color = img.GetPixel(i, j + 1);
                        zet = pixel.B;
                        bdeger = pixel.B;
                        bdeger1 = color.B;
                        jet = color.B;
                        fark = bdeger - bdeger1;
                        fark = Math.Abs(fark);
                        if (fark == key)
                        {
                            karaktersayisi = karaktersayisi + 1;
                            if (karaktersayisi > a.Length)
                            {
                                if (zet == 0) { zet1 = zet; }
                                else { zet1 = zet - eksi; }
                                if (jet >= 254) { jet1 = jet; }
                                else { jet1 = jet + eksi +eksi; }
                                img.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, zet1));
                                img.SetPixel(i, j + 1, Color.FromArgb(color.R, color.G, jet1));
                            }
                        }
                    }
                }
                ortyp = 0;
                int kola = 0;
                //Kalan Şifrelene bilir pixel sayısı
                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j = j + 3)
                    {
                        Color pixel = img.GetPixel(i, j);
                        Color color = img.GetPixel(i, j + 1);
                        bdeger = pixel.B;
                        bdeger1 = color.B;
                        fark = bdeger - bdeger1;
                        fark = Math.Abs(fark);
                        if (fark == key)
                        {
                            ortyp = ortyp + 1;
                        }
                    }
                }
                MessageBox.Show(Convert.ToString(ortyp) + "Kalan Şifrelene Bilir Pixel");
                ortyp = 0;
                //Şifreleme For'u  
                Boolean kontrol1 = false;
                for (int i = 0; i < w; i++)
                {
                    if (kontrol1 == true)
                    {
                        break;
                    }
                    for (int j = 0; j < h; j = j + 3)
                    {
                        Color pixel = img.GetPixel(i, j);
                        Color color = img.GetPixel(i, j + 1);
                        Color cixel2 = img.GetPixel(i, j + 2);
                        bdeger = pixel.B;
                        bdeger1 = color.B;
                        fark = bdeger - bdeger1;
                        fark = Math.Abs(fark);
                        if (fark == key)
                        {
                            char karakter = Convert.ToChar(a.Substring(kola, 1));
                            int asc1 = Convert.ToInt32(karakter);
                            img.SetPixel(i, j + 2, Color.FromArgb(cixel2.R, cixel2.G, asc1));
                            ;
                            if (kola == a.Length)
                            {
                                kontrol1 = true;
                                break;
                            }
                            else
                            {
                                kola = kola + 1;
                            }
                            ortyp = ortyp + 1;
                        }
                    }
                }
                //MessageBox.Show(Convert.ToString(ortyp)+"Şifrelenen Pixel");
                Color cixel = img.GetPixel(w, h);
                img.SetPixel(w, h, Color.FromArgb(cixel.R, cixel.G, Convert.ToInt32(key)));
                //mesaj.Text = mesaj.Text.Substring(0, mesaj.TextLength - 3);
                saveFile.Filter = "Resim Dosyası |*.jpg;*.nef;| Video|*.avi| Tüm Dosyalar |*.*";
                saveFile.Title = "Dosyanın Kaydedileceği Yeri Seçiniz";
                saveFile.ShowDialog();
                dosya1 = saveFile.FileName.ToString();
                img.Save(dosya1);
            }

            }

        private void button3_Click(object sender, EventArgs e)
        {
            if (durum =="dhy")
            {

            img = new Bitmap(dosya1);
            w = img.Width;
            h = img.Height;
            w = w - 1;
            h = h - 1;

            mod = h % 6;
            if (mod == 0)
            {modheight = 0;}
            else
            {h = h - mod;}

            int msuzunluk = 0;
            String karakter1, karakter2;
            String mesaj = "";
            Boolean kontrol = false;
            Color lastPixel = img.GetPixel(w, h);
            int key = lastPixel.B;

            //msuzunluk bulma for'u
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j = j + 3)
                {
                    Color pixel = img.GetPixel(i, j);
                    Color color = img.GetPixel(i, j + 1);
                    Color cixel = img.GetPixel(i, j + 2);
                    bdeger = pixel.B;
                    bdeger1 = color.B;

                    fark = bdeger - bdeger1;

                    fark = Math.Abs(fark);
                    if (fark == key)
                    {
                        msuzunluk = msuzunluk + 1;
                        int asc1 = cixel.B;
                        char c = Convert.ToChar(asc1);
                        karakter1 = System.Text.Encoding.ASCII.GetString(new byte[] { Convert.ToByte(c) });
                        karakter2 = karakter1;
                        if (karakter2 == "~")
                        {
                            kontrol = true;
                            break;
                        }

                    }

                }
                if (kontrol == true)
                {
                    break;

                }
            }
            kontrol = false;
            //Şifre çözme for'u
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j = j + 3)
                {

                    Color pixel = img.GetPixel(i, j);
                    Color color = img.GetPixel(i, j + 1);
                    Color cixel = img.GetPixel(i, j + 2);

                    if (mesaj.Length == msuzunluk)
                    {
                        kontrol = true;
                        break;

                    }
                    bdeger = pixel.B;
                    bdeger1 = color.B;

                    fark = bdeger - bdeger1;

                    fark = Math.Abs(fark);
                    if (fark == key)
                    {
                        int asc1 = cixel.B;
                        char c = Convert.ToChar(asc1);
                        string karakter = System.Text.Encoding.ASCII.GetString(new byte[] { Convert.ToByte(c) });
                        mesaj = mesaj + karakter;
                    }

                }
                if (kontrol == true)
                {
                    break;

                }
            }
            //ŞİFRE ÇÖZME
            if (key > 10)
            {
                MessageBox.Show("Seçilen Resimde Gizlenmiş Şifre Bulunmamaktadır.");
                mesaj = "";
            }
            else
            {

                if ((mesaj[mesaj.Length - 3] == 'a') && (mesaj[mesaj.Length - 2] == '|') && (mesaj[mesaj.Length - 1] == '~'))
                {
                    string str = Convert.ToString(mesaj.Substring(0, msuzunluk - 3));
                    textBox1.Text = MD5Sifreleme.Decrypt(str);
                    //MessageBox.Show(Convert.ToString());
                }


                }
                MessageBox.Show("SONRA SONRA");
            }
            else if (durum == "lsb")
            {
                EncryptedImage = new Bitmap(pictureBox2.Image);

                /* Decoding process */
                #region Decoding

                int k = 0;
                string tlength = "";

                #region length_extract

                for (int j = 3; j < 16; j++)
                {
                    Color pixelToDecode = EncryptedImage.GetPixel(EncryptedImage.Width - j - 1, EncryptedImage.Height - 1);
                    byte delength = extract(pixelToDecode);
                    tlength += Convert.ToInt32(Encoding.ASCII.GetString(BitConverter.GetBytes(delength)));
                }

                int length = Convert.ToInt32(tlength);

                #endregion

                k = 0;

                for (int i = 0; i < EncryptedImage.Height; i++)
                {
                    for (int j = 0; j < EncryptedImage.Width; j++)
                    {
                        if (k < length)
                        {
                            Color pixelToDecode = EncryptedImage.GetPixel(j, i);                           
                            byte demsg = extract(pixelToDecode);
                           textBox1.Text += Encoding.ASCII.GetString(BitConverter.GetBytes(demsg));
                            k++;
                        }
                    }
                }

                #endregion
            }
            else if (durum == "fp")
            {
                Bitmap img = new Bitmap(dosya1);
                String mesaj = "";

                Color lastPixel = img.GetPixel(img.Width - 1, img.Height - 1);
                int msLength = lastPixel.B;

                for (int i = 0; i < img.Width; i++)
                {
                    for (int j = 0; j < img.Height; j++)
                    {
                        Color pixel = img.GetPixel(i, j);
                        if (i < 1 && j < msLength)
                        {
                            int value = pixel.B;
                            char c = Convert.ToChar(value);
                            string letter = System.Text.Encoding.ASCII.GetString(new byte[] { Convert.ToByte(c) });
                            mesaj = mesaj + letter;
                        }
                    }
                }
                if ((mesaj[mesaj.Length - 3] == 'a') && (mesaj[mesaj.Length - 2] == '|') && (mesaj[mesaj.Length - 1] == '~'))
                {
                    string str = Convert.ToString(mesaj.Substring(0, msLength - 3));
                    textBox1.Text = str;
                }
                else
                {
                    MessageBox.Show("Seçilen Resiimde Gizlenmiş Şifre Bulunmamaktadır.");
                    mesaj = "";
                }
            }

        }
    }
    #endregion
}



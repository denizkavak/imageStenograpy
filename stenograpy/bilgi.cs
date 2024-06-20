using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stenograpy
{
    public partial class bilgi : Form
    {
        public bilgi()
        {
            InitializeComponent();
        }

        private void Bilgi_Load(object sender, EventArgs e)
        {
            axAcroPDF1.LoadFile(@"C:\Users\JuFi\Downloads\yontem.pdf");
        }

        private void Bilgi_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 frm = new Form1();
            this.Hide();
            frm.Show();
        }
    }
}

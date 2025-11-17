using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom3
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void đóngChươngTrìnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void nhậpHóaĐơnBánHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tssl1.Text = "Đang nhập hóa đơn bán hàng!";
            Form1 f = new Form1();
            f.ShowDialog();
            tssl1.Text = "Ready!";
        }

        private void kếtThúcChươngTrìnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tsbLogOut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tsbLogin_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //100ml giây gọi một lần
            tssl2.Text = System.DateTime.Now.ToString();
        }

        private void nhậpHóaĐơnThanhToánCôngNợToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void danhMụcHàngHóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHH f = new frmHH();
            f.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }
    }
}

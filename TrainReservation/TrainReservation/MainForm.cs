using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrainReservation
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            IptalMemuru Iptal = new IptalMemuru();
            Iptal.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            IptalMemuru Iptal = new IptalMemuru();
            Iptal.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            RezervasyonMemuru Rezervasyon = new RezervasyonMemuru();
            Rezervasyon.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            RezervasyonMemuru Rezervasyon = new RezervasyonMemuru();
            Rezervasyon.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            SeyahatMemuru Seyahat = new SeyahatMemuru();
            Seyahat.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            SeyahatMemuru Seyahat = new SeyahatMemuru();
            Seyahat.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            YolcuMemuru Yolcu = new YolcuMemuru();
            Yolcu.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            YolcuMemuru Yolcu = new YolcuMemuru();
            Yolcu.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            TrenMemuru Tren = new TrenMemuru();
            Tren.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            TrenMemuru Tren = new TrenMemuru();
            Tren.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();

        }
    }
}

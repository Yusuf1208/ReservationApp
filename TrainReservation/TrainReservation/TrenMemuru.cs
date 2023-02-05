using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;



namespace TrainReservation
{
    public partial class TrenMemuru : Form
    {
        public TrenMemuru()
        {
            InitializeComponent();
            populate();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\YusufSahin\Documents\RailwaysDb.mdf;Integrated Security=True;Connect Timeout=30");
        
        private void populate()
        {
            Con.Open();
            string query = "select * from TrenTbl ";
            SqlDataAdapter  sda = new SqlDataAdapter(query,Con);
            var ds = new DataSet();
            sda.Fill(ds);
            TrenDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            string TrenDurumu= "";
            if(TrAdiTb.Text == "" || TrenKapTb.Text == "") 
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {
                if (RezerveRd.Checked == true)
                {
                    TrenDurumu = "Rezerve";
                }else if(BosRd.Checked == true)
                {
                    TrenDurumu = "Uygun";
                }
                   try
                {
                    Con.Open();
                    string Query = "insert into TrenTbl values ('" + TrAdiTb.Text + "'," + TrenKapTb.Text + " , '" + TrenDurumu + "')";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tren Basariyla Kaydedildi");
                    Con.Close();
                    populate();

                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

      

        private void sifirla()
        {
            TrAdiTb.Text = "";
            TrenKapTb.Text = "";
            RezerveRd.Checked = false;
            BosRd.Checked = false;
            key = 0;

        }
        private void button4_Click(object sender, EventArgs e)
        {
            sifirla();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int key = 0;
        private void TrenDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            TrAdiTb.Text = TrenDGV.SelectedRows[0].Cells[1].Value.ToString();
            TrenKapTb.Text = TrenDGV.SelectedRows[0].Cells[2].Value.ToString();
            if(TrAdiTb.Text== "")
            {
                key = 0;
            }
            else
            {
                key=Convert.ToInt32(TrenDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(key == 0)
            {
                MessageBox.Show("Silinecek treni secin");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "Delete from TrenTbl where TrenKodu="+key+"";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tren Basariyla Silindi");
                    Con.Close();
                    populate();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string TrenDurumu = "";
            if (TrAdiTb.Text == "" || TrenKapTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {
                if (RezerveRd.Checked == true)
                {
                    TrenDurumu = "Rezerve";
                }
                else if (BosRd.Checked == true)
                {
                    TrenDurumu = "Uygun";
                }
                try
                {
                    Con.Open();
                    string Query = "update TrenTbl set TrenAdi='"+ TrAdiTb.Text + "',TrenNumarasi="+ TrenKapTb.Text + ",TrenDurumu='"+ TrenDurumu + "'where TrenKodu="+key+";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tren Basariyla Güncellendi");
                    Con.Close();
                    populate();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MainForm Main = new MainForm();
            Main.Show();
            this.Hide();
        }
    }
}

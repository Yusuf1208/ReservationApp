using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrainReservation
{
    public partial class SeyahatMemuru : Form
    {
        public SeyahatMemuru()
        {
            InitializeComponent();
            populate();
            FillTKodu();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\YusufSahin\Documents\RailwaysDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select * from SeyahatTbl ";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            var ds = new DataSet();
            sda.Fill(ds);
            SeyahatDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void FillTKodu()
        {
            string TrenDurumu = "Uygun";
            Con.Open();
            SqlCommand cmd = new SqlCommand("select TrenKodu from TrenTbl where TrenDurumu ='"+ TrenDurumu +"' ", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TrainId",typeof(int));
            dt.Load(rdr);
            TKodu.ValueMember = "TrenKodu";
            TKodu.DataSource = dt;
            Con.Close();
        }
        private void SeyahatMemuru_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void ChangeStatus()
        {
            string TrenDurumu = "Uygun";
                try
                {
                    Con.Open();
                    string Query = "update TrenTbl set TrenDurumu='" + TrenDurumu + "'where TrenKodu=" + TKodu.SelectedValue.ToString() + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show("Tren Basariyla Güncellendi");
                    Con.Close();
                    populate();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (SucretTb.Text == "" || TKodu.SelectedIndex == -1 || NeredenCb.SelectedIndex == -1 || NereyeCb.SelectedIndex == -1)
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {
                
                try
                {
                    Con.Open();

                    string Query = "insert into SeyahatTbl values ('" +SeyahatGun.Text+ "','" + TKodu.SelectedValue.ToString()+"' , '" + NeredenCb.SelectedItem.ToString() + "', '" + NereyeCb.SelectedItem.ToString() + "', " + SucretTb.Text + ")";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seyahat Basariyla Kaydedildi");
                    Con.Close();
                    populate();
                    ChangeStatus();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void Reset()
        {
            NeredenCb.SelectedIndex = -1;
            NereyeCb.SelectedIndex = -1;
            //TKodu.SelectedIndex = -1;
            SucretTb.Text = "";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (NeredenCb.SelectedIndex == -1 || NereyeCb.SelectedIndex == -1 || SucretTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "update SeyahatTbl set SeyahatGunu='" + SeyahatGun.Text + "',Tren='" + TKodu.SelectedValue.ToString() + "',Basla='" + NeredenCb.SelectedItem.ToString() + "',Bitis='" + NereyeCb.SelectedItem.ToString() + "',Ucret=" + SucretTb.Text + " where SeyahatKodu=" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seyahat Basariyla Güncellendi");
                    Con.Close();
                    populate();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int key = 0;
        private void SeyahatDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SeyahatGun.Text = SeyahatDGV.SelectedRows[0].Cells[1].Value.ToString();
            TKodu.SelectedValue = SeyahatDGV.SelectedRows[0].Cells[2].Value.ToString();
            NeredenCb.SelectedItem = SeyahatDGV.SelectedRows[0].Cells[3].Value.ToString();
            NereyeCb.SelectedItem = SeyahatDGV.SelectedRows[0].Cells[4].Value.ToString();
            SucretTb.Text = SeyahatDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (TKodu.SelectedIndex == -1)
            {
                key = 0;
                //SucretTb.Text = "";
                //NeredenCb.SelectedIndex = -1;
                //NereyeCb.SelectedIndex = -1;

            }
            else
            {
                key = Convert.ToInt32(SeyahatDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainForm Main = new MainForm();
            Main.Show();
            this.Hide();
        }
    }
}

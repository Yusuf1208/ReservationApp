using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrainReservation
{
    public partial class YolcuMemuru : Form
    {
        public YolcuMemuru()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\YusufSahin\Documents\RailwaysDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select * from YolcuTbl ";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            var ds = new DataSet();
            sda.Fill(ds);
            YolcuDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Cinsiyet = "";
            if (YadiTb.Text == "" || YtelTb.Text == "" || YadresTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {
                if (ErkekRd.Checked == true)
                {
                    Cinsiyet = "Erkek";
                }
                else if (KadinRd.Checked == true)
                {
                    Cinsiyet = "Kadin";
                }
                try
                {
                    Con.Open();
                    string Query = "insert into YolcuTbl values ('" + YadiTb.Text + "'," + YtelTb.Text + " , '" + Cinsiyet + "', '" + UyrukCb.SelectedItem.ToString() + "', '" + YadresTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Yolcu Basariyla Kaydedildi");
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
        private void Reset()
        {
            YadiTb.Text = "";
            YtelTb.Text = "";
            YadresTb.Text = "";
            ErkekRd.Checked = false;
            KadinRd.Checked = false;
            UyrukCb.SelectedIndex = -1;
            key = 0;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Reset();
        }
        int key = 0;
        private void YolcuDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            YadiTb.Text = YolcuDGV.SelectedRows[0].Cells[1].Value.ToString();
            YtelTb.Text = YolcuDGV.SelectedRows[0].Cells[2].Value.ToString();
            UyrukCb.SelectedItem = YolcuDGV.SelectedRows[0].Cells[4].Value.ToString();
            YadresTb.Text = YolcuDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (YadiTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(YolcuDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Silinecek yolcuyu secin");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "Delete from YolcuTbl where PId="+key+"";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Yolcu Basariyla Silindi");
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

        private void button2_Click(object sender, EventArgs e)
        {
            string Cinsiyet = "";
            if (YadiTb.Text == "" || YtelTb.Text == "" || YadresTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {
                if (ErkekRd.Checked == true)
                {
                    Cinsiyet = "Erkek";
                }
                else if (KadinRd.Checked == true)
                {
                    Cinsiyet = "Kadin";
                }
                try
                {
                    Con.Open();
                    string Query = "update YolcuTbl set PAdi='" + YadiTb.Text + "',Pphone='" + YtelTb.Text + "',PCinsiyet='" + Cinsiyet + "',PNation='"+UyrukCb.SelectedItem.ToString()+"',PAdres='"+YadresTb.Text+"' where PId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Yolcu Basariyla Güncellendi");
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

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MainForm Main = new MainForm();
            Main.Show();
            this.Hide();

        }
    }
}

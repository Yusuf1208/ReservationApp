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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace TrainReservation
{
    public partial class IptalMemuru : Form
    {
        public IptalMemuru()
        {
            InitializeComponent();
            populate();
            FillBiletId();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\YusufSahin\Documents\RailwaysDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select * from IptalTbl ";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            var ds = new DataSet();
            sda.Fill(ds);
            IptalDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void FillBiletId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select BiletId from RezervasyonTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("BiletId", typeof(int));
            dt.Load(rdr);
            BidCb.ValueMember = "BiletId";
            BidCb.DataSource = dt;
            Con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (BidCb.SelectedIndex == -1 )
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {

                try
                {
                    Con.Open();
                    string Query = "insert into IptalTbl values (" + BidCb.SelectedValue.ToString() + ",'" + DateTime.Today.Date + "' )";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bilet İptal Edildi");
                    Con.Close();
                    populate();
                    kaldir();
                    FillBiletId();
                    BidCb.SelectedIndex = -1;

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void kaldir()
        {

            try
            {
                Con.Open();
                string Query = "Delete from RezervasyonTbl where BiletId=" + BidCb.SelectedValue.ToString() + "";
                SqlCommand cmd = new SqlCommand(Query, Con);
                cmd.ExecuteNonQuery();
                Con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

            
         }

        private void IptalMemuru_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainForm Main = new MainForm();
            Main.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

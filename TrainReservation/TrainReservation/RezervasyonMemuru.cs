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
    public partial class RezervasyonMemuru : Form
    {
        public RezervasyonMemuru()
        {
            InitializeComponent();
            populate();
            FillPId();
            FillSeyahatK();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (SeyahatKCb.SelectedIndex == -1 || YIdCb.SelectedIndex == -1 )
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {
                
                try
                {
                    Con.Open();
                    string Query = "insert into RezervasyonTbl values (" + YIdCb.SelectedValue.ToString() + ",'" + padi + "'  , " +SeyahatKCb.SelectedValue.ToString() + ", '" + Gun + "', '" + Basla + "', '" + Bitis + "', " + Ucret + ")";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rezevasyon Onaylandı");
                    Con.Close();
                    populate();
                    //Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\YusufSahin\Documents\RailwaysDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select * from RezervasyonTbl ";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            var ds = new DataSet();
            sda.Fill(ds);
            RezervasyonDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void FillPId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select PId from YolcuTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PId", typeof(int));
            dt.Load(rdr);
            YIdCb.ValueMember = "PId";
            YIdCb.DataSource = dt;
            Con.Close();
        }
        private void FillSeyahatK()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select SeyahatKodu from SeyahatTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("SeyahatKodu", typeof(int));
            dt.Load(rdr);
            SeyahatKCb.ValueMember = "SeyahatKodu";
            SeyahatKCb.DataSource = dt;
            Con.Close();
        }

        string padi;
        private void GetPAdi()
        {
            Con.Open();
            string mysql = "select * from YolcuTbl where PId=" + YIdCb.SelectedValue.ToString() + "";
            SqlCommand cmd= new SqlCommand(mysql, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda=new SqlDataAdapter(cmd);    
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                padi = dr["PAdi"].ToString();
            }
            Con.Close();
            MessageBox.Show(padi);
        }
        string Gun, Basla, Bitis;
        int Ucret;
        private void GetSeyahat()
        {
            Con.Open();
            string mysql = "select * from SeyahatTbl where SeyahatKodu=" + SeyahatKCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(mysql, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Gun = dr["SeyahatGunu"].ToString();
                Basla= dr["Basla"].ToString();
                Bitis = dr["Bitis"].ToString();
                Ucret = Convert.ToInt32(dr["Ucret"].ToString());
            }
            Con.Close();
            MessageBox.Show(Gun+Basla+Bitis+Ucret);
        }

        private void SeyahatKCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetSeyahat();
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

        private void RezervasyonMemuru_Load(object sender, EventArgs e)
        {

        }

        private void YIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetPAdi();
        }
    }
}

namespace TrainReservation
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(KadiTb.Text == "" || SifreTb.Text == "")
            {
                MessageBox.Show("Kullanici adi ve sifreyi girin");
            }else if(KadiTb.Text == "Merhaba" && SifreTb.Text == "Sifre")
            {
                MainForm Main = new MainForm();
                Main.Show();
                this.Hide();
            }else
            {
                MessageBox.Show("Kullanici adi veya sifreyi yanlis");
            }
        }
    }
}
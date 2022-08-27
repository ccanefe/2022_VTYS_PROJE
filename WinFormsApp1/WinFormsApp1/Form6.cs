using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class Form6 : Form
    {
        Form1 previous;

        NpgsqlConnection baglanti = new NpgsqlConnection("server= localHost; port=5432; Database=HastaneOtomasyonu; user ID= postgres; password=4468219");
        public Form6(Form1 parent)
        {
            previous = parent;
            InitializeComponent();
            teshisleriListele();
        }

        void teshisleriListele()
        {
            NpgsqlDataAdapter request = new NpgsqlDataAdapter("SELECT * FROM teshisler ", baglanti);
            DataSet ds = new DataSet();
            request.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select * from teshisekle(@p1, @p2)", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
            komut.Parameters.AddWithValue("@p2", textBox2.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Teshis başarılı bir şekilde eklendi.", "BILGI",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            teshisleriListele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            previous.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("delete from teshisler where teshis_id=@p1", baglanti);
            komut3.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Teshis başarılı bir şekilde silindi.", "BILGI",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            teshisleriListele();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            teshisleriListele();

        }
    }
}

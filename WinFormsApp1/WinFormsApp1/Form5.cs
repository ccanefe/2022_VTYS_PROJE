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

namespace WinFormsApp1
{
    public partial class Form5 : Form
    {
        Form1 previous;
        NpgsqlConnection baglanti = new NpgsqlConnection("server= localHost; port=5432; Database=HastaneOtomasyonu; user ID= postgres; password=4468219");
        public Form5(Form1 parent)
        {
            previous = parent;
            InitializeComponent();
            receteListele();
        }

        void receteListele()
        {
            NpgsqlDataAdapter request = new NpgsqlDataAdapter("SELECT * FROM recete ", baglanti);
            DataSet ds = new DataSet();
            request.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select * from receteekle(@p1, @p2, @p3, @p4)", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
            komut.Parameters.AddWithValue("@p2", int.Parse(textBox3.Text));
            komut.Parameters.AddWithValue("@p3", int.Parse(textBox2.Text));
            komut.Parameters.AddWithValue("@p4", int.Parse(textBox4.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Reçete başarılı bir şekilde eklendi.", "BILGI",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            receteListele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            previous.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from recete where recete_no=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Reçete başarılı bir şekilde silindi.", "BILGI",
              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            receteListele();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            receteListele();
        }
    }
}

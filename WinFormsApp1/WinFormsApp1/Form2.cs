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
    public partial class Form2 : Form
    {
        Form1 previous;
       
        NpgsqlConnection baglanti = new NpgsqlConnection("server= localHost; port=5432; Database=HastaneOtomasyonu; user ID= postgres; password=4468219");
        public Form2(Form1 parent)
        {
            previous = parent;
            InitializeComponent();
            hastalariListele();
        }

        void hastalariListele()
        {
            NpgsqlDataAdapter request = new NpgsqlDataAdapter("SELECT * FROM hasta ", baglanti);
            DataSet ds = new DataSet();
            request.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select * from hastaekle(@p8, @p1, @p2::timestamp with time zone, @p3,@p4::REAL, @p5::REAL, @p6, @p7)", baglanti);
            komut.Parameters.AddWithValue("@p8", int.Parse(textBox1.Text));
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox2.Text));
            komut.Parameters.AddWithValue("@p7", textBox3.Text);
            komut.Parameters.AddWithValue("@p2", DateTime.Parse(textBox4.Text));
            komut.Parameters.AddWithValue("@p3", textBox5.Text);
            komut.Parameters.AddWithValue("@p4", double.Parse(textBox6.Text));
            komut.Parameters.AddWithValue("@p5", double.Parse(textBox7.Text));
            komut.Parameters.AddWithValue("@p6", textBox8.Text);
            
            
                komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Hasta başarılı bir şekilde eklendi.","BILGI",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            hastalariListele();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("delete from hasta where hasta_id=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Hasta başarılı bir şekilde silindi.", "BILGI",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            hastalariListele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            previous.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            hastalariListele();
        }
    }
}

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

namespace _260423_NBUY
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       
        SqlConnection baglanti = new SqlConnection("Server=.;Database=Musteriler;User=sa;Pwd=123");
        private void Form1_Load(object sender, EventArgs e)
        {
            MusteriListele();
            MusterileriGetir();
            OdalariGetir();
        }

        private void MusteriListele()
        {
            SqlDataAdapter adp = new SqlDataAdapter("Select * from Musteriler", baglanti);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dgvListe.DataSource = dt;

        }
        private void MusterileriGetir()
        {
            SqlDataAdapter adp = new SqlDataAdapter("SELECT MusteriID,Ad+' '+Soyad as AdSoyad FROM Musteriler", baglanti);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            CmbMusteri.DataSource = dt;
            CmbMusteri.DisplayMember = "AdSoyad";
            CmbMusteri.ValueMember = "MusteriID";
        }
        private void OdalariGetir()
        {
            SqlDataAdapter adp = new SqlDataAdapter("SELECT OdaNo,OdaFiyati FROM Oda", baglanti);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            cmbOda.DataSource = dt;
            cmbOda.DisplayMember = "OdaNo";
            cmbOda.ValueMember = "OdaFiyati";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string adi = textBox1.Text;
            string soyad = textBox2.Text;
            string telefon = textBox3.Text;
            string mail = textBox4.Text;
            if (adi == string.Empty || soyad == string.Empty || telefon == string.Empty || mail == string.Empty)
            {
                MessageBox.Show("Lütfen alanları doldurun.");
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = string.Format("insert musteriler(Ad,Soyad,Telefon,Mail) values ('{0}','{1}','{2}','{3}')", adi, soyad, telefon, mail);
                cmd.Connection = baglanti;
                baglanti.Open();
                int etk = cmd.ExecuteNonQuery();
                if (etk > 0)
                {
                    MessageBox.Show("Kayıt Eklendi.");
                    MusteriListele();
                }
                else
                {
                    MessageBox.Show("Hata.");
                }
                baglanti.Close();
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
                textBox3.Text = string.Empty;
                textBox4.Text = string.Empty;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string adi = textBox1.Text;
            string soyad = textBox2.Text;
            string telefon = textBox3.Text;
            string mail = textBox4.Text;
            int musteriID = Convert.ToInt32(dgvListe.CurrentRow.Cells["MusteriID"].Value); // Seçilen satırdaki MüşteriID değerini al

            SqlCommand komut = new SqlCommand();
            komut.CommandText = string.Format("UPDATE musteriler SET Ad='{0}', Soyad='{1}', Telefon='{2}', Mail='{3}' WHERE MusteriID={4}", adi, soyad, telefon, mail, musteriID);
            komut.Connection = baglanti;

            try
            {
                baglanti.Open();
                int etk = komut.ExecuteNonQuery();
                if (etk > 0)
                {
                    MessageBox.Show("Güncellendi.");
                    MusteriListele();
                }
                else
                {
                    MessageBox.Show("Güncelleme Başarısız.");
                }
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvListe.SelectedRows.Count == 0) // Hiçbir satır seçilmediyse
            {
                MessageBox.Show("Lütfen önce bir satır seçin.");
                return;
            }

            int musteriID = Convert.ToInt32(dgvListe.CurrentRow.Cells["MusteriID"].Value); // Seçilen satırdaki MüşteriID değerini al

            if (MessageBox.Show("Seçilen müşteri kaydını silmek istediğinize emin misiniz?", "Müşteri Silme", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand();
                komut.CommandText = string.Format("DELETE FROM musteriler WHERE MusteriID={0}", musteriID);
                komut.Connection = baglanti;

                try
                {
                    baglanti.Open();
                    int etk = komut.ExecuteNonQuery();
                    if (etk > 0)
                    {
                        MessageBox.Show("Silindi.");
                        MusteriListele();
                    }
                    else
                    {
                        MessageBox.Show("Silme Başarısız.");
                    }
                    baglanti.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
 
        }
    }
}


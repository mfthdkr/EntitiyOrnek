using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntitiyOrnek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private void btnDersListesi_Click(object sender, EventArgs e)
        {   
            // Adonet
            SqlConnection baglanti = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DbSinavOgrenci;Integrated Security=True");
            SqlCommand komut = new SqlCommand("Select * From tbldersler",baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnOgrenciListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.TBLOGRENCI.ToList();

            dataGridView1.Columns[3].Visible =false;
            dataGridView1.Columns[4].Visible =false;
        }

        private void btnNotListesi_Click(object sender, EventArgs e)
        {

            var query = from item in db.TBLNOTLAR
                select new {item.NOTID,item.OGR, item.DERS, item.SINAV1,item.SINAV2,item.SINAV3,item.ORTALAMA,item.DURUM};
            dataGridView1.DataSource = query.ToList();
            //   dataGridView1.DataSource = db.TBLNOTLAR.ToList();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            TBLOGRENCI t = new TBLOGRENCI();
            t.AD = txtAd.Text;
            t.SOYAD = txtSoyad.Text;
            db.TBLOGRENCI.Add(t);
            db.SaveChanges();
            MessageBox.Show("Öğrenci listeye eklendi.");
        }

        private void btnDersEkle_Click(object sender, EventArgs e)
        {
            TBLDERSLER tbldersler = new TBLDERSLER();
            tbldersler.DERSAD = txtDersAd.Text;
            db.TBLDERSLER.Add(tbldersler);
            db.SaveChanges();
            MessageBox.Show("Yeni ders eklendi");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtOgrenciId.Text);
            var x = db.TBLOGRENCI.Find(id);
            db.TBLOGRENCI.Remove(x);
            db.SaveChanges();
            MessageBox.Show("Öğrenci sistemden silindi.");
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtOgrenciId.Text);
            var x = db.TBLOGRENCI.Find(id);
            x.AD = txtAd.Text;
            x.SOYAD = txtSoyad.Text;
            x.FOTOGRAF = txtFoto.Text;
            db.SaveChanges();
            MessageBox.Show("Öğrenci bilgileri başarıyla güncellendi.");

        }

        private void btnProsedur_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.NOTLISTESI();
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.TBLOGRENCI.Where(x => x.AD == txtAd.Text | x.SOYAD == txtSoyad.Text).ToList();
        }

        private void txtAd_TextChanged(object sender, EventArgs e)
        {
            string aranan = txtAd.Text;
            var degerler = from item in db.TBLOGRENCI
                where item.AD.StartsWith(aranan)
                select item;
            dataGridView1.DataSource = degerler.ToList();
        }

        private void btnLinqEntity_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                List<TBLOGRENCI> liste1 = db.TBLOGRENCI.OrderBy(p => p.AD).ToList();
                dataGridView1.DataSource = liste1;
            }

            if (radioButton2.Checked == true)
            {
                List<TBLOGRENCI> liste2 = db.TBLOGRENCI.OrderByDescending(p => p.AD).ToList();
                dataGridView1.DataSource = liste2;
            }

            if (radioButton3.Checked == true)
            {
                List<TBLOGRENCI> liste3 = db.TBLOGRENCI.OrderBy(p => p.AD).Take(3).ToList();
                dataGridView1.DataSource = liste3;
            }

            if (radioButton4.Checked == true)
            {
                List<TBLOGRENCI> liste4 = db.TBLOGRENCI.Where(p => p.ID.ToString() == txtOgrenciId.Text).ToList();
                dataGridView1.DataSource = liste4;
            }

            if (radioButton5.Checked == true)
            {
                List<TBLOGRENCI> liste5 = db.TBLOGRENCI.Where(p => p.AD.StartsWith("a")).ToList();
                dataGridView1.DataSource = liste5;
            }

            if (radioButton6.Checked == true)
            {
                List<TBLOGRENCI> liste6 = db.TBLOGRENCI.Where(p => p.AD.EndsWith("a")).ToList();
                dataGridView1.DataSource = liste6;
            }

            if (radioButton7.Checked == true)
            {
                bool deger = db.TBLKULUPLER.Any();
                MessageBox.Show(deger.ToString(),"Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

            if (radioButton8.Checked == true)
            {
                var count = db.TBLOGRENCI.Count();
                MessageBox.Show(count.ToString());
            }

            if (radioButton9.Checked == true)
            {
                var sumSinav1 = db.TBLNOTLAR.Sum(p=>p.SINAV1).ToString();
                MessageBox.Show(sumSinav1);
            }
            if (radioButton10.Checked == true)
            {
                var avgSinav1 = db.TBLNOTLAR.Average(p => p.SINAV1);
                MessageBox.Show(avgSinav1.ToString());
            }

            if (radioButton11.Checked == true)
            {
                var avgSinav1 = db.TBLNOTLAR.Average(p => p.SINAV1);
                dataGridView1.DataSource = db.TBLNOTLAR.Where(p => p.SINAV1 >= avgSinav1).ToList();
            }

            if (radioButton12.Checked == true)
            {
                var enyuksek = db.TBLNOTLAR.Max(p => p.SINAV1);
                MessageBox.Show(enyuksek.ToString());
            }
            if (radioButton13.Checked == true)
            {
                var endusuk = db.TBLNOTLAR.Min(p => p.SINAV1);
                MessageBox.Show(endusuk.ToString());
            }

            if (radioButton14.Checked == true)
            {

            }
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            var sorgu = from d1 in db.TBLNOTLAR
                join d2 in db.TBLOGRENCI on d1.OGR equals d2.ID
                join d3 in  db.TBLDERSLER on d1.DERS equals d3.DERSID 
                select new
                {
                    ÖĞRENCİ = d2.AD + " " + d2.SOYAD,
                    DERS = d3.DERSAD,
                    //SOYAD = d2.SOYAD,
                    SINAV1 = d1.SINAV1,
                    SINAV2 = d1.SINAV2,
                    SINAV3 = d1.SINAV3,
                    ORTALAMA = d1.ORTALAMA
                };
            dataGridView1.DataSource = sorgu.ToList();

        }
    }
}

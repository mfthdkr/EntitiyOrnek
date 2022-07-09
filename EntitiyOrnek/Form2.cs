using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntitiyOrnek
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();
        private void btnLinq_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked==true)
            {
                var sorgu = db.TBLNOTLAR.Where(p => p.SINAV1 <= 50);
                dataGridView1.DataSource= sorgu.ToList();
            }

            if (radioButton2.Checked==true)
            {
                var sorgu = db.TBLOGRENCI.Where(p => p.AD == "Ali");
                dataGridView1.DataSource = sorgu.ToList();
            }

            if (radioButton3.Checked == true){

                var sorgu = db.TBLOGRENCI.Where(p => p.AD == textBox1.Text || p.SOYAD == textBox1.Text );
                dataGridView1.DataSource = sorgu.ToList();
            }

            if (radioButton4.Checked == true)
            {
                var sorgu = db.TBLOGRENCI.Select(x=>new {soyadı =x.SOYAD});
                dataGridView1.DataSource = sorgu.ToList();
            }

            if (radioButton5.Checked == true)
            {
                var sorgu = db.TBLOGRENCI.Select(x => new
                {
                    AD = x.AD.ToUpper(),
                    soyad = x.SOYAD.ToLower()
                });

                dataGridView1.DataSource = sorgu.ToList();
            }

            if (radioButton6.Checked == true)
            {
                var sorgu = db.TBLOGRENCI.Select(x => new
                {
                    ADI = x.AD.ToUpper(),
                    soyad = x.SOYAD.ToLower()
                }).Where(x => x.ADI != "Ali");

                dataGridView1.DataSource = sorgu.ToList();
            }

            if (radioButton7.Checked ==true)
            {
                var sorgu = db.TBLNOTLAR.Select(x => new
                {
                    ÖğrenciAd = x.OGR,
                    Ortalaması = x.ORTALAMA,
                    Durum  = x.DURUM == true ? "Geçti" : "Kaldı"
                });
                dataGridView1.DataSource = sorgu.ToList();
            }

            if (radioButton8.Checked == true)
            {
                var sorgu = db.TBLNOTLAR.SelectMany(x => db.TBLOGRENCI.Where(p => p.ID == x.OGR),(x,p)=>new
                {
                    Adı = p.AD,
                    Ders = x.DERS,
                    Ortalama = x.ORTALAMA,
                    Durum = x.DURUM == true ? "Geçti" : "Kaldı"
                });
                dataGridView1.DataSource = sorgu.ToList();
            }
            if (radioButton9.Checked == true)
            {
                var sorgu = db.TBLOGRENCI.OrderBy(x=>x.AD).Take(3);
                dataGridView1.DataSource = sorgu.ToList();
            }
            if (radioButton10.Checked == true)
            {
                var sorgu = db.TBLOGRENCI.OrderByDescending(x => x.ID).Take(3);
                dataGridView1.DataSource = sorgu.ToList();
            }

            if (radioButton11.Checked == true)
            {
                var sorgu = db.TBLOGRENCI.OrderBy(x => x.AD);
                dataGridView1.DataSource = sorgu.ToList();
            }
            if (radioButton12.Checked == true)
            {
                var sorgu = db.TBLOGRENCI.OrderBy(x=>x.ID).Skip(5);
                dataGridView1.DataSource = sorgu.ToList();
            }
        }

        
    }
}

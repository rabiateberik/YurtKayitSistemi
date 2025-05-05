using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace YurtKayitSistemi
{
    public partial class FrmOgrKayit : Form
    {
        public FrmOgrKayit()
        {
            InitializeComponent();
        }
        SqlBaglantim bgl=new SqlBaglantim();
       
        private void FrmOgrKayit_Load(object sender, EventArgs e)
        {
            //Bölümleri Listeleme Komutları
            SqlCommand komut = new SqlCommand("Select BolumAd From Bolumler" ,bgl.baglanti());
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                cmbBolum.Items.Add(oku[0].ToString());
            }
            bgl.baglanti().Close();

            //Boş Odaları Listeleme Komutları
            SqlCommand komut2 = new SqlCommand("Select Odano From Odalar where OdaDurum!=OdaAktif", bgl.baglanti());
             SqlDataReader oku2 = komut2.ExecuteReader();
            while (oku2.Read())
            {
                cmbOdaNo.Items.Add(oku2[0].ToString());
            }
            bgl.baglanti().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //öğrenci bilgilerinin kayıt edilme komutları
            try
            {
                SqlCommand komutKaydet = new SqlCommand("insert into Ogrenci(OgrAd,OgrSoyad,OgrTc,OgrTelefon,OgrDogum,OgrBolum," +
                    "OgrMail,OgrOdaNo,OgrVeliAdSoyad,OgrVeliTelefon,OgrVeliAdres) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", bgl.baglanti());
                komutKaydet.Parameters.AddWithValue("@p1", txtOgrAd.Text);
                komutKaydet.Parameters.AddWithValue("@p2", txtOgrSoyad.Text);
                komutKaydet.Parameters.AddWithValue("@p3", mskTC.Text);
                komutKaydet.Parameters.AddWithValue("@p4", mskOgrTelefon.Text);
                komutKaydet.Parameters.AddWithValue("@p5", mskDogum.Text);
                komutKaydet.Parameters.AddWithValue("@p6", cmbBolum.Text);
                komutKaydet.Parameters.AddWithValue("@p7", txtMail.Text);
                komutKaydet.Parameters.AddWithValue("@p8", cmbOdaNo.Text);
                komutKaydet.Parameters.AddWithValue("@p9", txtVeliAdSoyad.Text);
                komutKaydet.Parameters.AddWithValue("@p10", mskVeliTelefon.Text);
                komutKaydet.Parameters.AddWithValue("@p11", rchAdres.Text);
                komutKaydet.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kayıt Başarılı Bir Şekilde Eklendi");
               
                //Öğrenci id yi label e çekme
                SqlCommand komut=new SqlCommand("select Ogrid from Ogrenci",bgl.baglanti());
                SqlDataReader oku=komut.ExecuteReader();
                while (oku.Read())
                {
                    label12.Text = oku[0].ToString();
                }
                bgl.baglanti().Close();




                //Öğrenci borç alanı oluşturma
                SqlCommand komutKaydet2 = new SqlCommand("insert into Borclar (Ogrid,OgrAd,OgrSoyad) values(@b1,@b2,@b3)",bgl.baglanti());
                komutKaydet2.Parameters.AddWithValue("@b1",label12.Text);
                komutKaydet2.Parameters.AddWithValue("@b2", txtOgrAd.Text);
                komutKaydet2.Parameters.AddWithValue("@b3",txtOgrSoyad.Text);
                komutKaydet2.ExecuteNonQuery();
                bgl.baglanti().Close();
            }
            catch (Exception)
            {

                MessageBox.Show("HATA!!! Lütfen yeniden deneyin");
            }

            //Öğrenci Oda Kontenjanı Arttırma

            SqlCommand komutoda = new SqlCommand("update Odalar set  OdaAktif=OdaAktif+1 where OdaNo=@oda1",bgl.baglanti());
            komutoda.Parameters.AddWithValue("@oda1",cmbOdaNo.Text);
            komutoda.ExecuteNonQuery();
            bgl.baglanti().Close();
        }
    }
}

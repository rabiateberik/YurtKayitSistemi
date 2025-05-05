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
    public partial class FrmGiderGuncelle : Form
    {
        public FrmGiderGuncelle()
        {
            InitializeComponent();
        }
        public string elektrik, su, dogalGaz, gida, diger, internet, personel,id;

         
        SqlBaglantim bgl=new SqlBaglantim();
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
           

             try
             {
                 SqlCommand komut = new SqlCommand("update Giderler set Elektrik=@p1,Su=@p2,Dogalgaz=@p3,internet=@p4,Gida=@p5,Personel=@p6,Diger=@p7 where Odemeid=@p8", bgl.baglanti());
                 komut.Parameters.AddWithValue("@p8", txtGiderId.Text);
                 komut.Parameters.AddWithValue("@p1", txtElektrik.Text);
                 komut.Parameters.AddWithValue("@p2", txtSu.Text);
                 komut.Parameters.AddWithValue("@p3", txtDogalGaz.Text);
                 komut.Parameters.AddWithValue("@p4", txtInternet.Text);
                 komut.Parameters.AddWithValue("@p5", txtGida.Text);
                 komut.Parameters.AddWithValue("@p6", txtPersonel.Text);
                 komut.Parameters.AddWithValue("@p7", txtDiger.Text);
                 komut.ExecuteNonQuery();
                 bgl.baglanti().Close();
                 MessageBox.Show("Güncelleme yapıldı");
             }
             catch (Exception)
             {

                 MessageBox.Show("Hata oluştu. Yeniden deneyin.");
             }
        }

        private void FrmGiderGuncelle_Load(object sender, EventArgs e)
        {
            txtGiderId.Text = id;
            txtElektrik.Text = elektrik;
            txtGida.Text = gida;
            txtSu.Text = su;
            txtPersonel.Text = personel;
            txtInternet.Text = internet;
            txtDogalGaz.Text = dogalGaz;
            txtDiger.Text = diger;
        }
    }
}

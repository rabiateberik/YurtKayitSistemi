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
    public partial class FrmPersonel : Form
    {
        public FrmPersonel()
        {
            InitializeComponent();
        }
        SqlBaglantim bgl=new SqlBaglantim();

        private void FrmPersonel_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'yurtOtomasyonuDataSet7.Personel' table. You can move, or remove it, as needed.
            this.personelTableAdapter.Fill(this.yurtOtomasyonuDataSet7.Personel);

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Personel (PersonelAdSoyad,PersonelDepartman) values(@p1,@p2)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtPersonelAd.Text);
            komut.Parameters.AddWithValue("@p2",txtPersonelGorev.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel eklendi");
            this.personelTableAdapter.Fill(this.yurtOtomasyonuDataSet7.Personel);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from Personel where Personelid=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtPersonelid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel silindi");
            this.personelTableAdapter.Fill(this.yurtOtomasyonuDataSet7.Personel);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen;
            secilen = dataGridView1.SelectedCells[0].RowIndex;
            string PerAd, PerGorev, Perid;
            Perid = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            PerAd = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            PerGorev = dataGridView1.Rows[secilen].Cells[2].Value.ToString();

            txtPersonelid.Text = Perid;
            txtPersonelAd.Text = PerAd;
            txtPersonelGorev.Text = PerGorev;
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Personel set PersonelAdSoyad=@p2,PersonelDepartman=@p3 where Personelid=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtPersonelid.Text);
            komut.Parameters.AddWithValue("@p2",txtPersonelAd.Text);
            komut.Parameters.AddWithValue("@p3", txtPersonelGorev.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Güncellendi");
            this.personelTableAdapter.Fill(this.yurtOtomasyonuDataSet7.Personel);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
 using System.Data.SqlClient;
using System.IO;


namespace pTakip
{
    public partial class personel : Form
    {
        public personel()
        {
            InitializeComponent();
        }
      
        SqlConnection baglanti;//sql bağlantı adresi
        SqlDataAdapter adaptor;//sorgu ve bağlantı
        SqlCommand komut;//komut
        DataTable tablo;
       public static  string foto;
        void veriAl()
        {


            baglanti = new SqlConnection("server=.;Initial Catalog=dbPersonel;Integrated Security=SSPI");
            baglanti.Open();
            adaptor = new SqlDataAdapter("Select ad,soyad, tel,mail,gorev,gorevYeri From tblpersonel", baglanti);
            // ds = new DataSet();
            DataTable tablo = new DataTable();
            adaptor.Fill(tablo);
            tblper.DataSource = tablo;
            baglanti.Close();
        }
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void personel_Load(object sender, EventArgs e)
        {

            this.Text = "Personel ekranı";//form yazısını değiştirdim
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;//burada sadece form yapısının değiştirilmesini engelledim
            veriAl();
            //nesneler için sınırlamaları veriyorum
            lblperadsoyad.ForeColor = Color.DarkRed;
            txtyenisifre .MaxLength = 6;//şifre 6 karakterden fazla olamaz kıstı
            txtyenisifretekrar.MaxLength = 6;

            //yan taraftaki aktif kişi resmi
            pctrperresim.Height = 150;
            pctrperresim.Width = 150;
            pctrperresim.SizeMode = PictureBoxSizeMode.StretchImage;
            //bilgi sayfasındaki resim
            pctrresim1 .Height = 200;
            pctrresim1.Width = 200;
            pctrresim1.SizeMode = PictureBoxSizeMode.StretchImage;
            //resim yükle alanındaki resim kutusu
            pctrresimyukle.Height = 250;
            pctrresimyukle.Width = 250;
            pctrresimyukle.SizeMode = PictureBoxSizeMode.StretchImage;
            try
                //veri tabanından alınan resim yolunu giriş yaaprken bu sayfadaki değişkene eşitledim ve dosya yolunu burdan veridim
            {
                //pctrperresim.Image = Image.FromFile(Application.StartupPath + "\\resimler\\civciv2.jpg");
                //pctrresim1.Image = Image.FromFile(Application.StartupPath + "\\resimler\\civciv2.jpg");
                //pctrresimyukle.Image = Image.FromFile(Application.StartupPath + "\\resimler\\civciv2.jpg");
                pctrperresim.Image = Image.FromFile(Application.StartupPath + "\\resimler\\" + foto);
                pctrresim1.Image = Image.FromFile(Application.StartupPath + "\\resimler\\" + foto);
                pctrresimyukle.Image = Image.FromFile(Application.StartupPath + "\\resimler\\" + foto);

            }
            catch
            //eğer kullanıcının resmi yoksa resimler klasörüne ekldeğim resim yok adlı resmi koydum kullanıcı resmine boş durmasın diye
            {
                pctrperresim.Image = Image.FromFile(Application.StartupPath + "\\resimler\\resimYok.png");
                pctrresim1.Image = Image.FromFile(Application.StartupPath + "\\resimler\\resimYok.png");
                pctrresimyukle.Image = Image.FromFile(Application.StartupPath + "\\resimler\\resimYok.png");

            }
        }

        private void btncikis_Click(object sender, EventArgs e)
        {
            giris giris = new giris();
            giris.Show();
            this.Hide();
          
        }
    }
}

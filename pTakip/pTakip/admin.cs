using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace pTakip
{
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
        }
        SqlConnection baglanti;//sql bağlantı adresi
        SqlDataAdapter adaptor;//sorgu ve bağlantı
        SqlCommand komut;//komut
        DataTable tablo;
        DataSet ds;
        public static string foto;
        int seçilensatır;
        void veriAl()
        {


            baglanti = new SqlConnection("server=.;Initial Catalog=dbPersonel;Integrated Security=SSPI");
            baglanti.Open();
            adaptor = new SqlDataAdapter("Select * From tblpersonel", baglanti);

            DataTable tablo = new DataTable();
            adaptor.Fill(tablo);
            tbladminveri.DataSource = tablo;
            baglanti.Close();
            btnkaydet.Enabled = false;
            btnsil.Enabled = true;
            btnguncelle.Enabled = true;
        }
        public void temizle()
        {
            lblid.Text = "";
            txtadminad.Clear();
            txtadminkadi.Clear();
            txtadminsoyad.Clear();
            txtadmintel.Clear();
            txtmail.Clear();
            txtsifre.Clear();
            txtsifretekrar.Clear();
            txtadmintc.Clear();
            txtmaas.Clear();
            cmdgorev.SelectedIndex = -1;
            cmdgorevyeri.SelectedIndex = -1;
            ccmdegitim.SelectedIndex = -1;
            cmdyetki.SelectedIndex = -1;
            radiobay.Checked = false;
            radiobayan.Checked = false;
            pctradminresimyukle.Image = Image.FromFile(Application.StartupPath + "\\resimler\\resimYok.png");
            btnkaydet.Enabled = false;
            btnsil.Enabled = true;
            btnguncelle.Enabled = true;


        }
        public void ara()
        {
            string kayit = ("select * from tblpersonel where tc=" + txtadmintc.Text);
            // müşteriler tablomuzun ilgili alanlarına kayıt ekleme işlemini gerçekleştirecek sorgumuz.
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                btnkaydet.Enabled = false;
                btnguncelle.Enabled = true;
                btnsil.Enabled = true;
                MessageBox.Show("Girdiğiniz TC ile eşleşen kayıt bulundu !", "Kişi bilgisi bulundu  !",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lblid.Text = dr.GetValue(0).ToString();
                txtadminad.Text = dr.GetValue(1).ToString();
                txtadminsoyad.Text = dr.GetValue(2).ToString();
                if (dr.GetValue(3).ToString() == "k") { radiobayan.Checked = true; }
                else { radiobay.Checked = true; }
                txtadmintel.Text = dr.GetValue(4).ToString();
                txtadmintc.Text = dr.GetValue(5).ToString();
                //txtadminad.Text = dr.GetValue(6).ToString();dtarih kısmı
                cmdgorev.SelectedItem = dr.GetValue(7).ToString();
                cmdgorevyeri.SelectedItem = dr.GetValue(8).ToString();
                string f = dr.GetValue(9).ToString();
                try
                {
                    pctradminresimyukle.Image = Image.FromFile(Application.StartupPath + "\\resimler\\" + f);
                }
                catch
                {
                    pctradminresimyukle.Image = Image.FromFile(Application.StartupPath + "\\resimler\\resimYok.png");
                }
                //txtadminad.Text = dr.GetValue(10).ToString();iş tarih  kısmı
                ccmdegitim.SelectedItem = dr.GetValue(11).ToString();
                txtmaas.Text = dr.GetValue(12).ToString();
                cmdyetki.SelectedItem = dr.GetValue(13).ToString();
                txtadminkadi.Text = dr.GetValue(14).ToString();
                txtsifre.Text = dr.GetValue(15).ToString();
                txtmail.Text = dr.GetValue(16).ToString();

                break;
            }
            baglanti.Close();
        }
        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void admin_Load(object sender, EventArgs e)
        {

            btnguncelle.Enabled = false;
            btnsil.Enabled = false;
            veriAl();
            this.Text = "Admin ekranı";//form yazısını değiştirdim
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;//burada sadece form yapısının değiştirilmesini engelledim
            //nesneler için sınırlamaları veriyorum
            lbladminad.ForeColor = Color.DarkRed;
            txtadmintc.MaxLength = 11;
            txtadmintel.MaxLength = 11;
            txtmaas.MaxLength = 6;
            txtsifre.MaxLength = 6;
            txtsifretekrar.MaxLength = 6;
            //dateye şekil verdim
            datedogumtarih.Format = DateTimePickerFormat.Short;
            dateistarih.Format = DateTimePickerFormat.Short;

            //yan taraftaki aktif kişi resmi burası
            pctradminresim.Height = 150;
            pctradminresim.Width = 150;
            pctradminresim.SizeMode = PictureBoxSizeMode.StretchImage;

            //admin kullanıcı resmi yükleme ekranı
            pctradminresimyukle.Height = 150;
            pctradminresimyukle.Width = 150;
            pctradminresimyukle.SizeMode = PictureBoxSizeMode.StretchImage;



            try
            {//buraya sonra veritabanından veri alarak yapacam 
             //  pctradminresim.Image = Image.FromFile(Application.StartupPath + "\\resimler\\resimYok.png");
             //  pctradminresim.Image = Image.FromFile(Application.StartupPath + "\\resimler\\"+foto);
                pctradminresim.Image = Image.FromFile(Application.StartupPath + "\\resimler\\resimYok.png");
                pctradminresimyukle.Image = Image.FromFile(Application.StartupPath + "\\resimler\\resimYok.png");

            }
            catch
            {
                pctradminresim.Image = Image.FromFile(Application.StartupPath + "\\resimler\\resimYok.png");
                pctradminresimyukle.Image = Image.FromFile(Application.StartupPath + "\\resimler\\resimYok.png");

            }
        }

        private void btncikis_Click(object sender, EventArgs e)
        {
            giris giris = new giris();
            giris.Show();
            this.Hide();
        }

        //tc, tel ve maas yerine sadece rakam girilmesini sağladım
        private void txtadmintc_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txtadmintel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txtmaas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
            btnkaydet.Enabled = true;
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {

            //şifre kontrolü
            string sifre = txtsifre.Text;
            string sifretekrar = txtsifretekrar.Text;
            if (sifre != sifretekrar)
            {
                MessageBox.Show("şifreler eşleşmedi lütfen tekrar kontrol edin !", "Şifre hatası !",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtsifre.Clear();
                txtsifretekrar.Clear();
            }

            //else if (txtadmintc.Text.Length <= 11)
            //{
            //    ara();
            //}
            else if (txtadminad.Text != "" && txtadminkadi.Text != "" && txtadminsoyad.Text != "" && txtadmintel.Text != "" && txtmail.Text != "" && txtadmintc.Text != ""
                && txtmaas.Text != "" && cmdgorev.SelectedItem.ToString() != "" && cmdgorevyeri.SelectedItem.ToString() != "" && cmdyetki.SelectedIndex.ToString() != ""
                && ccmdegitim.SelectedIndex.ToString() != "")
            {
                try
                {

                    string ad = txtadminad.Text;
                    string kadi = txtadminkadi.Text;
                    string soyad = txtadminsoyad.Text;
                    string tel = txtadmintel.Text;
                    string mail = txtmail.Text;
                    string tc = txtadmintc.Text;
                    string maas = txtmaas.Text;
                    string gorev = cmdgorev.SelectedItem.ToString();
                    string gorevYeri = cmdgorevyeri.SelectedItem.ToString();
                    string egitim = ccmdegitim.SelectedItem.ToString();
                    string yetki = cmdyetki.SelectedIndex.ToString();
                    string foto = " resimYok.png";
                    string dtarihi = "1990-01-01";
                    string istarih = "2020-01-01";
                    string cinsiyet;
                    if (radiobayan.Checked == true) { cinsiyet = "k"; }
                    else { cinsiyet = "e"; }





                    baglanti.Open();

                    string kayit = "INSERT INTO tblpersonel(ad,soyad,cinsiyet,tel,tc,dtarihi,gorev,gorevYeri,foto,istarih,egitim,maas,yetki,kadi,sifre,mail)values" +
                        " (@ad,@soyad,@cinsiyet,@tel,@tc,@dtarihi,@gorev,@gorevYeri,@foto,@istarih,@egitim,@maas,@yetki,@kadi,@sifre,@mail)";
                    // personel tablosunun ilgili alanlarına kayıt ekleme işlemini yapcak sorgu
                    SqlCommand komut = new SqlCommand(kayit, baglanti);

                    komut.Parameters.AddWithValue("@ad", ad);
                    komut.Parameters.AddWithValue("@soyad", soyad);
                    komut.Parameters.AddWithValue("@cinsiyet", cinsiyet);
                    komut.Parameters.AddWithValue("@tel", tel);
                    komut.Parameters.AddWithValue("@tc", tc);
                    komut.Parameters.AddWithValue("@dtarihi", dtarihi);
                    komut.Parameters.AddWithValue("@gorev", gorev);
                    komut.Parameters.AddWithValue("@gorevYeri", gorevYeri);
                    komut.Parameters.AddWithValue("@foto", foto);
                    komut.Parameters.AddWithValue("@istarih", istarih);
                    komut.Parameters.AddWithValue("@egitim", egitim);
                    komut.Parameters.AddWithValue("@maas", maas);
                    komut.Parameters.AddWithValue("@yetki", yetki);
                    komut.Parameters.AddWithValue("@kadi", kadi);
                    komut.Parameters.AddWithValue("@sifre", sifre);
                    komut.Parameters.AddWithValue("@mail", mail);
                    //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                    komut.ExecuteNonQuery();
                    //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                    baglanti.Close();
                    MessageBox.Show("Müşteri Kayıt İşlemi Gerçekleşti.", "Kayıt Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    temizle();
                    veriAl();
                }
                catch (Exception hata)
                {
                    MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
                }
            }
            else
            {
                temizle();
                MessageBox.Show("Lütfen tüm alanları doldurun !");
            }

        }

        private void btntcara_Click(object sender, EventArgs e)
        {

            ara();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {


            //şifre kontrolü
            string sifre = txtsifre.Text;
            string sifretekrar = txtsifretekrar.Text;
            if (sifre != sifretekrar)
            {
                MessageBox.Show("şifreler eşleşmedi lütfen tekrar kontrol edin !", "Şifre hatası !",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtsifre.Clear();
                txtsifretekrar.Clear();
            }

            //else if (txtadmintc.Text.Length <= 11)
            //{
            //    ara();
            //}
            else if (txtadminad.Text != "" && txtadminkadi.Text != "" && txtadminsoyad.Text != "" && txtadmintel.Text != "" && txtmail.Text != "" && txtadmintc.Text != ""
                && txtmaas.Text != "" && cmdgorev.SelectedItem.ToString() != "" && cmdgorevyeri.SelectedItem.ToString() != "" && cmdyetki.SelectedIndex.ToString() != ""
                && ccmdegitim.SelectedIndex.ToString() != "")
            {
                try
                {
                    string id = lblid.Text;
                    string ad = txtadminad.Text;
                    string kadi = txtadminkadi.Text;
                    string soyad = txtadminsoyad.Text;
                    string tel = txtadmintel.Text;
                    string mail = txtmail.Text;
                    string tc = txtadmintc.Text;
                    string maas = txtmaas.Text;
                    string gorev = cmdgorev.SelectedItem.ToString();
                    string gorevYeri = cmdgorevyeri.SelectedItem.ToString();
                    string egitim = ccmdegitim.SelectedItem.ToString();
                    string yetki = cmdyetki.SelectedIndex.ToString();
                    string foto = " resimYok.png";
                    string dtarihi = "1990-01-01";
                    string istarih = "2020-01-01";
                    string cinsiyet;
                    if (radiobayan.Checked == true) { cinsiyet = "k"; }
                    else { cinsiyet = "e"; }


                    baglanti.Open();

                    string kayit = "update tblpersonel set ad=@ad,soyad=@soyad,cinsiyet=@cinsiyet,tel=@tel,dtarihi=@dtarihi,gorev=@gorev,gorevYeri=@gorevYeri," +
                        "foto=@foto,istarih=@istarih,egitim=@egitim,maas=@maas,yetki=@yetki,kadi=@kadi,sifre=@sifre,mail=@mail where id=@id";
                    // personel tablosunun ilgili alanlarına kayıt ekleme işlemini yapcak sorgu
                    SqlCommand komut = new SqlCommand(kayit, baglanti);

                    komut.Parameters.AddWithValue("@ad", ad);
                    komut.Parameters.AddWithValue("@soyad", soyad);
                    komut.Parameters.AddWithValue("@cinsiyet", cinsiyet);
                    komut.Parameters.AddWithValue("@tel", tel);
                    komut.Parameters.AddWithValue("@tc", tc);
                    komut.Parameters.AddWithValue("@dtarihi", dtarihi);
                    komut.Parameters.AddWithValue("@gorev", gorev);
                    komut.Parameters.AddWithValue("@gorevYeri", gorevYeri);
                    komut.Parameters.AddWithValue("@foto", foto);
                    komut.Parameters.AddWithValue("@istarih", istarih);
                    komut.Parameters.AddWithValue("@egitim", egitim);
                    komut.Parameters.AddWithValue("@maas", maas);
                    komut.Parameters.AddWithValue("@yetki", yetki);
                    komut.Parameters.AddWithValue("@kadi", kadi);
                    komut.Parameters.AddWithValue("@sifre", sifre);
                    komut.Parameters.AddWithValue("@mail", mail);
                    komut.Parameters.AddWithValue("@id", id);
                    //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                    komut.ExecuteNonQuery();
                    //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                    baglanti.Close();
                    MessageBox.Show("Müşteri Kayıt İşlemi Gerçekleşti.", "Kayıt Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    temizle();
                    veriAl();
                }
                catch (Exception hata)
                {
                    MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
                }
            }
            else
            {
                temizle();
                MessageBox.Show("Lütfen tüm alanları doldurun !");
            }
        }

        private void tbladminveri_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            btnkaydet.Enabled = false;
            btnsil.Enabled = true;
            btnguncelle.Enabled = true;
            baglanti = new SqlConnection("server=.;Initial Catalog=dbPersonel;Integrated Security=SSPI");

            adaptor = new SqlDataAdapter("Select * From tblpersonel", baglanti);
            baglanti.Open();
            ds = new DataSet();
            tablo = new DataTable();
            adaptor.Fill(ds, "tablo");
            baglanti.Close();
            
            seçilensatır = Convert.ToInt32(ds.Tables["tablo"].Rows[e.RowIndex]["tc"]);

            lblid.Text = ds.Tables["tablo"].Rows[e.RowIndex]["id"].ToString();
            txtadminad.Text = ds.Tables["tablo"].Rows[e.RowIndex]["ad"].ToString();
            txtadminkadi.Text = ds.Tables["tablo"].Rows[e.RowIndex]["kadi"].ToString();
            txtadminsoyad.Text = ds.Tables["tablo"].Rows[e.RowIndex]["soyad"].ToString();
            txtadmintel.Text = ds.Tables["tablo"].Rows[e.RowIndex]["tel"].ToString();
            txtmaas.Text = ds.Tables["tablo"].Rows[e.RowIndex]["maas"].ToString();
            txtmail.Text = ds.Tables["tablo"].Rows[e.RowIndex]["mail"].ToString();
            txtsifre.Text = ds.Tables["tablo"].Rows[e.RowIndex]["sifre"].ToString();
            txtadmintc.Text = ds.Tables["tablo"].Rows[e.RowIndex]["tc"].ToString();
           cmdgorev.SelectedItem = ds.Tables["tablo"].Rows[e.RowIndex]["gorev"].ToString();
            cmdgorevyeri.SelectedItem = ds.Tables["tablo"].Rows[e.RowIndex]["gorevYeri"].ToString();
            cmdyetki.SelectedItem = ds.Tables["tablo"].Rows[e.RowIndex]["yetki"].ToString();
            ccmdegitim.SelectedItem = ds.Tables["tablo"].Rows[e.RowIndex]["egitim"].ToString();
           
           
        }
    }
}

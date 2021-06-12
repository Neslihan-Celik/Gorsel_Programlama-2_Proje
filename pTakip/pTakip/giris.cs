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
using System.IO;

namespace pTakip
{
    public partial class giris : Form
    {
        public giris()
        {
            InitializeComponent();
        }
        SqlConnection baglanti;//sql bağlantı adresi
        SqlDataAdapter adaptor;//sorgu ve bağlantı
        SqlCommand komut;//komut
        DataTable tablo;

       


        private void giris_Load(object sender, EventArgs e)
        {
            this.Text = "Kullanıcı giriş ekranı";//form yazısını değiştirdim
            this.AcceptButton = btngiris;//enter tuşuna basmak demek bu butona basmak demek dedim kısaca
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;//burada sadece form yapısının değiştirilmesini engelledim
        }

        private void btngiris_Click(object sender, EventArgs e)
        {
           

            try
            {
                baglanti = new SqlConnection("server=.;Initial Catalog=dbPersonel;Integrated Security=SSPI");
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select * from tblpersonel where kadi='" + txtkadi.Text + "' and sifre='" + txtsifre.Text + "'", baglanti);
                SqlDataReader data = komut.ExecuteReader();
                if (data.Read() == true)
                {
                    //veri tabanından aldığım verileri değişkenlere atadım diğer tablolarada atabilmek için
                    string id = data["id"].ToString();
                    string  yetki = data["yetki"].ToString();
                    string ad = data["ad"].ToString();
                    string soyad = data["soyad"].ToString();
                    string tc = data["tc"].ToString();
                    string tel = data["tel"].ToString();

                    string cinsiyet = data["cinsiyet"].ToString();//char olarak gelen bilgiyi burada düzelttim 
                    if (cinsiyet == "k")
                    {
                        cinsiyet = "Kadın";
                    }
                    else
                    {
                        cinsiyet = "Erkek";
                    }

                    string dtarihi = data["dtarihi"].ToString();
                    string gorev = data["gorev"].ToString();
                    string gorevYeri = data["gorevYeri"].ToString();
                    string foto = data["foto"].ToString();
                    string istarih = data["istarih"].ToString();
                    string egitim = data["egitim"].ToString();
                    string maas = data["maas"].ToString();
                    string kadi = data["kadi"].ToString();
                    string sifre = data["sifre"].ToString();
                    string mail = data["mail"].ToString();

                    // yetki kontrolü
                    if (yetki=="0") {
                        admin admin = new admin();
                        admin.Show();
                        this.Hide();
                        baglanti.Close();
                        admin.lbladminad.Text = (ad + " " + soyad);
                        admin.foto = foto;
                    }
                    else
                    {
                        personel personel = new personel();
                        personel.Show();
                        this.Hide();
                        baglanti.Close();
                        //buradan sonrası için formların modifiler özelliğini public yaptım ulaşıp değiştirmek için

                        personel.lblperadsoyad.Text = (ad + " " + soyad);//yan taraftaki aktif kullanıcıya ad soyad attım

                        //kullanıcı işlem sayfası verilerini attım
                        personel.lblkadi.Text = kadi;
                        personel.lblad.Text = ad;
                        personel.lblsoyad.Text = soyad;
                        personel.txtguncelsifre.Text = sifre;
                        //burası kullanıcı bilgi kısmı veri ataması
                        personel.lblbilgitc.Text = tc;
                        personel.lblbilgiad.Text = ad;
                        personel.lblbilgisoyad.Text = soyad;
                        personel.lblbilgicinsiyet.Text = cinsiyet;
                        personel.lblbilgiegitim.Text = egitim;
                        personel.lblbilgidtarih.Text = dtarihi;
                        personel.lblbilgigorev.Text = gorev;
                        personel.lblbilgigorevyeri.Text = gorevYeri;
                        personel.lblbilgimaas.Text = maas;
                        personel.lblbilgiistarih.Text = istarih;
                        personel.lblbilgimail.Text = mail;
                        personel.foto = foto;
                    }



                  
                }
                else
                {
                    MessageBox.Show("KULLANICI ADI YADA ŞİFRE HATALI");

                }
                baglanti.Close();
            }
            catch (Exception HATA)
            {
                MessageBox.Show("HATA = " + HATA.Message);
            }
        }
    }
}

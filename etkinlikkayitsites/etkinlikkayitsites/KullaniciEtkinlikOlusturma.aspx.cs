using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace etkinlikkayitsites
{
    public partial class KullaniciEtkinlikOlusturma : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                KategoriListesiniYukle();
            }


        }


        private void KategoriListesiniYukle()
        {
            string query = "SELECT ID, IlgiAlaniIsmi FROM IlgiAlani";
            using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                VeriTabaniBaglantisi.BaglantiKontrolu();
                SqlDataReader reader = cmd.ExecuteReader();

                ddlKategori.DataSource = reader;
                ddlKategori.DataTextField = "IlgiAlaniIsmi";
                ddlKategori.DataValueField = "ID"; 
                ddlKategori.DataBind();
                reader.Close();
            }
        }

        protected void btnEtkinlikOlustur_Click(object sender, EventArgs e)
        {
            string etkinlikAdi = txtEtkinlikAdi.Text.Trim();
            string aciklama = txtAciklama.Text.Trim();
            DateTime tarih = DateTime.Parse(txtTarih.Text);
            TimeSpan saat = TimeSpan.Parse(txtSaat.Text);
            int etkinlikSuresi = int.Parse(txtEtkinlikSuresi.Text);
            string konum = txtKonum.Text.Trim();
            int kategoriID = int.Parse(ddlKategori.SelectedValue);



            TimeSpan yeniEtkinlikBaslangic = saat;
            TimeSpan yeniEtkinlikBitis = saat.Add(TimeSpan.FromMinutes(etkinlikSuresi));

            // Çakışmayı kontrol et
            string conflictQuery = @"
        SELECT Saat, EtkinlikSuresi 
        FROM Etkinlikler 
        WHERE Tarih = @Tarih
        ORDER BY Saat ASC";

            using (SqlCommand cmd = new SqlCommand(conflictQuery, VeriTabaniBaglantisi.baglanti))
            {
                cmd.Parameters.AddWithValue("@Tarih", tarih);

                VeriTabaniBaglantisi.BaglantiKontrolu();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    bool cakismaVar = false;
                    List<TimeSpan> mevcutEtkinlikSaatleri = new List<TimeSpan>();
                    List<int> mevcutEtkinlikSureleri = new List<int>();

                    while (reader.Read())
                    {
                        mevcutEtkinlikSaatleri.Add(reader.GetTimeSpan(0));
                        mevcutEtkinlikSureleri.Add(reader.GetInt32(1));
                    }

                    for (int i = 0; i < mevcutEtkinlikSaatleri.Count; i++)
                    {
                        TimeSpan mevcutBaslangic = mevcutEtkinlikSaatleri[i];
                        TimeSpan mevcutBitis = mevcutBaslangic.Add(TimeSpan.FromMinutes(mevcutEtkinlikSureleri[i]));

                        if ((yeniEtkinlikBaslangic < mevcutBitis && yeniEtkinlikBitis > mevcutBaslangic) ||
                            (mevcutBaslangic < yeniEtkinlikBitis && mevcutBitis > yeniEtkinlikBaslangic))
                        {
                            cakismaVar = true;
                        }
                    }

                  
                    if (cakismaVar && hdnCakismaOnayi.Value != "1")
                    {
                        string mesaj = "Bu etkinlik başka bir etkinlikle çakışıyor. Yine de devam etmek istiyor musunuz?";
                        ScriptManager.RegisterStartupScript(this, GetType(), "confirm", $"ConfirmKaydet('{mesaj}');", true);
                        return;
                    }
                }
            }


            KaydetEtkinlik(etkinlikAdi, aciklama, tarih, saat, etkinlikSuresi, konum, kategoriID);
        }




        private void KaydetEtkinlik(string ad, string aciklama, DateTime tarih, TimeSpan saat, int sure, string konum, int kategori)
        {
           
            int onayliMi = -1;
            string kullaniciAdi = Session["KullaniciAdi"]?.ToString();
            int etkinlikSahibiID = GetKullaniciID(kullaniciAdi);

            // Etkinliği Veritabanına Kaydet
            string insertQuery = @"
       INSERT INTO Etkinlikler (EtkinlikAdi, Aciklama, Tarih, Saat, EtkinlikSuresi, Konum, Kategori, OnaylıMi, EtkinlikSahibi) 
      VALUES (@EtkinlikAdi, @Aciklama, @Tarih, @Saat, @EtkinlikSuresi, @Konum, @Kategori, @OnaylıMi, @EtkinlikSahibi)";

            using (SqlCommand cmd = new SqlCommand(insertQuery, VeriTabaniBaglantisi.baglanti))
            {
                cmd.Parameters.AddWithValue("@EtkinlikAdi", ad); 
                cmd.Parameters.AddWithValue("@Aciklama", aciklama);
                cmd.Parameters.AddWithValue("@Tarih", tarih);
                cmd.Parameters.AddWithValue("@Saat", saat);
                cmd.Parameters.AddWithValue("@EtkinlikSuresi", sure); 
                cmd.Parameters.AddWithValue("@Konum", konum);
                cmd.Parameters.AddWithValue("@Kategori", kategori);
                cmd.Parameters.AddWithValue("@OnaylıMi", onayliMi);
                cmd.Parameters.AddWithValue("@EtkinlikSahibi", etkinlikSahibiID);

                VeriTabaniBaglantisi.BaglantiKontrolu();
                cmd.ExecuteNonQuery();
            }

            // Etkinlik başarıyla kaydedildi mesajı
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Oluşturduğunuz etkinlik yönetici onayına sunuldu.');", true);
            EtkinlikBitisSaatiniHesapla hesapla = new EtkinlikBitisSaatiniHesapla();
            hesapla.HesaplaVeGuncelle();
        }

        private int GetKullaniciID(string kullaniciAdi)
        {
            int kullaniciID = 0;
            string query = "SELECT ID FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi";
            using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                cmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                VeriTabaniBaglantisi.BaglantiKontrolu();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    kullaniciID = Convert.ToInt32(result);
                }
            }
            return kullaniciID;
        }

        private TimeSpan OnerilenSaatBul(List<TimeSpan> mevcutSaatler, List<int> mevcutSureler, int yeniSure)
        {
            TimeSpan baslangicSaat = new TimeSpan(09, 0, 0); 
            TimeSpan bitisSaat = new TimeSpan(23, 59, 59); 
            for (int i = 0; i < mevcutSaatler.Count; i++)
            {
                TimeSpan mevcutBaslangic = mevcutSaatler[i];
                TimeSpan mevcutBitis = mevcutBaslangic.Add(TimeSpan.FromMinutes(mevcutSureler[i]));

                if (baslangicSaat.Add(TimeSpan.FromMinutes(yeniSure)) <= mevcutBaslangic)
                {
                    
                    return baslangicSaat;
                }

                baslangicSaat = mevcutBitis;
            }

          
            if (baslangicSaat.Add(TimeSpan.FromMinutes(yeniSure)) <= bitisSaat)
            {
                return baslangicSaat;
            }

          
            return new TimeSpan(0, 0, 0);
        }




    }
}
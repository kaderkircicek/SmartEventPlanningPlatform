using System;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Services;

namespace etkinlikkayitsites
{
    public partial class KullaniciOlusturduguEtkinlikleriGoruntuleme : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EtkinlikleriYukle();
            }
        }

        private void EtkinlikleriYukle()
        {
            VeriTabaniBaglantisi.BaglantiKontrolu();

            
            string kullaniciAdi = Session["KullaniciAdi"]?.ToString();
            int etkinlikSahibiID = GetKullaniciID(kullaniciAdi);

           
            string query = @"
        SELECT e.ID, e.EtkinlikAdi, e.Aciklama, e.Tarih, e.Saat, e.EtkinlikSuresi, e.Konum, 
               e.OnaylıMi, i.IlgiAlaniIsmi AS Kategori
        FROM Etkinlikler e
        LEFT JOIN IlgiAlani i ON e.Kategori = i.ID
        WHERE e.EtkinlikSahibi = @EtkinlikSahibiID";

            StringBuilder html = new StringBuilder();

            using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                cmd.Parameters.AddWithValue("@EtkinlikSahibiID", etkinlikSahibiID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int etkinlikId = (int)reader["ID"];
                    string tarih = Convert.ToDateTime(reader["Tarih"]).ToString("yyyy-MM-dd");
                    string saat = reader["Saat"].ToString();
                    string sure = reader["EtkinlikSuresi"].ToString();
                    string konum = reader["Konum"].ToString();
                    string aciklama = reader["Aciklama"].ToString();
                    string kategori = reader["Kategori"].ToString();
                    int onayDurumu = Convert.ToInt32(reader["OnaylıMi"]);
                    string onayDurumuText = onayDurumu == 1 ? "Onaylı" : "Onay bekleniyor";

                    html.Append($@"
            <div class='etkinlik-kutu-wrapper'>
                <div class='etkinlik-kutusu'>
                    <div class='etkinlik-baslik'>{reader["EtkinlikAdi"]}</div>
                    <div class='etkinlik-detay'>
                        <p><strong>Tarih:</strong> {tarih}</p>
                        <p><strong>Saat:</strong> {saat}</p>
                        <p><strong>Süre:</strong> {sure} dakika</p>
                        <p><strong>Konum:</strong> {konum}</p>
                        <p><strong>Açıklama:</strong> {aciklama}</p>
                        <p><strong>Kategori:</strong> {kategori}</p>
                        <p><strong>Onay Durumu:</strong> {onayDurumuText}</p>
                    </div>
                        <div class='etkinlik-butonlar'>
                        <button class='btn btn-danger' onclick='Sil({etkinlikId})'>Sil</button>
                       <a href='KullaniciEtkinlikGuncelle.aspx?ID={etkinlikId}' class='btn btn-primary'>Güncelle</a>
                        <a href='HaritadaGoruntuleme.aspx?etkinlikId={etkinlikId}' class='btn btn-info'>Haritada Görüntüle</a>
                    </div>
                </div>
            </div>");
                }
                reader.Close();
            }

            etkinliklerContainer.InnerHtml = html.ToString();
        }

        [WebMethod]
        public static bool SilEtkinlik(int etkinlikId)
        {
            try
            {
                VeriTabaniBaglantisi.BaglantiKontrolu();

                using (SqlCommand cmd = new SqlCommand("DELETE FROM Katilimcilar WHERE EtkinlikID = @etkinlikId", VeriTabaniBaglantisi.baglanti))
                {
                    cmd.Parameters.AddWithValue("@etkinlikId", etkinlikId);
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand("DELETE FROM Etkinlikler WHERE ID = @etkinlikId", VeriTabaniBaglantisi.baglanti))
                {
                    cmd.Parameters.AddWithValue("@etkinlikId", etkinlikId);
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
               
                return false; // Hata
            }
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
    }
}

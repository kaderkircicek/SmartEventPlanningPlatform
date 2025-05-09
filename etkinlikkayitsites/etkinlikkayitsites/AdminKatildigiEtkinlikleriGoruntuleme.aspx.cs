using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace etkinlikkayitsites
{
    public partial class AdminKatildigiEtkinlikleriGoruntuleme : System.Web.UI.Page
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
            string kullaniciAdi = Session["KullaniciAdi"]?.ToString();
            if (string.IsNullOrEmpty(kullaniciAdi))
            {
                Response.Redirect("Giris.aspx");
                return;
            }

            int kullaniciID = GetKullaniciID(kullaniciAdi);

            string query = @"
        SELECT E.ID, E.EtkinlikAdi, E.Tarih, E.Saat, E.EtkinlikSuresi, E.Konum, E.Aciklama, IA.IlgiAlaniIsmi AS Kategori
        FROM Etkinlikler E
        INNER JOIN Katilimcilar K ON E.ID = K.EtkinlikID
        INNER JOIN IlgiAlani IA ON E.Kategori = IA.ID
        WHERE K.KullaniciID = @KullaniciID";

            StringBuilder html = new StringBuilder();
            using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                VeriTabaniBaglantisi.BaglantiKontrolu();

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
                            <p><strong>Kategori:</strong> {kategori}</p> <!-- Kategori bilgisi eklendi -->
                        </div>
                        <div class='etkinlik-butonlar'>
                            <button class='btn btn-danger' onclick='KatilmaktanVazgec({etkinlikId})'>Katılmaktan Vazgeç</button>
                            <a href='SohbetiGoruntule.aspx?etkinlikId={etkinlikId}&kullaniciId={kullaniciID}' class='btn btn-info'>Sohbeti Görüntüle</a>
                            <a href='HaritadaGoruntuleme.aspx?etkinlikId={etkinlikId}' class='btn btn-info'>Haritada Görüntüle</a>
                        </div>
                    </div>
                </div>");
                }
                reader.Close();
            }

            etkinliklerContainer.InnerHtml = html.ToString();
        }

        private static int GetKullaniciID(string kullaniciAdi)
        {
            int kullaniciID = 0;
            string query = "SELECT ID FROM Adminler WHERE KullaniciAdi = @KullaniciAdi";

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

        [System.Web.Services.WebMethod]
        public static bool KatilmaktanVazgec(int etkinlikId)
        {
            try
            {
                string kullaniciAdi = HttpContext.Current.Session["KullaniciAdi"]?.ToString();
                if (string.IsNullOrEmpty(kullaniciAdi))
                {
                    return false;
                }

                int kullaniciID = GetKullaniciID(kullaniciAdi);
                string deleteQuery = "DELETE FROM Katilimcilar WHERE KullaniciID = @KullaniciID AND EtkinlikID = @EtkinlikID";

                using (SqlCommand cmd = new SqlCommand(deleteQuery, VeriTabaniBaglantisi.baglanti))
                {
                    cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                    cmd.Parameters.AddWithValue("@EtkinlikID", etkinlikId);
                    VeriTabaniBaglantisi.BaglantiKontrolu();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}

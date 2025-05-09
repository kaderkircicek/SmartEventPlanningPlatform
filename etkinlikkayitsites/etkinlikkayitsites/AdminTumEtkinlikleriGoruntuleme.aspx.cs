using System;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Services;

namespace etkinlikkayitsites
{
    public partial class AdminTumEtkinlikleriGoruntuleme : System.Web.UI.Page
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

            
            string query = @"
        SELECT 
            E.ID, E.EtkinlikAdi, E.Aciklama, E.Tarih, E.Saat, E.EtkinlikSuresi, E.Konum, E.OnaylıMi, 
            I.IlgiAlaniIsmi AS KategoriIsmi
        FROM 
            Etkinlikler AS E
        INNER JOIN 
            IlgiAlani AS I
        ON 
            E.Kategori = I.ID";

            StringBuilder html = new StringBuilder();

            using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int etkinlikId = (int)reader["ID"];
                    string tarih = Convert.ToDateTime(reader["Tarih"]).ToString("yyyy-MM-dd");
                    string saat = reader["Saat"].ToString();
                    string sure = reader["EtkinlikSuresi"].ToString();
                    string konum = reader["Konum"].ToString();
                    string aciklama = reader["Aciklama"].ToString();
                    int onayliMi = Convert.ToInt32(reader["OnaylıMi"]);
                    string onayDurumu = onayliMi == 1 ? "Onaylı" : "Onay Bekliyor";
                    string kategoriIsmi = reader["KategoriIsmi"].ToString(); 

                  
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
                <p><strong>Kategori:</strong> {kategoriIsmi}</p> <!-- Kategori bilgisi ekleniyor -->
                <p><strong>Durum:</strong> {onayDurumu}</p>
            </div>
            <div class='etkinlik-butonlar'>
                <button class='btn btn-danger' onclick='Sil({etkinlikId})'>Sil</button>
                <button class='btn btn-primary' onclick='Katila({etkinlikId})'>Katıl</button>
                <a href='EtkinlikGuncelle.aspx?ID={etkinlikId}' class='btn btn-primary'>Güncelle</a>
                <a href='AdminSohbetGoruntuleme.aspx?EtkinlikID={etkinlikId}' class='btn btn-primary'>Sohbet</a>
                <a href='HaritadaGoruntuleme.aspx?EtkinlikID={etkinlikId}' class='btn btn-primary'>Haritadaki Konumu</a>
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




        [System.Web.Services.WebMethod]
        public static string Katil(int etkinlikId)
        {    


            VeriTabaniBaglantisi.BaglantiKontrolu();

            int kullaniciId = -1;


            if (!EtkinlikOnayKontrolu(etkinlikId))
            {
                return "Onaylamadığınız etkinliğe katılamazsınız. Öncelikle etkinliği onaylayınız.";
            }


            if (EtkinlikZamanCakismaKontrolu(kullaniciId, etkinlikId)) { 
                string onerilenEtkinlik = OnerilenEtkinlikBul(etkinlikId);
            
                return onerilenEtkinlik != null
                ? $"Bu etkinliğe katılamazsınız. Bu tarihte aynı kategoriye ait etkinlik (Önerilen etkinlik): {onerilenEtkinlik}."
                : "Bu etkinliğe katılamazsınız. Bu tarihte aynı kategoriye ait başka bir etkinlik yok.";
            }
            KaydetKatilim(kullaniciId, etkinlikId);
            return "Etkinliğe başarıyla katıldınız.";
        }

        private static bool EtkinlikOnayKontrolu(int etkinlikId)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=EtkinlikYonetimSitesi;Integrated Security=True"))
            {
                string query = "SELECT OnaylıMi FROM Etkinlikler WHERE ID = @EtkinlikId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EtkinlikId", etkinlikId);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null && Convert.ToInt32(result) != -1; 
                }
            }
        }

        private static bool EtkinlikZamanCakismaKontrolu(int kullaniciId, int etkinlikId)
        {
            string conflictQuery = @"
        SELECT TOP 1 1
        FROM Katilimcilar K
        INNER JOIN Etkinlikler E ON K.EtkinlikID = E.ID
        WHERE K.KullaniciID = @KullaniciID
        AND E.Tarih = (SELECT Tarih FROM Etkinlikler WHERE ID = @EtkinlikID)
        AND (
          -- Kullanıcının katılmak istediği etkinlik, daha önce katıldığı bir etkinlik zaman aralığını kapsıyor mu?
          ((SELECT Saat FROM Etkinlikler WHERE ID = @EtkinlikID) <= E.Saat 
           AND DATEADD(MINUTE, (SELECT EtkinlikSuresi FROM Etkinlikler WHERE ID = @EtkinlikID), 
                       (SELECT Saat FROM Etkinlikler WHERE ID = @EtkinlikID)) 
               >= DATEADD(MINUTE, E.EtkinlikSuresi, E.Saat))
          OR
          -- Daha önce katıldığı bir etkinlik, katılmak istediği etkinlik zaman aralığını kapsıyor mu?
          (E.Saat <= (SELECT Saat FROM Etkinlikler WHERE ID = @EtkinlikID)
           AND DATEADD(MINUTE, E.EtkinlikSuresi, E.Saat) >= 
               DATEADD(MINUTE, (SELECT EtkinlikSuresi FROM Etkinlikler WHERE ID = @EtkinlikID), 
                       (SELECT Saat FROM Etkinlikler WHERE ID = @EtkinlikID)))
          OR
          -- Katılmak istediği etkinlik, daha önceki etkinliğin zaman aralığı ile kısmen çakışıyor mu?
          ((SELECT Saat FROM Etkinlikler WHERE ID = @EtkinlikID) BETWEEN E.Saat 
            AND DATEADD(MINUTE, E.EtkinlikSuresi, E.Saat))
          OR
          (DATEADD(MINUTE, (SELECT EtkinlikSuresi FROM Etkinlikler WHERE ID = @EtkinlikID), 
                   (SELECT Saat FROM Etkinlikler WHERE ID = @EtkinlikID)) 
            BETWEEN E.Saat AND DATEADD(MINUTE, E.EtkinlikSuresi, E.Saat))
            )";

            using (SqlCommand cmd = new SqlCommand(conflictQuery, VeriTabaniBaglantisi.baglanti))
            {
                cmd.Parameters.AddWithValue("@KullaniciID", kullaniciId);
                cmd.Parameters.AddWithValue("@EtkinlikID", etkinlikId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.HasRows; 
                }
            }
        }


        private static string OnerilenEtkinlikBul(int etkinlikId)
        {
            string query = @"
    SELECT TOP 1 EtkinlikAdi
    FROM Etkinlikler
    WHERE Kategori = (SELECT Kategori FROM Etkinlikler WHERE ID = @EtkinlikID)
      AND Tarih = (SELECT Tarih FROM Etkinlikler WHERE ID = @EtkinlikID)
      AND OnaylıMi = 1
      AND ID <> @EtkinlikID"; 

            using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                cmd.Parameters.AddWithValue("@EtkinlikID", etkinlikId);

                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : null;
            }
        }

        private static void KaydetKatilim(int kullaniciId, int etkinlikId)
        {
            string insertQuery = "INSERT INTO Katilimcilar (KullaniciID, EtkinlikID) VALUES (@KullaniciID, @EtkinlikID)";
            using (SqlCommand cmd = new SqlCommand(insertQuery, VeriTabaniBaglantisi.baglanti))
            {
                cmd.Parameters.AddWithValue("@KullaniciID", kullaniciId);
                cmd.Parameters.AddWithValue("@EtkinlikID", etkinlikId);
                cmd.ExecuteNonQuery();
            }
        }



    }
}
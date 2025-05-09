using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;

namespace etkinlikkayitsites
{
    public partial class HaritadaEtkinlikGoruntuleme : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    TumKonumlariYukle();
                    MesajGoster("Konumlar başarıyla yüklendi.", "success");
                }
                catch (Exception ex)
                {
                    MesajGoster("Konumlar yüklenirken bir hata oluştu: " + ex.Message, "error");
                }
            }
        }

        private void TumKonumlariYukle()
        {
            string baglantiCumlesi = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=EtkinlikYonetimSitesi;Integrated Security=True";
            string sorgu = "SELECT ID, Konum FROM Etkinlikler";
            var konumlar = new List<object>();

            using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
            {
                using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                {
                    baglanti.Open();
                    using (SqlDataReader reader = komut.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string konum = reader["Konum"].ToString();
                            int id = Convert.ToInt32(reader["ID"]);
                            if (!string.IsNullOrWhiteSpace(konum))
                            {
                                konumlar.Add(new { ID = id, Konum = konum });
                            }
                        }
                    }
                }
            }

            if (konumlar.Count == 0)
            {
                throw new Exception("Hiçbir etkinlik bulunamadı.");
            }

           
            gizliAlanKonumlar.Value = Newtonsoft.Json.JsonConvert.SerializeObject(konumlar);
        }

        private void MesajGoster(string mesaj, string tur)
        {
            
            string script = $"<script>mesajGoster('{mesaj}', '{tur}');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "MesajGoster", script);
        }
    }
}

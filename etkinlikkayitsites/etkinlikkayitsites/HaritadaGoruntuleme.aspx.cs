using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace etkinlikkayitsites
{
    public partial class HaritadaGoruntuleme : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string etkinlikID = Request.QueryString["EtkinlikID"];
                if (!string.IsNullOrEmpty(etkinlikID))
                {
                    EtkinlikKonumunuYukle(etkinlikID);
                }
                else
                {
                    HataGoster("Etkinlik ID geçerli değil.");
                }
            }
        }

        private void EtkinlikKonumunuYukle(string etkinlikID)
        {
            string baglantiCumlesi = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=EtkinlikYonetimSitesi;Integrated Security=True";
            string sorgu = "SELECT Konum FROM Etkinlikler WHERE ID = @EtkinlikID";

            using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
            {
                using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                {
                    komut.Parameters.AddWithValue("@EtkinlikID", etkinlikID);

                    try
                    {
                        baglanti.Open();
                        object sonuc = komut.ExecuteScalar();

                        if (sonuc != null)
                        {
                            string konum = sonuc.ToString();
                            ClientScript.RegisterStartupScript(GetType(), "AdresKodla",
                                $"adresiKodla('{konum}');", true);
                        }
                        else
                        {
                            HataGoster("Etkinlik bulunamadı.");
                        }
                    }
                    catch (Exception ex)
                    {
                        HataGoster("Bir hata oluştu: " + ex.Message);
                    }
                }
            }
        }

        private void HataGoster(string mesaj)
        {
            ltHataMesaji.Text = $"<div class='alert alert-danger'>{mesaj}</div>";
            ltHataMesaji.Visible = true;
        }
    }
}

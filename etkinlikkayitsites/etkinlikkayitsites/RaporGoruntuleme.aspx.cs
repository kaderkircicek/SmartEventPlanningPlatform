using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace etkinlikkayitsites
{
    public partial class RaporGoruntuleme : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EtkinlikRaporu();
                KatilimciRaporu();
            }
        }

        private void EtkinlikRaporu()
        {
            string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=EtkinlikYonetimSitesi;Integrated Security=True";
            string eventQuery = @"
                SELECT COUNT(e.ID) AS ToplamEtkinlikSayisi,
                       e.EtkinlikAdi, e.Tarih, e.Saat, e.EtkinlikBitis, e.Konum,
                       (SELECT COUNT(*) FROM Katilimcilar k WHERE k.EtkinlikID = e.ID) AS KatilimciSayisi
                FROM Etkinlikler e
                GROUP BY e.ID, e.EtkinlikAdi, e.Tarih, e.Saat, e.EtkinlikBitis, e.Konum;
            ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(eventQuery, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                gvEventReports.DataSource = reader;
                gvEventReports.DataBind();

                // Etkinlik sayısını almak için
                reader.Close();
                string countQuery = "SELECT COUNT(ID) FROM Etkinlikler;";
                SqlCommand countCmd = new SqlCommand(countQuery, conn);
                var totalEvents = countCmd.ExecuteScalar();
                lblTotalEvents.Text = totalEvents.ToString();
            }
        }

        private void KatilimciRaporu()
        {
            string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=EtkinlikYonetimSitesi;Integrated Security=True";
            string userQuery = @"
                SELECT u.KullaniciAdi, u.Eposta, u.Ad, u.Soyad,
                       (SELECT COUNT(*) FROM Etkinlikler e WHERE e.EtkinlikSahibi = u.ID) AS OlusturduguEtkinlikSayisi,
                       (SELECT COUNT(*) FROM Katilimcilar k WHERE k.KullaniciID = u.ID) AS KatildigiEtkinlikSayisi
                FROM Kullanicilar u;
            ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(userQuery, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                gvUserReports.DataSource = reader;
                gvUserReports.DataBind();

                
                reader.Close();
                string countQuery = "SELECT COUNT(ID) FROM Kullanicilar;";
                SqlCommand countCmd = new SqlCommand(countQuery, conn);
                var totalUsers = countCmd.ExecuteScalar();
                lblTotalUsers.Text = totalUsers.ToString();
            }
        }
    }
}

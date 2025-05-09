using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace etkinlikkayitsites
{
    public partial class EtkinlikDetay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int etkinlikId;
                    if (int.TryParse(Request.QueryString["ID"], out etkinlikId))
                    {
                        LoadEventDetails(etkinlikId);
                    }
                    else
                    {
                        ShowMessage("Geçersiz etkinlik ID'si. Lütfen bağlantıyı kontrol edin.");
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage("Bir hata oluştu: " + ex.Message);
                }
            }
        }

        private void LoadEventDetails(int etkinlikId)
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=EtkinlikYonetimSitesi;Integrated Security=True";

                string query = @"
            SELECT E.EtkinlikAdi, E.Aciklama, E.Tarih, E.Saat, E.Konum, E.EtkinlikBitis, E.OnaylıMi, IA.IlgiAlaniIsmi AS Kategori
            FROM Etkinlikler E
            INNER JOIN IlgiAlani IA ON E.Kategori = IA.ID
            WHERE E.ID = @ID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", etkinlikId);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                pnlEventDetails.Visible = true;
                                lblEtkinlikAdi.Text = reader["EtkinlikAdi"].ToString();
                                lblAciklama.Text = reader["Aciklama"].ToString();
                                lblTarih.Text = Convert.ToDateTime(reader["Tarih"]).ToString("dd/MM/yyyy");
                                lblSaat.Text = reader["Saat"].ToString();
                                lblBitisSaati.Text = reader["EtkinlikBitis"].ToString();
                                lblKategori.Text = reader["Kategori"].ToString(); 
                                lblKonum.Text = reader["Konum"].ToString();
                                lblOnayDurumu.Text = Convert.ToBoolean(reader["OnaylıMi"]) ? "Onaylı" : "Onay Bekliyor";
                            }
                            else
                            {
                                ShowMessage("Bu etkinlik bulunamadı. Lütfen farklı bir etkinlik seçin.");
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                ShowMessage("Veritabanı hatası: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                ShowMessage("Beklenmeyen bir hata oluştu: " + ex.Message);
            }
        }

        private void ShowMessage(string message)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;
            pnlEventDetails.Visible = false;
        }
    }
}

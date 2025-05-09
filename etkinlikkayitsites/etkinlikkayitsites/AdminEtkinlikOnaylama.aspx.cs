using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace etkinlikkayitsites
{
    public partial class AdminEtkinlikOnaylama : System.Web.UI.Page
    {
        private string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=EtkinlikYonetimSitesi;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OnaysizEtkinlikleriListele();
            }
            else
            {
              
                OnaysizEtkinlikleriListele();
            }
        }

        protected void ApproveButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int etkinlikId;
                if (int.TryParse(button.CommandArgument, out etkinlikId))
                {
                    ShowAlert($"Tıklanan etkinlik ID'si: {etkinlikId}");

                    string query = "UPDATE Etkinlikler SET OnaylıMi = 1 WHERE ID = @etkinlikId";
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@etkinlikId", etkinlikId);
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            ShowAlert("Etkinlik başarıyla onaylandı.");
                            OnaysizEtkinlikleriListele(); 
                        }
                        else
                        {
                            ShowAlert("Hata: Etkinlik onaylanamadı.");
                        }
                    }
                }
                else
                {
                    ShowAlert("Hata: Etkinlik ID alınamadı.");
                }
            }
        }

     
        private void OnaysizEtkinlikleriListele()
        {
            string query = @"
                SELECT 
                    e.ID,
                    e.EtkinlikAdi,
                    e.Aciklama,
                    e.Tarih,
                    e.Saat,
                    e.EtkinlikSuresi,
                    e.Konum,
                    k.IlgiAlaniIsmi
                FROM 
                    Etkinlikler e
                INNER JOIN 
                    IlgiAlani k ON e.Kategori = k.ID
                WHERE 
                    e.OnaylıMi = -1"; 

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                EventsContainer.Controls.Clear(); 

                if (!reader.HasRows)
                {
                    
                    EventsContainer.Controls.Add(new Literal { Text = "<p>Onay bekleyen etkinlik bulunmamaktadır.</p>" });
                    return;
                }

                while (reader.Read())
                {
                    Panel eventPanel = new Panel { CssClass = "event-container" };

                   
                    eventPanel.Controls.Add(new Literal { Text = $"<h4>{reader["EtkinlikAdi"]}</h4>" });
                    eventPanel.Controls.Add(new Literal { Text = $"<p><strong>Açıklama:</strong> {reader["Aciklama"]}</p>" });
                    eventPanel.Controls.Add(new Literal { Text = $"<p><strong>Tarih:</strong> {Convert.ToDateTime(reader["Tarih"]).ToShortDateString()}</p>" });
                    eventPanel.Controls.Add(new Literal { Text = $"<p><strong>Saat:</strong> {reader["Saat"]}</p>" });
                    eventPanel.Controls.Add(new Literal { Text = $"<p><strong>Etkinlik Süresi:</strong> {reader["EtkinlikSuresi"]} dakika</p>" });
                    eventPanel.Controls.Add(new Literal { Text = $"<p><strong>Konum:</strong> {reader["Konum"]}</p>" });
                    eventPanel.Controls.Add(new Literal { Text = $"<p><strong>Kategori:</strong> {reader["IlgiAlaniIsmi"]}</p>" });

                  
                    Button approveButton = new Button
                    {
                        Text = "Onayla",
                        CommandArgument = reader["ID"].ToString(),
                        CssClass = "approve-button"
                    };
                    approveButton.Click += ApproveButton_Click;

                    eventPanel.Controls.Add(approveButton);

                   
                    EventsContainer.Controls.Add(eventPanel);
                }
            }
        }


        private void ShowAlert(string message)
        {
            string script = $"alert('{message}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
        }
    }
}
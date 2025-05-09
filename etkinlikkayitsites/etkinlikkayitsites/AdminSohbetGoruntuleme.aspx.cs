using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace etkinlikkayitsites
{
    public partial class AdminSohbetGoruntuleme : System.Web.UI.Page
    {
        private string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=EtkinlikYonetimSitesi;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string etkinlikIDParam = Request.QueryString["EtkinlikID"];
                if (!string.IsNullOrEmpty(etkinlikIDParam) && int.TryParse(etkinlikIDParam, out int etkinlikID))
                {
                    LoadMessages(etkinlikID);
                }
                else
                {
                    Response.Redirect("AnaSayfa.aspx"); 
                }
            }
        }

        private void LoadMessages(int etkinlikID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
            SELECT 
                 m.MesajID, 
                 m.MesajMetni, 
                 m.GönderimZamani, 
                   CASE 
                  WHEN m.GöndericiID < 0 THEN 
                (SELECT KullaniciAdi FROM Adminler WHERE ID = m.GöndericiID)
            ELSE 
                (SELECT KullaniciAdi FROM Kullanicilar WHERE ID = m.GöndericiID)
                 END AS KullaniciAdi
                FROM Mesajlar m
                     WHERE m.EtkinlikID = @EtkinlikID
                     ORDER BY m.MesajID ASC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EtkinlikID", etkinlikID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptMesajlar.DataSource = dt;
                rptMesajlar.DataBind();
            }
        }

        protected void btnGonder_Click(object sender, EventArgs e)
        {
            int etkinlikID = Convert.ToInt32(Request.QueryString["EtkinlikID"]);
            string mesajMetni = txtMesaj.Text.Trim();

            if (!string.IsNullOrEmpty(mesajMetni))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "INSERT INTO Mesajlar (GöndericiID, MesajMetni, EtkinlikID, GönderimZamani) " +
                                   "VALUES (-1, @MesajMetni, @EtkinlikID, @GönderimZamani)";  

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MesajMetni", "Yönetici: " + mesajMetni);  
                    cmd.Parameters.AddWithValue("@EtkinlikID", etkinlikID);
                    cmd.Parameters.AddWithValue("@GönderimZamani", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }

                txtMesaj.Text = "";
                LoadMessages(etkinlikID);
            }
        }
    }
}

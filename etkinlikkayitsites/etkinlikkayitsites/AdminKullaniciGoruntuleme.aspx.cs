using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace etkinlikkayitsites
{
    public partial class AdminKullaniciGoruntuleme : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadKullanicilar();
            }
        }

       
        private void LoadKullanicilar()
        {
            string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=EtkinlikYonetimSitesi;Integrated Security=True"; 
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ID, KullaniciAdi, Eposta, Ad, Soyad, TelefonNumarası FROM Kullanicilar";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvKullanicilar.DataSource = dt;
                gvKullanicilar.DataBind();
            }
        }

      
        protected void btnGuncelle_Click(object sender, EventArgs e)
        {
            int kullaniciId = Convert.ToInt32(((Button)sender).CommandArgument);
            Response.Redirect($"AdminKullaniciGuncelleme.aspx?ID={kullaniciId}");
        }

     
        protected void btnSil_Click(object sender, EventArgs e)
        {
            int kullaniciId = Convert.ToInt32(((Button)sender).CommandArgument);

            
            string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=EtkinlikYonetimSitesi;Integrated Security=True"; 
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    DELETE FROM KullaniciIlgiAlanlari WHERE KullaniciID = @KullaniciID;
                    DELETE FROM Katilimcilar WHERE KullaniciID = @KullaniciID;
                    DELETE FROM Kullanicilar WHERE ID = @KullaniciID;";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@KullaniciID", kullaniciId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            
            Response.Redirect("AdminKullaniciGoruntuleme.aspx");
        }

        
        protected void gvKullanicilar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Sil")
            {
               
            }
        }
    }
}

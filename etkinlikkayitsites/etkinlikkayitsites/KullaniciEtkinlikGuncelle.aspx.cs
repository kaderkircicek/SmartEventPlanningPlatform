using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace etkinlikkayitsites
{
    public partial class KullaniciEtkinlikGuncelle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int etkinlikId;
                if (int.TryParse(Request.QueryString["ID"], out etkinlikId))
                {
                    EtkinlikBilgileriniGetir(etkinlikId);
                    LoadIlgiAlani();
                }
                else
                {
                    lblMessage.Text = "Geçersiz Etkinlik ID.";
                }
            }
        }


        private void EtkinlikBilgileriniGetir(int id)
        {
            string query = "SELECT EtkinlikAdi, Aciklama, Tarih, Saat, EtkinlikSuresi, Konum, Kategori FROM Etkinlikler WHERE ID = @ID";
            using (SqlConnection conn = new SqlConnection("Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=EtkinlikYonetimSitesi;Integrated Security=True"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtEtkinlikAdi.Text = reader["EtkinlikAdi"].ToString();
                        txtAciklama.Text = reader["Aciklama"].ToString();
                        txtTarih.Text = Convert.ToDateTime(reader["Tarih"]).ToString("yyyy-MM-dd");
                        txtSaat.Text = reader["Saat"].ToString();
                        txtSure.Text = reader["EtkinlikSuresi"].ToString();
                        txtKonum.Text = reader["Konum"].ToString();
                        ddlKategori.SelectedValue = reader["Kategori"].ToString();
                    }
                    reader.Close();
                }
            }
        }


        private void LoadIlgiAlani()
        {
            string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=EtkinlikYonetimSitesi;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ID, IlgiAlaniIsmi FROM IlgiAlani";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        ddlKategori.DataSource = reader;
                        ddlKategori.DataTextField = "IlgiAlaniIsmi"; 
                        ddlKategori.DataValueField = "ID"; 
                        ddlKategori.DataBind();

                        ddlKategori.Items.Insert(0, new ListItem("Seçiniz", "0")); 
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Bir hata oluştu: " + ex.Message;
                    }
                }
            }
        }


        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            int etkinlikId;
            if (int.TryParse(Request.QueryString["ID"], out etkinlikId))
            {
                EtkinlikGuncelleme(etkinlikId);
                Response.Redirect("KullaniciOlusturduguEtkinlikleriGoruntuleme.aspx");
            }
            else
            {
                lblMessage.Text = "Geçersiz Etkinlik ID.";
            }
        }

        private void EtkinlikGuncelleme(int id)
        {
            string query = @"
        UPDATE Etkinlikler
        SET EtkinlikAdi = @EtkinlikAdi,
            Aciklama = @Aciklama,
            Tarih = @Tarih,
            Saat = @Saat,
            EtkinlikSuresi = @EtkinlikSuresi,
            Konum = @Konum,
            Kategori = @Kategori
        WHERE ID = @ID";

            using (SqlConnection conn = new SqlConnection("Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=EtkinlikYonetimSitesi;Integrated Security=True"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EtkinlikAdi", txtEtkinlikAdi.Text);
                    cmd.Parameters.AddWithValue("@Aciklama", txtAciklama.Text);
                    cmd.Parameters.AddWithValue("@Tarih", txtTarih.Text);
                    cmd.Parameters.AddWithValue("@Saat", txtSaat.Text);
                    cmd.Parameters.AddWithValue("@EtkinlikSuresi", int.Parse(txtSure.Text));
                    cmd.Parameters.AddWithValue("@Konum", txtKonum.Text);
                    cmd.Parameters.AddWithValue("@Kategori", int.Parse(ddlKategori.SelectedValue)); 
                    cmd.Parameters.AddWithValue("@ID", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    lblMessage.Text = rowsAffected > 0 ? "Etkinlik başarıyla güncellendi." : "Güncelleme başarısız.";
                }
            }
        }
    }
}
    
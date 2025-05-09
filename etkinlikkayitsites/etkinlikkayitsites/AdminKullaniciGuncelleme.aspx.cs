using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace etkinlikkayitsites
{
    public partial class AdminKullaniciGuncelleme : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int kullaniciId;
                if (int.TryParse(Request.QueryString["ID"], out kullaniciId))
                {
                    // Kullanıcıyı veritabanından çek
                    LoadKullaniciData(kullaniciId);
                    IlgiAlanlariniYukle();
                    KullaniciIlgiAlanlariniYukle(kullaniciId);
                }
            }
        }

        private void IlgiAlanlariniYukle()
        {
            string query = "SELECT ID, IlgiAlaniIsmi FROM IlgiAlani";

            VeriTabaniBaglantisi.BaglantiKontrolu();

            using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    chkIlgiAlanlari.Items.Clear();

                    while (reader.Read())
                    {
                        string ilgiAlaniID = reader["ID"].ToString();
                        string ilgiAlaniIsmi = reader["IlgiAlaniIsmi"].ToString();
                        chkIlgiAlanlari.Items.Add(new ListItem(ilgiAlaniIsmi, ilgiAlaniID));
                    }
                }
            }
        }

        private void KullaniciIlgiAlanlariniYukle(int kullaniciId)
        {
            string query = "SELECT IlgiAlaniID FROM KullaniciIlgiAlanlari WHERE KullaniciID = @KullaniciID";

            VeriTabaniBaglantisi.BaglantiKontrolu();

            using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                cmd.Parameters.AddWithValue("@KullaniciID", kullaniciId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string ilgiAlaniID = reader["IlgiAlaniID"].ToString();
                        ListItem item = chkIlgiAlanlari.Items.FindByValue(ilgiAlaniID);
                        if (item != null)
                        {
                            item.Selected = true;
                        }
                    }
                }
            }
        }

        private void LoadKullaniciData(int kullaniciId)
        {
            string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=EtkinlikYonetimSitesi;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Kullanicilar WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", kullaniciId);

                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtKullaniciAdi.Text = reader["KullaniciAdi"].ToString();
                        txtSifre.Text = reader["Sifre"].ToString();
                        txtEposta.Text = reader["Eposta"].ToString();
                        txtAd.Text = reader["Ad"].ToString();
                        txtSoyad.Text = reader["Soyad"].ToString();
                        txtDogumTarihi.Text = Convert.ToDateTime(reader["DogumTarihi"]).ToString("yyyy-MM-dd");
                        ddlCinsiyet.SelectedValue = reader["Cinsiyet"].ToString();
                        txtKonum.Text = reader["Konum"].ToString();
                        txtTelefon.Text = reader["TelefonNumarası"].ToString();

                        string resimYolu = reader["Resim"].ToString();
                        imgResim.ImageUrl = string.IsNullOrEmpty(resimYolu) ? "~/Resimler/varsayilanResim.png" : "~/" + resimYolu;
                    }
                }
            }
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            int kullaniciId;
            if (int.TryParse(Request.QueryString["ID"], out kullaniciId))
            {
                string resimYolu = imgResim.ImageUrl;

                
                if (fileUploadResim.HasFile)
                {
                    resimYolu = "Resimler/" + fileUploadResim.FileName;
                    fileUploadResim.SaveAs(Server.MapPath("~/" + resimYolu));
                }

                
                string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=EtkinlikYonetimSitesi;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string updateQuery = "UPDATE Kullanicilar SET KullaniciAdi = @KullaniciAdi, Sifre = @Sifre, Eposta = @Eposta, Ad = @Ad, Soyad = @Soyad, Konum=@Konum, DogumTarihi = @DogumTarihi, Cinsiyet = @Cinsiyet, TelefonNumarası = @TelefonNumarası, Resim = @Resim WHERE ID = @ID";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@KullaniciAdi", txtKullaniciAdi.Text);
                        cmd.Parameters.AddWithValue("@Sifre", txtSifre.Text);
                        cmd.Parameters.AddWithValue("@Eposta", txtEposta.Text);
                        cmd.Parameters.AddWithValue("@Ad", txtAd.Text);
                        cmd.Parameters.AddWithValue("@Soyad", txtSoyad.Text);
                        cmd.Parameters.AddWithValue("@DogumTarihi", DateTime.Parse(txtDogumTarihi.Text));
                        cmd.Parameters.AddWithValue("@Cinsiyet", ddlCinsiyet.SelectedValue);
                        cmd.Parameters.AddWithValue("@Konum", txtKonum.Text);
                        cmd.Parameters.AddWithValue("@TelefonNumarası", txtTelefon.Text);
                        cmd.Parameters.AddWithValue("@Resim", resimYolu);
                        cmd.Parameters.AddWithValue("@ID", kullaniciId);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

               
                string deleteQuery = "DELETE FROM KullaniciIlgiAlanlari WHERE KullaniciID = @KullaniciID";

                VeriTabaniBaglantisi.BaglantiKontrolu();

                using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, VeriTabaniBaglantisi.baglanti))
                {
                    deleteCmd.Parameters.AddWithValue("@KullaniciID", kullaniciId);
                    deleteCmd.ExecuteNonQuery();
                }

               
                foreach (ListItem item in chkIlgiAlanlari.Items)
                {
                    if (item.Selected)
                    {
                        string insertQuery = "INSERT INTO KullaniciIlgiAlanlari (KullaniciID, IlgiAlaniID) VALUES (@KullaniciID, @IlgiAlaniID)";

                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, VeriTabaniBaglantisi.baglanti))
                        {
                            insertCmd.Parameters.AddWithValue("@KullaniciID", kullaniciId);
                            insertCmd.Parameters.AddWithValue("@IlgiAlaniID", item.Value);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Güncelleme İşlemi Başarıyla Yapıldı.');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Redirect", "setTimeout(function() { window.location.href = 'AdminKullaniciGoruntuleme.aspx'; }, 3000);", true);
            }
        }
    }
}

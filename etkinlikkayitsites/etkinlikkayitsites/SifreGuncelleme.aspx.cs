using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace etkinlikkayitsites
{
    public partial class SifreGuncelleme : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        protected void btnSifreGuncelleme_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = username.Text;
            string eposta = email.Text;
            string yeniSifre = newPassword.Text;

            VeriTabaniBaglantisi.BaglantiKontrolu();

            string query = "UPDATE Kullanicilar SET Sifre = @YeniSifre WHERE KullaniciAdi = @KullaniciAdi AND Eposta = @Eposta";

            using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                cmd.Parameters.AddWithValue("@YeniSifre", yeniSifre);
                cmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                cmd.Parameters.AddWithValue("@Eposta", eposta);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    string script = "alert('Şifre güncelleme başarılı. Sizi giriş sayfasına yönlendiriyorum.');" +
                 "setTimeout(function(){ window.location = 'GirisYap.aspx'; }, 2000);";  // 2 saniye sonra yönlendirme
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                }
                else
                {
                    // Güncelleme başarısız
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Şifre güncelleme başarısız. Lütfen bilgilerinizi kontrol edin.');", true);
                }
            }

            // Bağlantıyı kapatma (isteğe bağlı)
            VeriTabaniBaglantisi.baglanti.Close();
        }
    }
}

   
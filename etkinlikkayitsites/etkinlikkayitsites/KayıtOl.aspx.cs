using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace etkinlikkayitsites
{
    public partial class KayıtOl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
          
            string ad = firstName.Text.Trim();
            string soyad = lastName.Text.Trim();
            string email = this.email.Text;
            string kullaniciAdi = username.Text.Trim();
            string sifre = password.Text.Trim();
            string Konum = konum.Text.Trim();
            DateTime? dogumTarihi = null;
            string cinsiyet = gender.SelectedValue;
            string telefon = phoneNumber.Text.Trim();

            
            if (DateTime.TryParse(birthDate.Text.Trim(), out DateTime dt))
            {
                dogumTarihi = dt;
            }

           
            if (string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(soyad) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(sifre) || !dogumTarihi.HasValue || string.IsNullOrEmpty(cinsiyet) || string.IsNullOrEmpty(telefon) || !email.Contains("@"))
            {
                lblSuccessMessage.Text = "Lütfen tüm alanları doğru şekilde doldurduğunuzdan emin olun.";
                lblSuccessMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

         
            VeriTabaniBaglantisi.BaglantiKontrolu();

           
            string checkQuery = "SELECT COUNT(*) FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi OR Eposta = @Eposta";
            SqlCommand checkCmd = new SqlCommand(checkQuery, VeriTabaniBaglantisi.baglanti);
            checkCmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
            checkCmd.Parameters.AddWithValue("@Eposta", email);

            int userExists = (int)checkCmd.ExecuteScalar();
            if (userExists > 0)
            {
                lblSuccessMessage.Text = "Bu kullanıcı adı veya e-posta adresi zaten mevcut. Lütfen başka bir kullanıcı adı veya e-posta adresi seçin.";
                lblSuccessMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            
            string query = "INSERT INTO Kullanicilar (KullaniciAdi, Sifre,Konum, Eposta, Ad, Soyad, DogumTarihi, Cinsiyet, TelefonNumarası) " +
                           "VALUES (@KullaniciAdi, @Sifre, @Konum ,@Eposta, @Ad, @Soyad, @DogumTarihi, @Cinsiyet, @TelefonNumarası)";

            SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti);
            cmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
            cmd.Parameters.AddWithValue("@Sifre", sifre);
            cmd.Parameters.AddWithValue("@Konum", Konum);
            cmd.Parameters.AddWithValue("@Eposta", email);
            cmd.Parameters.AddWithValue("@Ad", ad);
            cmd.Parameters.AddWithValue("@Soyad", soyad);
            cmd.Parameters.AddWithValue("@DogumTarihi", dogumTarihi.Value);
            cmd.Parameters.AddWithValue("@Cinsiyet", cinsiyet);
            cmd.Parameters.AddWithValue("@TelefonNumarası", telefon);

            try
            {
                cmd.ExecuteNonQuery();
                lblSuccessMessage.Text = "Kayıt başarıyla tamamlandı!";
                lblSuccessMessage.ForeColor = System.Drawing.Color.Green;

               
                string script = "<script type='text/javascript'>setTimeout(function(){ window.location.href = 'GirisYap.aspx'; }, 2000);</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "redirect", script);
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "Kayıt yapılırken bir hata oluştu: " + ex.Message;
                lblSuccessMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void RedirectHome(object sender, EventArgs e)
        {
            Response.Redirect("AnaSayfa.aspx");
        }
    }
}

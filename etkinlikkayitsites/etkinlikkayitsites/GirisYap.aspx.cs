using System;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;

namespace etkinlikkayitsites
{
    public partial class GirisYap : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack && Request.Cookies["username"] != null)
            {
                username.Text = Request.Cookies["username"].Value;
                chkRememberMe.Checked = true;
                EtkinlikBitisSaatiniHesapla hesapla = new EtkinlikBitisSaatiniHesapla();
                hesapla.HesaplaVeGuncelle();
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = username.Text.Trim();
            string sifre = password.Text.Trim();

          
            VeriTabaniBaglantisi.BaglantiKontrolu();
            string query = "SELECT COUNT(*) FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi AND Sifre = @Sifre";

            using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                cmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                cmd.Parameters.AddWithValue("@Sifre", sifre);

                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    
                    if (chkRememberMe.Checked)
                    {  
                        HttpCookie cookie = new HttpCookie("username", kullaniciAdi);
                        cookie.Expires = DateTime.Now.AddDays(7);
                        Response.Cookies.Add(cookie);
                    }
                    else
                    {
                       
                        if (Request.Cookies["username"] != null)
                        {
                            HttpCookie cookie = new HttpCookie("username");
                            cookie.Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies.Add(cookie);
                        }
                    }

                   
                    Session["KullaniciAdi"] = kullaniciAdi;
                    Response.Redirect("KullaniciSayfasi.aspx");
                }
                else
                {
                   
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Hatalı kullanıcı adı veya şifre.');", true);
                }
            }
        }

        protected void btnSifreGuncelleme_Click(object sender, EventArgs e)
        {
            Response.Redirect("SifreGuncelleme.aspx");
        }
    }
}

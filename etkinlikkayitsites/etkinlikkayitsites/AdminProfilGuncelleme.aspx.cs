using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace etkinlikkayitsites
{
    public partial class AdminProfilGuncelleme : System.Web.UI.Page
    {
        private int KullaniciID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KullaniciAdi"] == null)
            {
                Response.Redirect("GirisYap.aspx");
            }

            KullaniciID = GetKullaniciID(Session["KullaniciAdi"].ToString());

            if (!IsPostBack)
            {
                KullaniciBilgileriniGetir();
                IlgiAlanlariniYukle();
                KullaniciIlgiAlanlariniYukle();
                ResimYukle();
                PuanDurumunuYukle();
                PuanTarihiniYukle();

            }
        }

        private void PuanDurumunuYukle()
        {
           
            string query = "SELECT Puan FROM Adminler WHERE ID = @KullaniciID";
            using (SqlConnection conn = new SqlConnection("Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=EtkinlikYonetimSitesi;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@KullaniciID", KullaniciID);
                conn.Open();
                var result = cmd.ExecuteScalar();
                txtPuanDurumu.Text = result?.ToString();
            }
        }


        private void PuanTarihiniYukle()
        {
           
            string query = "SELECT KazanılanTarih FROM Puanlar WHERE KullaniciID = @KullaniciID";

            using (SqlConnection conn = new SqlConnection("Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=EtkinlikYonetimSitesi;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@KullaniciID", KullaniciID);
                conn.Open();

                var result = cmd.ExecuteScalar();

                
                if (result != DBNull.Value && result != null)
                {
                    DateTime kazanilanTarih = Convert.ToDateTime(result);
                    txtPuanTarihi.Text = kazanilanTarih.ToString("yyyy-MM-dd"); 
                }
                else
                {
                    txtPuanTarihi.Text = string.Empty;
                }
            }
        }


        private void ResimYukle()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=EtkinlikYonetimSitesi;Integrated Security=True"))
                {
                    connection.Open();

                    string query = "SELECT Resim FROM Adminler WHERE ID = @KullaniciID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@KullaniciID", KullaniciID);

                    string resimYolu = command.ExecuteScalar() as string;
                    if (!string.IsNullOrEmpty(resimYolu))
                    {
                        ProfilImage.ImageUrl = resimYolu;
                    }
                    else
                    {
                        ProfilImage.ImageUrl = "~/Resimler/varsayilanResim.png"; 
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Resim yüklenirken bir hata oluştu: " + ex.Message;
            }
        }

        private int GetKullaniciID(string kullaniciAdi)
        {
            VeriTabaniBaglantisi.BaglantiKontrolu();
            string query = "SELECT ID FROM Adminler WHERE KullaniciAdi = @KullaniciAdi";
            using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                cmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                return (int)cmd.ExecuteScalar();
            }
        }

        private void KullaniciBilgileriniGetir()
        {
            VeriTabaniBaglantisi.BaglantiKontrolu();
            string query = "SELECT KullaniciAdi, Sifre,  Eposta, Ad, Soyad, DogumTarihi, Cinsiyet, Konum,TelefonNumarası, Resim FROM Adminler WHERE ID = @ID";
            using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                cmd.Parameters.AddWithValue("@ID", KullaniciID);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtKullaniciAdi.Text = reader["KullaniciAdi"].ToString();
                        txtSifre.Text = reader["Sifre"].ToString();
                        txtEposta.Text = reader["Eposta"].ToString();
                        txtAd.Text = reader["Ad"].ToString();
                        txtSoyad.Text = reader["Soyad"].ToString();
                        txtKonum.Text = reader["Konum"].ToString();
                        txtDogumTarihi.Text = Convert.ToDateTime(reader["DogumTarihi"]).ToString("yyyy-MM-dd");
                        ddlCinsiyet.SelectedValue = reader["Cinsiyet"].ToString();
                        txtTelefonNumarasi.Text = reader["TelefonNumarası"].ToString();

                        string resimYolu = reader["Resim"].ToString();
                        if (!string.IsNullOrEmpty(resimYolu))
                        {
                            ProfilImage.ImageUrl = resimYolu;
                        }
                        else
                        {
                            ProfilImage.ImageUrl = "~/Resimler/varsayilanResim.png"; 
                        }
                    }
                }
            }
        }

        private void IlgiAlanlariniYukle()
        {
            try
            {
                VeriTabaniBaglantisi.BaglantiKontrolu();

                
                string query = "SELECT ID, IlgiAlaniIsmi FROM IlgiAlani";
                using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        chkIlgiAlanlari.Items.Clear();
                        while (reader.Read())
                        {
                           
                            chkIlgiAlanlari.Items.Add(new ListItem(reader["IlgiAlaniIsmi"].ToString(), reader["ID"].ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "İlgi alanları yüklenirken bir hata oluştu: " + ex.Message;
            }
        }

        private void KullaniciIlgiAlanlariniYukle()
        {
            try
            {
                Debug.WriteLine("Metot başladı.");

               
                VeriTabaniBaglantisi.BaglantiKontrolu();
                if (VeriTabaniBaglantisi.baglanti.State != ConnectionState.Open)
                {
                    lblMessage.Text = "Veritabanı bağlantısı açık değil.";
                    Debug.WriteLine("Veritabanı bağlantısı açık değil.");
                    return;
                }
                Debug.WriteLine("Veritabanı bağlantısı başarılı.");

            

               
                string query = "SELECT IlgiAlaniID FROM AdminIlgiAlanlari WHERE KullaniciID = @KullaniciID";
                Debug.WriteLine("Query: " + query);

                using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
                {
                    cmd.Parameters.AddWithValue("@KullaniciID", KullaniciID);

                   
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Debug.WriteLine("Sorgu çalıştırıldı ve sonuçlar okunuyor...");
                        while (reader.Read())
                        {
                            
                            var ilgiAlaniID = reader["IlgiAlaniID"].ToString();
                            Debug.WriteLine("IlgiAlaniID: " + ilgiAlaniID);

                            
                            ListItem item = chkIlgiAlanlari.Items.FindByValue(ilgiAlaniID);
                            if (item != null)
                            {
                                item.Selected = true;
                                Debug.WriteLine($"Seçilen öğe: {item.Text} (Value: {item.Value})");
                            }
                            else
                            {
                                Debug.WriteLine($"Öğe bulunamadı: {ilgiAlaniID}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
                lblMessage.Text = "Kullanıcının ilgi alanları yüklenirken bir hata oluştu: " + ex.Message;
                Debug.WriteLine("Hata oluştu: " + ex.ToString());
            }
            finally
            {
                Debug.WriteLine("Metot tamamlandı.");
            }
        }


        protected void btnKaydet_Click(object sender, EventArgs e)
        {    
            if (string.IsNullOrWhiteSpace(txtSifre.Text))
            {
                lblMessage.Text = "Lütfen geçerli bir metin girin.";  
                return;  
            }
            try
            {
               
                VeriTabaniBaglantisi.BaglantiKontrolu();

                string resimYolu = null;

                
                if (fuResim.HasFile)
                {
                    string dosyaAdi = Path.GetFileName(fuResim.PostedFile.FileName);
                    resimYolu = "~/Resimler/" + dosyaAdi; 

                   
                    string fullPath = Server.MapPath(resimYolu);
                    if (!Directory.Exists(Path.GetDirectoryName(fullPath)))  
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)); 
                    }
                    fuResim.SaveAs(fullPath);  
                }
                else
                {
                   
                    resimYolu = ProfilImage.ImageUrl;
                }

                string query = "UPDATE Adminler SET Sifre = @Sifre, Eposta = @Eposta, Ad = @Ad, Soyad = @Soyad, DogumTarihi = @DogumTarihi, Cinsiyet = @Cinsiyet, Konum=@Konum ,TelefonNumarası = @TelefonNumarasi, Resim = @Resim WHERE ID = @ID";
                using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
                {
                    cmd.Parameters.AddWithValue("@ID", KullaniciID);
                    cmd.Parameters.AddWithValue("@Sifre", txtSifre.Text);
                    cmd.Parameters.AddWithValue("@Eposta", txtEposta.Text);
                    cmd.Parameters.AddWithValue("@Ad", txtAd.Text);
                    cmd.Parameters.AddWithValue("@Soyad", txtSoyad.Text);
                    cmd.Parameters.AddWithValue("@DogumTarihi", DateTime.Parse(txtDogumTarihi.Text));
                    cmd.Parameters.AddWithValue("@Cinsiyet", ddlCinsiyet.SelectedValue);
                    cmd.Parameters.AddWithValue("@TelefonNumarasi", txtTelefonNumarasi.Text);
                    cmd.Parameters.AddWithValue("@Konum", txtKonum.Text);
                    cmd.Parameters.AddWithValue("@Resim", resimYolu); 

                    
                    cmd.ExecuteNonQuery();
                }

                string deleteQuery = "DELETE FROM AdminIlgiAlanlari WHERE KullaniciID = @KullaniciID";
                using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, VeriTabaniBaglantisi.baglanti))
                {
                    deleteCmd.Parameters.AddWithValue("@KullaniciID", KullaniciID);
                    deleteCmd.ExecuteNonQuery();
                }

                
                foreach (ListItem item in chkIlgiAlanlari.Items)
                {
                    if (item.Selected)
                    {
                        string selectQuery = "SELECT ID FROM IlgiAlani WHERE IlgiAlaniIsmi = @IlgiAlaniIsmi";
                        using (SqlCommand selectCmd = new SqlCommand(selectQuery, VeriTabaniBaglantisi.baglanti))
                        {
                            selectCmd.Parameters.AddWithValue("@IlgiAlaniIsmi", item.Text);
                            object result = selectCmd.ExecuteScalar();

                            if (result != null)
                            {
                                int ilgiAlaniID = Convert.ToInt32(result);

                                string insertQuery = "INSERT INTO AdminIlgiAlanlari (KullaniciID, IlgiAlaniID) VALUES (@KullaniciID, @IlgiAlaniID)";
                                using (SqlCommand insertCmd = new SqlCommand(insertQuery, VeriTabaniBaglantisi.baglanti))
                                {
                                    insertCmd.Parameters.AddWithValue("@KullaniciID", KullaniciID);
                                    insertCmd.Parameters.AddWithValue("@IlgiAlaniID", ilgiAlaniID);
                                    insertCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }

               
                lblMessage.Text = "Profiliniz başarıyla güncellenmiştir.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
            "setTimeout(function(){ window.location = 'AdminSayfasi.aspx'; }, 4000);", true);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Bir hata oluştu: " + ex.Message;
            }
        }
    }
}
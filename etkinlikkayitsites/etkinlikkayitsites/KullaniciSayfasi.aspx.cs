using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace etkinlikkayitsites
{
    public partial class KullaniciSayfasi : System.Web.UI.Page
    {
        int etkinlikSahibiID;

        protected void Page_Load(object sender, EventArgs e)
        {
            string kullaniciAdi = Session["KullaniciAdi"]?.ToString();
            etkinlikSahibiID = GetKullaniciID(kullaniciAdi);
        }


        private int GetKullaniciID(string kullaniciAdi)
        {
            int kullaniciID = 0;
            string query = "SELECT ID FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi";
            using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                cmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                VeriTabaniBaglantisi.BaglantiKontrolu();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    kullaniciID = Convert.ToInt32(result);
                }
            }
            return kullaniciID;
        }

        protected void btnProfilim_Click(object sender, EventArgs e)
        {
            Puanlama puanlama = new Puanlama(etkinlikSahibiID);
            puanlama.PuanHesaplama();
            Response.Redirect("ProfilGuncelleme.aspx");
        }


        protected void btnCikisYap_Click(object sender, EventArgs e)
        {

            Response.Redirect("AnaSayfa.aspx");
        }

        protected void TumEtkinlikleriGoruntule_Click(object sender, EventArgs e)
        {
            Response.Redirect("KullaniciTumEtkinlikleriGoruntuleme.aspx");
        }

        protected void OnerilenEtkinlikleriGoruntule_Click(object sender, EventArgs e)
        {
            Response.Redirect("KullaniciEtkinlikOneri.aspx");
        }

        protected void EtkinlikOlustur_Click(object sender, EventArgs e)
        {
            Response.Redirect("KullaniciEtkinlikOlusturma.aspx");
        }


        protected void KendiEtkinlikleriniGoruntule_Click(object sender, EventArgs e)
        {
            Response.Redirect("KullaniciOlusturduguEtkinlikleriGoruntuleme.aspx");
        }


        protected void KatildiginEtkinlikleriGoruntule_Click(object sender, EventArgs e)
        {
            Response.Redirect("KullaniciKatildigiEtkinlikleriGoruntuleme.aspx");
        }

        protected void HaritaGoruntuleme_Click(object sender, EventArgs e)
        {
            Response.Redirect("KullaniciHaritadaEtkinlikGoruntuleme.aspx");
        }

    }
}
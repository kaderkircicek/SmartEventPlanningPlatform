using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace etkinlikkayitsites
{
    public partial class AdminSayfasi : System.Web.UI.Page
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
            string query = "SELECT ID FROM Adminler WHERE KullaniciAdi = @KullaniciAdi";
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
            Response.Redirect("AdminProfilGuncelleme.aspx");
        }

      

            protected void btnCikisYap_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("AnaSayfa.aspx");
        }

        protected void TumEtkinlikleriGoruntule_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminTumEtkinlikleriGoruntuleme.aspx");
        }

        protected void EtkinliklereOnayVer_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminEtkinlikOnaylama.aspx");
        }

        protected void EtkinlikOlustur_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminEtkinlikOlusturma.aspx");
        }

        protected void KullaniciGoruntule_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminKullaniciGoruntuleme.aspx");
        }


        protected void KatildiginEtkinlikleriGoruntule_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminKatildigiEtkinlikleriGoruntuleme.aspx");
        }


        protected void EtkinlikOnerileri_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminEtkinlikOneri.aspx");
        }


        protected void AdminRaporGoruntuleme_Click(object sender, EventArgs e)
        {
            Response.Redirect("RaporGoruntuleme.aspx");
        }

        protected void HaritaGoruntuleme_Click(object sender, EventArgs e)
        {
            Response.Redirect("HaritadaEtkinlikGoruntuleme.aspx");
        }

    }
}
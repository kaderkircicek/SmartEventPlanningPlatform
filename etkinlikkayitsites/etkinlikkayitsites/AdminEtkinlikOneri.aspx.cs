using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace etkinlikkayitsites
{
    public partial class AdminEtkinlikOneri : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string kullaniciAdi = Session["KullaniciAdi"]?.ToString();
                if (!string.IsNullOrEmpty(kullaniciAdi))
                {
                    int kullaniciID = GetKullaniciID(kullaniciAdi);
                    DataTable etkinlikler = GetOnerilenEtkinlikler(kullaniciID);

                    if (etkinlikler.Rows.Count > 0)
                    {
                        gvOneriler.DataSource = etkinlikler;
                        gvOneriler.DataBind();
                        lblNoData.Visible = false;
                    }
                    else
                    {
                        gvOneriler.Visible = false;
                        lblNoData.Text = "Size uygun bir etkinlik bulunamadı.";
                        lblNoData.Visible = true;
                    }
                }
                else
                {
                    lblNoData.Text = "Kullanıcı giriş yapmamış.";
                    lblNoData.Visible = true;
                }
            }
        }

        private static int GetKullaniciID(string kullaniciAdi)
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

        private static (double enlem, double boylam) GetKullaniciKonum(int kullaniciID)
        {
            string query = "SELECT Konum FROM Adminler WHERE ID = @KullaniciID";
            string konum = string.Empty;

            using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                VeriTabaniBaglantisi.BaglantiKontrolu();
                konum = cmd.ExecuteScalar()?.ToString();
            }

            return KonumCozumleyici.KonumKordinatlariniAl(konum);
        }

        private static DataTable GetOnerilenEtkinlikler(int kullaniciID)
        {
            DataTable etkinlikler = new DataTable();
            etkinlikler.Columns.Add("EtkinlikAdi", typeof(string));
            etkinlikler.Columns.Add("KategoriAdi", typeof(string));
            etkinlikler.Columns.Add("Konum", typeof(string));
            etkinlikler.Columns.Add("Mesafe", typeof(double));
            etkinlikler.Columns.Add("OnerilmeNedeni", typeof(string)); 

            // 1. İlgi Alanı Uyum Kuralı
            if (IlgiAlaniOlanEtkinlikler(kullaniciID, out DataTable ilgiAlanlariEtkinlikleri))
            {
                foreach (DataRow row in ilgiAlanlariEtkinlikleri.Rows)
                {
                    DataRow newRow = etkinlikler.NewRow();
                    newRow["EtkinlikAdi"] = row["EtkinlikAdi"];
                    newRow["KategoriAdi"] = row["KategoriAdi"];
                    newRow["Konum"] = row["Konum"];
                    newRow["OnerilmeNedeni"] = "İlgi Alanınıza Göre"; 
                    etkinlikler.Rows.Add(newRow);
                }
            }

            // 2. Katılım Geçmişi Kuralı
            if (KatilimGecmisiOlanEtkinlikler(kullaniciID, out DataTable katilimGecmisiEtkinlikleri))
            {
                foreach (DataRow row in katilimGecmisiEtkinlikleri.Rows)
                {
                    DataRow newRow = etkinlikler.NewRow();
                    newRow["EtkinlikAdi"] = row["EtkinlikAdi"];
                    newRow["KategoriAdi"] = row["KategoriAdi"];
                    newRow["Konum"] = row["Konum"];
                    newRow["OnerilmeNedeni"] = "Katılım Geçmişinize Göre"; 
                    etkinlikler.Rows.Add(newRow);
                }
            }

            // 3. Coğrafi Konum Kuralı
            var kullaniciKonum = GetKullaniciKonum(kullaniciID);
            foreach (DataRow row in etkinlikler.Rows)
            {
                var etkinlikKonum = KonumCozumleyici.KonumKordinatlariniAl(row["Konum"].ToString());
                row["Mesafe"] = KonumCozumleyici.MesafeyiHesapla(kullaniciKonum.enlem, kullaniciKonum.boylam, etkinlikKonum.enlem, etkinlikKonum.boylam);
            }

            etkinlikler.DefaultView.Sort = "Mesafe ASC";
            return etkinlikler.DefaultView.ToTable();
        }

        private static bool IlgiAlaniOlanEtkinlikler(int kullaniciID, out DataTable etkinlikler)
        {
            etkinlikler = new DataTable();
            etkinlikler.Columns.Add("EtkinlikAdi", typeof(string));
            etkinlikler.Columns.Add("KategoriAdi", typeof(string));
            etkinlikler.Columns.Add("Konum", typeof(string));
            etkinlikler.Columns.Add("OnerilmeNedeni", typeof(string)); 

            string query = @"SELECT E.EtkinlikAdi, IA.IlgiAlaniIsmi AS KategoriAdi, E.Konum 
                     FROM Etkinlikler E
                     INNER JOIN IlgiAlani IA ON E.Kategori = IA.ID
                     INNER JOIN AdminIlgiAlanlari AIA ON AIA.IlgiAlaniID = IA.ID
                     WHERE AIA.KullaniciID = @KullaniciID AND E.OnaylıMi = 1";

            using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                VeriTabaniBaglantisi.BaglantiKontrolu();
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(etkinlikler);
                }
            }

            return etkinlikler.Rows.Count > 0;
        }


        private static bool KatilimGecmisiOlanEtkinlikler(int kullaniciID, out DataTable etkinlikler)
        {
            etkinlikler = new DataTable();
            string query = @"SELECT TOP 5 E.EtkinlikAdi, IA.IlgiAlaniIsmi AS KategoriAdi, E.Konum 
                         FROM Katilimcilar K
                         INNER JOIN Etkinlikler E ON K.EtkinlikID = E.ID
                         INNER JOIN IlgiAlani IA ON E.Kategori = IA.ID
                         WHERE K.KullaniciID = @KullaniciID AND E.OnaylıMi = 1
                         GROUP BY E.EtkinlikAdi, IA.IlgiAlaniIsmi, E.Konum";

            using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                VeriTabaniBaglantisi.BaglantiKontrolu();
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(etkinlikler);
                }
            }
            return etkinlikler.Rows.Count > 0;
        }



        public class KonumCozumleyici
        {
            static readonly string apiAnahtari = "AIzaSyDL0UrNYNAON-a4yBZRiegXByzuHMu0A4g";

            public static (double enlem, double boylam) KonumKordinatlariniAl(string adres)
            {
                string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={adres}&key={apiAnahtari}";

                using (HttpClient istemci = new HttpClient())
                {
                    HttpResponseMessage yanit = istemci.GetAsync(url).Result;
                    string icerik = yanit.Content.ReadAsStringAsync().Result;

                    JObject json = JObject.Parse(icerik);
                    var sonuc = json["results"];

                    if (sonuc != null && sonuc.HasValues)
                    {
                        var location = sonuc[0]?["geometry"]?["location"];
                        if (location != null)
                        {
                            double enlem = (double)location["lat"];
                            double boylam = (double)location["lng"];
                            return (enlem, boylam);
                        }
                    }
                }
                return (0, 0);
            }

            public static double MesafeyiHesapla(double enlem1, double boylam1, double enlem2, double boylam2)
            {
                double R = 6371;
                double dEnlem = RadyanDonustur(enlem2 - enlem1);
                double dBoylam = RadyanDonustur(boylam2 - boylam1);

                double a = Math.Sin(dEnlem / 2) * Math.Sin(dEnlem / 2) +
                           Math.Cos(RadyanDonustur(enlem1)) * Math.Cos(RadyanDonustur(enlem2)) *
                           Math.Sin(dBoylam / 2) * Math.Sin(dBoylam / 2);
                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

                return R * c;
            }

            private static double RadyanDonustur(double derece)
            {
                return derece * (Math.PI / 180);
            }
        }
    }
}
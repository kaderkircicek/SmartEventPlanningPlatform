using System;
using System.Data.SqlClient;

namespace etkinlikkayitsites
{
    public class Puanlama
    {
        private int etkinlikSahibiID;
        private string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=EtkinlikYonetimSitesi;Integrated Security=True"; // SQL Server bağlantı dizesi

        public Puanlama(int id)
        {
            etkinlikSahibiID = id;
        }

        public void PuanHesaplama()
        {
            int toplamPuan = 0;
            int katilimPuan = 0;
            int olusturmaPuan = 0;
            int bonusPuan = 20;

           
            toplamPuan += bonusPuan;

            
            int katilimSayisi = GetKatilimSayisi(etkinlikSahibiID);
            katilimPuan = katilimSayisi * 10;
            toplamPuan += katilimPuan;

            
            int olusturduguEtkinlikSayisi = GetOlusturduguEtkinlikSayisi(etkinlikSahibiID);
            olusturmaPuan = olusturduguEtkinlikSayisi * 15; 
            toplamPuan += olusturmaPuan;

            if (etkinlikSahibiID > 0) 
            {
                UpdateKullaniciPuan(etkinlikSahibiID, toplamPuan);
            }
            else 
            {
                UpdateAdminPuan(etkinlikSahibiID, toplamPuan);
            }

            UpdateOrInsertPuan(etkinlikSahibiID, toplamPuan);
        }

        private int GetKatilimSayisi(int kullaniciID)
        {
            int katilimSayisi = 0;
            string query = "SELECT COUNT(*) FROM Katilimcilar WHERE KullaniciID = @KullaniciID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                    katilimSayisi = (int)cmd.ExecuteScalar();
                }
            }
            return katilimSayisi;
        }

        private int GetOlusturduguEtkinlikSayisi(int kullaniciID)
        {
            int etkinlikSayisi = 0;
            string query = "SELECT COUNT(*) FROM Etkinlikler WHERE EtkinlikSahibi = @EtkinlikSahibi";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EtkinlikSahibi", kullaniciID);
                    etkinlikSayisi = (int)cmd.ExecuteScalar();
                }
            }
            return etkinlikSayisi;
        }

        private void UpdateKullaniciPuan(int kullaniciID, int puan)
        {
            string query = "UPDATE Kullanicilar SET Puan = @Puan WHERE ID = @KullaniciID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Puan", puan);
                    cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateAdminPuan(int adminID, int puan)
        {
            string query = "UPDATE Adminler SET Puan = @Puan WHERE ID = @AdminID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Puan", puan);
                    cmd.Parameters.AddWithValue("@AdminID", adminID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateOrInsertPuan(int kullaniciID, int puan)
        {
            string queryCheck = "SELECT COUNT(*) FROM Puanlar WHERE KullaniciID = @KullaniciID";
            string queryInsert = "INSERT INTO Puanlar (KullaniciID, Puan, KazanılanTarih) VALUES (@KullaniciID, @Puan, @KazanılanTarih)";
            string queryUpdate = "UPDATE Puanlar SET Puan = @Puan, KazanılanTarih = @KazanılanTarih WHERE KullaniciID = @KullaniciID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                    int count = (int)cmdCheck.ExecuteScalar();

                    if (count == 0) 
                    {
                        using (SqlCommand cmdInsert = new SqlCommand(queryInsert, conn))
                        {
                            cmdInsert.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                            cmdInsert.Parameters.AddWithValue("@Puan", puan);
                            cmdInsert.Parameters.AddWithValue("@KazanılanTarih", DateTime.Now); 
                            cmdInsert.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn))
                        {
                            cmdUpdate.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                            cmdUpdate.Parameters.AddWithValue("@Puan", puan);
                            cmdUpdate.Parameters.AddWithValue("@KazanılanTarih", DateTime.Now); 
                            cmdUpdate.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

    }
}


using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace etkinlikkayitsites
{
    public class EtkinlikBitisSaatiniHesapla
    {
        private string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=EtkinlikYonetimSitesi;Integrated Security=True";

        public void HesaplaVeGuncelle()
        {
            string query = @"
            UPDATE Etkinlikler
            SET EtkinlikBitis = CAST(DATEADD(MINUTE, EtkinlikSuresi, CAST(Saat AS DATETIME)) AS TIME)
            WHERE EtkinlikBitis IS NULL;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        int affectedRows = cmd.ExecuteNonQuery();
                        Console.WriteLine($"{affectedRows} satır güncellendi.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
            }
        }
    }
}
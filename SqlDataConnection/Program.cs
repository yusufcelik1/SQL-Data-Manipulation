using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

public class SqlDataConnection
{
    private static SqlConnection connection;

    #region Veri Girişi
    static void Main()
    {
        
        Console.WriteLine("Yapmak istediğiniz işlemin sayısını yazınız");
        Console.WriteLine("1 - Veri Ekleme");
        Console.WriteLine("2 - Veri Güncelleme");
        Console.WriteLine("3 - Veri Silme");
        Console.WriteLine("4 - Veriyi Listeleme");
        int secim = Convert.ToInt32(Console.ReadLine());
        string connectionString = "Server=.; Database=VeriTabaniBaglanti; Trusted_Connection=SSPI; MultipleActiveResultSets=true; TrustServerCertificate=true;";
        connection = new SqlConnection(connectionString);
        connection.Open();
        switch (secim)
        {
            case 1:
                Console.Write("Adınızı giriniz: ");
                string ad = Console.ReadLine();
                Console.Write("Soyadınızı giriniz: ");
                string soyad = Console.ReadLine();
                Console.Write("Bolumunuzu giriniz: ");
                string bolum = Console.ReadLine();
                Console.Write("Cinsiyetinizi giriniz: ");
                string cinsiyet = Console.ReadLine();
                VeriEkle(ad, soyad, bolum, cinsiyet);
                break;
            case 2:
                Console.Write("Bilgilerini güncellemek istediğiniz öğrencinin okul numarasını giriniz: ");
                int onumarasi = Convert.ToInt32(Console.ReadLine());
                Console.Write("Adınızı giriniz: ");
                ad = Console.ReadLine();
                Console.Write("Soyadınızı giriniz: ");
                soyad = Console.ReadLine();
                Console.Write("Bolumunuzu giriniz: ");
                bolum = Console.ReadLine();
                Console.Write("Cinsiyetinizi giriniz: ");
                cinsiyet = Console.ReadLine();
                VeriGuncelle(onumarasi, ad, soyad, bolum, cinsiyet);
                break;
            case 3:
                Console.Write("Bilgilerini silmek istediğiniz öğrencinin okul numarasını giriniz: ");
                int numara = Convert.ToInt32(Console.ReadLine());
                VeriSil(numara);
                break;
            case 4:
                VeriListele();
                break;
        }
        connection.Close();
    }
    #endregion
    #region VeriEkle
    public static void VeriEkle(string ad, string soyad, string bolum, string cinsiyet)
    {
        string query = "INSERT INTO KutuphaneKayit (ad, soyad, bolum, cinsiyet) VALUES (@Ad, @Soyad, @Bolum, @Cinsiyet)";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Ad", ad);
            command.Parameters.AddWithValue("@Soyad", soyad);
            command.Parameters.AddWithValue("@Bolum", bolum);
            command.Parameters.AddWithValue("@Cinsiyet", cinsiyet);
            command.ExecuteNonQuery();
        }
        Console.WriteLine("Veri eklendi.");
    }
    #endregion
    #region VeriGüncelle
    public static void VeriGuncelle(int onumarasi, string ad, string soyad, string bolum, string cinsiyet)
    {
        string query = "UPDATE KutuphaneKayit SET ad = @Ad, soyad = @Soyad, bolum = @Bolum, cinsiyet = @Cinsiyet WHERE onumarasi = @ONumarasi";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Onumarasi", onumarasi);
            command.Parameters.AddWithValue("@Ad", ad);
            command.Parameters.AddWithValue("@Soyad", soyad);
            command.Parameters.AddWithValue("@Bolum", bolum);
            command.Parameters.AddWithValue("@Cinsiyet", cinsiyet);
            command.ExecuteNonQuery();
        }
        Console.WriteLine("Veri güncellendi.");
    }
    #endregion
    #region VeriSil
    public static void VeriSil(int onumarasi)
    {
        string query = "DELETE FROM KutuphaneKayit WHERE onumarasi = @ONumarasi";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@ONumarasi", onumarasi);
            command.ExecuteNonQuery();
        }

        Console.WriteLine("Veri silindi.");
    }
    #endregion
    #region VeriListele
    public static void VeriListele()
    {
        string query = "SELECT * FROM KutuphaneKayit";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int onumarasi = reader.GetInt32(0);
                    string ad = reader.GetString(1);
                    string soyad = reader.GetString(2);
                    string bolum = reader.GetString(3);
                    string cinsiyet = reader.GetString(4);
                    Console.WriteLine($"Öğrenci Numarasi: {onumarasi}\nAd:{ad}\nSoyad: {soyad}\nBolum: {bolum}\nCinsiyet :{cinsiyet}\n");
                }
                Console.ReadKey();
            }
        }
    }
    #endregion
}
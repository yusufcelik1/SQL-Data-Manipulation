using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

public class SqlDataConnection
{
    private static SqlConnection connection;

    static void Main()
    {
        // Establishing the connection to the database
        string connectionString = "Server=.; Database=VeriTabaniBaglanti; Trusted_Connection=SSPI; MultipleActiveResultSets=true; TrustServerCertificate=true;";
        connection = new SqlConnection(connectionString);
        connection.Open();

        // Displaying menu options for user input
        Console.WriteLine("Enter the number of the operation you want to perform");
        Console.WriteLine("1 - Add Data");
        Console.WriteLine("2 - Update Data");
        Console.WriteLine("3 - Delete Data");
        Console.WriteLine("4 - List Data");
        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1: // Adding Data
                Console.Write("Enter your first name: ");
                string firstName = Console.ReadLine();
                Console.Write("Enter your last name: ");
                string lastName = Console.ReadLine();
                Console.Write("Enter your department: ");
                string department = Console.ReadLine();
                Console.Write("Enter your gender: ");
                string gender = Console.ReadLine();
                VeriEkle(firstName, lastName, department, gender);
                break;

            case 2: // Updating Data
                Console.Write("Enter the student's school number to update: ");
                int schoolNumber = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter your first name: ");
                firstName = Console.ReadLine();
                Console.Write("Enter your last name: ");
                lastName = Console.ReadLine();
                Console.Write("Enter your department: ");
                department = Console.ReadLine();
                Console.Write("Enter your gender: ");
                gender = Console.ReadLine();
                VeriGuncelle(schoolNumber, firstName, lastName, department, gender);
                break;

            case 3: // Deleting Data
                Console.Write("Enter the school number of the student you want to delete: ");
                int studentNumber = Convert.ToInt32(Console.ReadLine());
                VeriSil(studentNumber);
                break;

            case 4: // Listing Data
                VeriListele();
                break;
        }

        // Closing the database connection
        connection.Close();
    }

    // Methods to perform CRUD operations

    // Add data to the database
    public static void VeriEkle(string firstName, string lastName, string department, string gender)
    {
        string query = "INSERT INTO KutuphaneKayit (ad, soyad, bolum, cinsiyet) VALUES (@Ad, @Soyad, @Bolum, @Cinsiyet)";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Ad", firstName);
            command.Parameters.AddWithValue("@Soyad", lastName);
            command.Parameters.AddWithValue("@Bolum", department);
            command.Parameters.AddWithValue("@Cinsiyet", gender);
            command.ExecuteNonQuery();
        }
        Console.WriteLine("Data added.");
    }

    // Update data in the database
    public static void VeriGuncelle(int schoolNumber, string firstName, string lastName, string department, string gender)
    {
        string query = "UPDATE KutuphaneKayit SET ad = @Ad, soyad = @Soyad, bolum = @Bolum, cinsiyet = @Cinsiyet WHERE onumarasi = @ONumarasi";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@ONumarasi", schoolNumber);
            command.Parameters.AddWithValue("@Ad", firstName);
            command.Parameters.AddWithValue("@Soyad", lastName);
            command.Parameters.AddWithValue("@Bolum", department);
            command.Parameters.AddWithValue("@Cinsiyet", gender);
            command.ExecuteNonQuery();
        }
        Console.WriteLine("Data updated.");
    }

    // Delete data from the database
    public static void VeriSil(int schoolNumber)
    {
        string query = "DELETE FROM KutuphaneKayit WHERE onumarasi = @ONumarasi";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@ONumarasi", schoolNumber);
            command.ExecuteNonQuery();
        }
        Console.WriteLine("Data deleted.");
    }

    // List data from the database
    public static void VeriListele()
    {
        string query = "SELECT * FROM KutuphaneKayit";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int schoolNumber = reader.GetInt32(0);
                    string firstName = reader.GetString(1);
                    string lastName = reader.GetString(2);
                    string department = reader.GetString(3);
                    string gender = reader.GetString(4);
                    Console.WriteLine($"School Number: {schoolNumber}\nFirst Name: {firstName}\nLast Name: {lastName}\nDepartment: {department}\nGender: {gender}\n");
                }
                Console.ReadKey();
            }
        }
    }
}

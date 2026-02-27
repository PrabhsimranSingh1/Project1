using MySql.Data.MySqlClient;

using System;
// Use ONE of these depending on what you installed:
// using MySql.Data.MySqlClient;


class Program
{
    static void Main()
    {
        // CHANGE Password= to your real root password
        var connectionString = "Server=localhost;Database=world;User=root;Password=prabhjohal04197@;Port=3306;";
        // connection string details are for local setup. 
        connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
        try
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Connected to MySQL ✅");

            var query = "SELECT ID, Name, CountryCode, District, Population FROM city LIMIT 5;";
            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            Console.WriteLine("ID | Name | CountryCode | District | Population");
            Console.WriteLine("------------------------------------------------");
            while (reader.Read())
            {
                Console.WriteLine(
                    $"{reader["ID"]} | {reader["Name"]} | {reader["CountryCode"]} | {reader["District"]} | {reader["Population"]}"
                );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed ❌");
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("Press ENTER to exit...");
        Console.ReadLine();
    }
}
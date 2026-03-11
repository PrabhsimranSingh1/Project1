using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

public static class Db
{
    public static string ConnectionString =
        "Server=localhost;Database=world;User=root;Password=prabhjohal04197@;Port=3306;";

    public static List<Dictionary<string, object>> Query(string sql, Dictionary<string, object>? parameters = null)
    {
        var rows = new List<Dictionary<string, object>>();

        using var conn = new MySqlConnection(ConnectionString);
        conn.Open();

        using var cmd = new MySqlCommand(sql, conn);

        if (parameters != null)
        {
            foreach (var p in parameters)
                cmd.Parameters.AddWithValue(p.Key, p.Value);
        }

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            var row = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < reader.FieldCount; i++)
                row[reader.GetName(i)] = reader.GetValue(i);

            rows.Add(row);
        }

        return rows;
    }
}




public static class Print
{
    public static void Table(List<Dictionary<string, object>> rows, params string[] columns)
    {
        if (rows.Count == 0)
        {
            Console.WriteLine("No results.");
            return;
        }

        // Column widths
        var widths = new Dictionary<string, int>();
        foreach (var c in columns)
        {
            int max = c.Length;
            foreach (var r in rows)
            {
                var v = r.ContainsKey(c) ? r[c]?.ToString() ?? "" : "";
                if (v.Length > max) max = v.Length;
            }
            widths[c] = Math.Min(max, 40); // cap to avoid huge columns
        }

        // Header
        foreach (var c in columns)
            Console.Write(c.PadRight(widths[c] + 2));
        Console.WriteLine();

        foreach (var c in columns)
            Console.Write(new string('-', widths[c]) + "  ");
        Console.WriteLine();

        // Rows
        foreach (var r in rows)
        {
            foreach (var c in columns)
            {
                var v = r.ContainsKey(c) ? r[c]?.ToString() ?? "" : "";
                if (v.Length > 40) v = v[..37] + "...";
                Console.Write(v.PadRight(widths[c] + 2));
            }
            Console.WriteLine();
        }
    }
}
public static partial class Reports
{
    public static void CitiesByPopulation()
    {
        string sql = @"
            SELECT Name AS City, CountryCode, District, Population
            FROM city
            ORDER BY Population DESC;";
 
        var rows = Db.Query(sql);
        Print.Table(rows, "City", "CountryCode", "District", "Population");
    }
}


class Program
{
    static void Main()
    {
        
        var connectionString = "Server=localhost;Database=world;User=root;Password=prabhjohal04197@;Port=3306;";

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

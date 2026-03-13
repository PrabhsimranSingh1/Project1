
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
public static class Reports
{
    public static void CountriesByPopulation()
    {
        string sql = @"
            SELECT Name AS Country, Population
            FROM country
            ORDER BY Population DESC;";

        var rows = Db.Query(sql);
        Print.Table(rows, "Country", "Population");
    }
    
    public static void CitiesByPopulation()
    {
        string sql = @"
            SELECT Name AS City, CountryCode, District, Population
            FROM city
            ORDER BY Population DESC;";

        var rows = Db.Query(sql);
        Print.Table(rows, "City", "CountryCode", "District", "Population");
    }
    
    public static void TopNCitiesWorld(int n)
    {
        string sql = @"
        SELECT Name AS City, CountryCode, District, Population
        FROM city
        ORDER BY Population DESC
        LIMIT @n;";

        var rows = Db.Query(sql, new() { ["@n"] = n });
        Print.Table(rows, "City", "CountryCode", "District", "Population");
    }

    public static void PopulationByContinent()
    {
        string sql = @"
            SELECT Continent, SUM(Population) AS TotalPopulation
            FROM country
            GROUP BY Continent
            ORDER BY TotalPopulation DESC;";

        var rows = Db.Query(sql);
        Print.Table(rows, "Continent", "TotalPopulation");
    }
    public static void PopulationByRegion()
    {
            string sql = @"
            SELECT Region, SUM(Population) AS TotalPopulation
            FROM country
            GROUP BY Region
            ORDER BY TotalPopulation DESC;";

        var rows = Db.Query(sql);
        Print.Table(rows, "Region", "TotalPopulation");
    }
    public static void CityVsNonCityPopulation(string countryCode)
        {
            string sql = @"
            SELECT 
                co.Name AS Country,
                co.Population AS TotalPopulation,
                IFNULL(SUM(ci.Population), 0) AS CityPopulation,
                (co.Population - IFNULL(SUM(ci.Population), 0)) AS NonCityPopulation
            FROM country co
            LEFT JOIN city ci ON ci.CountryCode = co.Code
            WHERE co.Code = @code
            GROUP BY co.Code, co.Name, co.Population;";

            var rows = Db.Query(sql, new() { ["@code"] = countryCode });
            Print.Table(rows, "Country", "TotalPopulation", "CityPopulation", "NonCityPopulation");
        }
    
    public static void LanguageStatistics()
    {
            
                string sql = @"
            SELECT 
                cl.Language,
                ROUND(SUM(co.Population * (cl.Percentage / 100))) AS EstimatedSpeakers,
                ROUND((SUM(co.Population * (cl.Percentage / 100)) / (SELECT SUM(Population) FROM country)) * 100, 2) AS WorldPercent
            FROM countrylanguage cl
            JOIN country co ON co.Code = cl.CountryCode
            WHERE cl.Language IN ('Chinese','English','Hindi','Spanish','Arabic')
            GROUP BY cl.Language
            ORDER BY EstimatedSpeakers DESC;";

                var rows = Db.Query(sql);
                Print.Table(rows, "Language", "EstimatedSpeakers", "WorldPercent");
            
    }

    public static void TopNCountriesWorld(int n)
    {
        string sql = @"
            SELECT Name AS Country, Population
            FROM country
            ORDER BY Population DESC
            LIMIT @n;";
 
        var rows = Db.Query(sql, new() { ["@n"] = n });
        Print.Table(rows, "Country", "Population");
    }
}
 
 
    public static void CapitalsByPopulation()
    {
        string sql = @"
            SELECT ci.Name AS CapitalCity, co.Name AS Country, ci.Population
            FROM country co
            JOIN city ci ON co.Capital = ci.ID
            ORDER BY ci.Population DESC;";
 
        var rows = Db.Query(sql);
        Print.Table(rows, "CapitalCity", "Country", "Population");
    }
}



class Program
{
    static void Main()
    {
        while (true)
        
        var connectionString = "Server=localhost;Database=world;User=root;Password=prabhjohal04197@;Port=3306;";
        // connection string details are for local setup. 
        connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
        try
        {
            Console.WriteLine("\n--- Population Reporting System ---");
            Console.WriteLine("1) Countries by population");
            Console.WriteLine("2) Cities by population");
            Console.WriteLine("3) Capitals by population");
            Console.WriteLine("4) Top N countries (world)");
            Console.WriteLine("5) Top N cities (world)");
            Console.WriteLine("6) Population by continent");
            Console.WriteLine("7) Population by region");
            Console.WriteLine("8) City vs non-city population (by country code)");
            Console.WriteLine("9) Language statistics (5 languages)");
            Console.WriteLine("0) Exit");
            Console.Write("Select: ");

            var choice = Console.ReadLine()?.Trim();

            try
            {
                switch (choice)
                {
                    case "1": Reports.CountriesByPopulation(); break;
                    case "2": Reports.CitiesByPopulation(); break;
                    case "3": Reports.CapitalsByPopulation(); break;
                    case "4":
                        Console.Write("Enter N: ");
                        int n1 = int.Parse(Console.ReadLine()!);
                        Reports.TopNCountriesWorld(n1);
                        break;
                    case "5":
                        Console.Write("Enter N: ");
                        int n = int.Parse(Console.ReadLine()!);
                        Reports.TopNCitiesWorld(n);
                        break;
                    case "6": Reports.PopulationByContinent(); break;
                    case "7": Reports.PopulationByRegion(); break;
                    case "8":
                        Console.Write("Enter country code (e.g., GBR): ");
                        string code = Console.ReadLine()!.Trim().ToUpper();
                        Reports.CityVsNonCityPopulation(code);
                        break;
                    case "9": Reports.LanguageStatistics(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}



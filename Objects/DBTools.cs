using System;
using System.IO;
using System.Data.SqlClient;

namespace BeerRecommendation.Objects
{
  public class DBTools
  {
    public static void Main()
    {
      Console.WriteLine("Check 1");
      string path = "Database/oregonBeers.txt";

      try
      {
        SqlConnection conn = DB.Connection();
        conn.Open();
        Console.WriteLine("Check 2");
        using (StreamReader rdr = new StreamReader(path))
        {
          Console.WriteLine("Check 3");

          string line;
          while ((line = rdr.ReadLine()) != null)
          {
            string[] beerEntry = line.Split(';');
            string breweryName = beerEntry[0];
            string name = beerEntry[1];
            double abv = Convert.ToDouble(beerEntry[2]);
            double ibu = Convert.ToDouble(beerEntry[3]);
            string breweryLocation = beerEntry[4];

            SqlCommand beerCmd = new SqlCommand("INSERT INTO beers (name, abv, ibu) VALUES (@Name, @Abv, @Ibu);", conn);
            beerCmd.Parameters.AddWithValue("@Name", name);
            beerCmd.Parameters.AddWithValue("@Abv", abv);
            beerCmd.Parameters.AddWithValue("@Ibu", ibu);
            beerCmd.ExecuteNonQuery();

            SqlCommand breweryCmd = new SqlCommand("INSERT INTO breweries (name, location) VALUES (@BreweryName, @Location);", conn);
            breweryCmd.Parameters.AddWithValue("@BreweryName", breweryName);
            breweryCmd.Parameters.AddWithValue("@Location", breweryLocation);
            breweryCmd.ExecuteNonQuery();
          }

        }
        if (conn != null) conn.Close();
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
    }
  }
}

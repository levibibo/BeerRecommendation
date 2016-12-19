using System.IO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BeerRecommendation
{
    public class DBTools
    {
        public static void Main()
        {
            string path = @"..\Database\beer database plain text.txt";
            try
            {
                using (StreamReader rdr = new StreamReader(path))
                {
                    string line;
                    while ((line = rdr.ReadLine()) != null)
                    {
                        List<string> beerList = line.Split('\t').ToList();
                        string name = line[1];
                        float abv = (float) line[2];
                        float ibu = (float) line[3];
                        string location = line[5];

                        SqlConnection conn = DB.Connection();
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO beers SET beer_name = @name, abv = @abv, ibu = @ibu, location = @location;", conn);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@abv", abv);
                        cmd.Parameters.AddWithValue("@ibu", ibu);
                        cmd.Parameters.AddWithValue("@location", location);
                        cmd.ExecuteNonQuery();
                        if (conn != null) conn.Close();
                    }
                }
            }
            catch
            {
                Console.WriteLine("invalid text file.");
            }

        }
    }

    //public class Startup
    //{
    //    public void Configure(IApplicationBuilder app)
    //    {
    //        app.UseOwin(x => x.UseNancy());
    //    }
    //}
    
    //public class CustomRootPathProvider : IRootPathProvider
    //{
    //    public string GetRootPath()
    //    {
    //        return Directory.GetCurrentDirectory();
    //    }
    //}
    
    public class DB
    {
        public static string ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=beer_recommendation;Integrated Security=SSPI";

        public static SqlConnection Connection()
        {
            SqlConnection conn = new SqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }
    }
}
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BeerRecommendation.Objects
{
	public class Beer
	{
		private int _id;
		private string _name;
		private double _abv;
		private double _ibu;

		public Beer(string name, double abv, double ibu, int id = 0)
		{
			_name = name;
			_abv = Math.Round(abv, 2);
			_ibu = Math.Round(ibu, 2);
			_id = id;
		}

		//Static methods
		public static List<Beer> GetAll()
		{
			List<Beer> allBeers = new List<Beer>{};

			SqlConnection conn = DB.Connection();
			conn.Open();

			SqlCommand cmd = new SqlCommand("SELECT * FROM beers ORDER BY name ASC;", conn);
			SqlDataReader rdr = cmd.ExecuteReader();
			while(rdr.Read())
			{
				int id = rdr.GetInt32(0);
				string name = rdr.GetString(1);
				double abv = rdr.GetDouble(2);
				double ibu = rdr.GetDouble(3);

				Beer newBeer = new Beer(name, abv, ibu, id);
				allBeers.Add(newBeer);
			}
			if (rdr != null) rdr.Close();
			if (conn != null) conn.Close();
			return allBeers;
		}

		public static void DeleteAll()
		{
			SqlConnection conn = DB.Connection();
			conn.Open();
			SqlCommand cmd = new SqlCommand("DELETE FROM beers;", conn);
			cmd.ExecuteNonQuery();
			if (conn != null) conn.Close();
		}

		//Other mentods
		public void Save()
		{
			SqlConnection conn = DB.Connection();
			conn.Open();

			SqlCommand cmd = new SqlCommand("INSERT INTO beers (name, abv, ibu) OUTPUT INSERTED.id VALUES (@Name, @Abv, @Ibu);", conn);

			cmd.Parameters.AddWithValue("@Name", _name);
			cmd.Parameters.AddWithValue("@Abv", _abv);
			cmd.Parameters.AddWithValue("@Ibu", _ibu);

			SqlDataReader rdr = cmd.ExecuteReader();

			while(rdr.Read())
			{
				_id = rdr.GetInt32(0);
			}
			if (rdr != null)
			{
				rdr.Close();
			}
			if (conn != null)
			{
				conn.Close();
			}
		}

		//Overrides


		//Getters & Setters

	}
}

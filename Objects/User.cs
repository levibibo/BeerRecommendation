using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BeerRecommendation.Objects
{
	public class User
	{
		private int _id;
		private string _name;

		public User(string name, int id = 0)
		{
			_name = name;
			_id = id;
		}

		//Static methods
		public static List<User> GetAll()
		{
			List<User> allUsers = new List<User>{};

			SqlConnection conn = DB.Connection();
			conn.Open();

			SqlCommand cmd = new SqlCommand("SELECT * FROM users ORDER BY name ASC;", conn);
			SqlDataReader rdr = cmd.ExecuteReader();
			while(rdr.Read())
			{
				int id = rdr.GetInt32(0);
				string name = rdr.GetString(1);

				User newUser = new User(name, id);
				allUsers.Add(newUser);
			}
			if (rdr != null) rdr.Close();
			if (conn != null) conn.Close();
			return allUsers;
		}

		public static User Find(int id)
		{
			SqlConnection conn = DB.Connection();
			conn.Open();
			SqlCommand cmd = new SqlCommand("SELECT name FROM users WHERE id = @Id;", conn);
			cmd.Parameters.AddWithValue("@Id", id);
			SqlDataReader rdr = cmd.ExecuteReader();
			string name = null;
			while (rdr.Read())
			{
				name = rdr.GetString(0);
			}
			if (rdr != null) rdr.Close();
			return new User(name, id);
		}

		public static void DeleteUser(int id)
		{
			SqlConnection conn = DB.Connection();
			conn.Open();
			SqlCommand cmd = new SqlCommand("DELETE FROM users WHERE id = @Id;", conn);
			cmd.Parameters.AddWithValue("@Id", id);
			cmd.ExecuteNonQuery();
			if (conn != null) conn.Close();
		}

		public static void DeleteAll()
		{
			SqlConnection conn = DB.Connection();
			conn.Open();
			SqlCommand cmd = new SqlCommand("DELETE FROM users;", conn);
			cmd.ExecuteNonQuery();
			if (conn != null) conn.Close();
		}

		//Other methods
		public void Save()
		{
			SqlConnection conn = DB.Connection();
			conn.Open();

			SqlCommand cmd = new SqlCommand("INSERT INTO users (name) OUTPUT INSERTED.id VALUES (@Name);", conn);

			cmd.Parameters.AddWithValue("@Name", _name);

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

		public void Update(string name)
		{
			SqlConnection conn = DB.Connection();
			conn.Open();
			SqlCommand cmd = new SqlCommand("UPDATE users SET name = @Name WHERE id = @Id;", conn);
			cmd.Parameters.AddWithValue("@Name", name);
			cmd.Parameters.AddWithValue("@Id", _id);
			cmd.ExecuteNonQuery();
			if (conn != null) conn.Close();
			_name = name;
		}

		//Overrides
		public override bool Equals(Object otherUser)
		{
			if (!(otherUser is User))
			{
				return false;
			}
			else
			{
				User newUser = (User) otherUser;
				bool idEquality = (_id == newUser.GetId());
				bool nameEquality = (_name == newUser.GetName());
				return (idEquality && nameEquality);
			}
		}

		public override int GetHashCode()
		{
			return _name.GetHashCode();
		}

		//Getters & Setters
		public int GetId()
		{
			return _id;
		}
		public string GetName()
		{
			return _name;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BeerRecommendation.Objects
{
  public class Brewery
  {
    private int _id;
    private string _name;
    private string _location;

    public Brewery(string newName, string newLocation, int newId = 0)
    {
      _id = newId;
      _name = newName;
      _location = newLocation;
    }

    public override bool Equals(System.Object otherObject)
    {
      if (!(otherObject is Brewery))
      {
        return false;
      }
      else
      {
        Brewery testBrewery = (Brewery) otherObject;
        bool idEquality = this.GetId() == testBrewery.GetId();
        bool nameEquality = this.GetName() == testBrewery.GetName();
        bool locationEquality = this.GetLocation() == testBrewery.GetLocation();
        return (idEquality && nameEquality && locationEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM breweries;", conn);
      cmd.ExecuteNonQuery();

      if (conn != null) conn.Close();
    }







////Getters and Setters
    public void SetId(int newId)
    {
      _id = newId;
    }
    public int GetId()
    {
      return _id;
    }

    public void SetName(string newName)
    {
      _name = newName;
    }
    public string GetName()
    {
      return _name;
    }

    public void SetLocation(string newLocation)
    {
      _location = newLocation;
    }
    public string GetLocation()
    {
      return _location;
    }
  }
}

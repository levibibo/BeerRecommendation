using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BeerRecommendation.Objects
{
  public class BreweryTest : IDisposable
  {
    public BreweryTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=beer_recommendation_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Equals_ObjectsAreEqual_True()
    {
      //Arrange, Act
      Brewery testBrewery1 = new Brewery("Widmer", "Portland, OR");
      Brewery testBrewery2 = new Brewery("Widmer", "Portland, OR");
      //Assert
      Assert.Equal(testBrewery1, testBrewery2);
    }


    public void Dispose()
    {
      Brewery.DeleteAll();
    }
  }
}

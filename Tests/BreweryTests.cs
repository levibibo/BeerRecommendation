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

    public void Dispose()
    {
      Brewery.DeleteAll();
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

    [Fact]
    public void Save_ObjectSavedToDatabase()
    {
      //Arrange
      var testList = new List<Brewery>{};
      Brewery newBrewery = new Brewery("Widmer", "Portland, OR");

      //Act
      newBrewery.Save();
      testList.Add(newBrewery);
      var savedBreweries = Brewery.GetAll();
      //Assert
      Assert.Equal(testList, savedBreweries);
    }

    [Fact]
    public void GetAll_GetsAllObjectsFromTable()
    {
      //Arrange
      Brewery testBrewery1 = new Brewery("Widmer", "Portland, OR");
      Brewery testBrewery2 = new Brewery("Breakside", "Portland, OR");
      testBrewery1.Save();
      testBrewery2.Save();
      List<Brewery> expectedList = new List<Brewery> { testBrewery1, testBrewery2 };
      //Act
      List<Brewery> results = Brewery.GetAll();
      //Assert
      Assert.Equal(expectedList, results);
    }

    [Fact]
    public void Find_RetrievesTargetBrewery()
    {
      //Arrange
      Brewery testBrewery = new Brewery("Breakside", "Portland, OR");
      testBrewery.Save();
      //Act
      Brewery foundBrewery = Brewery.Find(testBrewery.GetId());
      //Assert
      Assert.Equal(testBrewery, foundBrewery);
    }
  }
}

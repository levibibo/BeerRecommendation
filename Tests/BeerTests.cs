using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BeerRecommendation.Objects
{
  public class BeerTest : IDisposable
  {
    public BeerTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=beer_recommendation;Integrated Security=SSPI;";
    }

    [Fact]
    public void Save_AddSingleBeerToDB_1()
    {
      //Arrange
      Beer newBeer = new Beer("Alpha IPA", 6.8, 70.0);

      //Act
      newBeer.Save();
      List<Beer> allBeers = Beer.GetAll();

      //Assert
      Assert.Equal(1, allBeers.Count);
    }

    public void Dispose()
    {
      Beer.DeleteAll();
    }
  }
}

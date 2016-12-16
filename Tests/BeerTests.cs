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

    public void Dispose()
    {
      Beer.DeleteAll();
    }
  }
}

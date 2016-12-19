using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BeerRecommendation.Objects
{
  public class UserTest : IDisposable
  {
    public UserTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=beer_recommendation_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Save_AddSingleUserToDB_1()
    {
      //Arrange
      User newUser = new User("Bob");

      //Act
      newUser.Save();
      List<User> allUsers = User.GetAll();

      //Assert
      Assert.Equal(1, allUsers.Count);
    }

    [Fact]
    public void Find_GetSingleUserFromDB_EquivalentUser()
    {
      //Arrange
      User user1 = new User("Bob");
      User user2 = new User("Fred");
      user1.Save();
      user2.Save();

      //Act
      User testUser = User.Find(user1.GetId());

      //Assert
      Assert.Equal(user1, testUser);
    }

    [Fact]
    public void GetAll_ReturnAListOfAllUsers_ListOfUsers()
    {
      //Arrange
      User user1 = new User("Bob");
      User user2 = new User("Fred");
      user1.Save();
      user2.Save();

      //Act
      List<User> testUsers = User.GetAll();
      List<User> expectedUsers = new List<User> {user1, user2};

      //Assert
      Assert.Equal(expectedUsers, testUsers);
    }

    [Fact]
    public void Update_UpdateUserName_EquivalentUser()
    {
      //Arrange
      User expectedUser = new User("Bob");

      //Act
      expectedUser.Save();
      expectedUser.Update("Robert");
      User foundUser = User.Find(expectedUser.GetId());

      //Assert
      Assert.Equal(expectedUser, foundUser);
    }

    public void Dispose()
    {
      User.DeleteAll();
    }
  }
}

using Nancy;
using Nancy.Cookies;
using System.Collections.Generic;
using BeerRecommendation.Objects;

namespace BeerRecommendation
{
	public class HomeModule : NancyModule
	{
		public HomeModule()
		{
			//Homepage
			Get["/"] = _ =>
			{
				return View["index.cshtml"];
			};

			//Login
			Get["/login"] = _ =>
			{
				return View["login.cshtml"];
			};

			Post["/login"] = _ =>
			{
				string userName = Request.Form["user-name"];
				int userId = User.CheckUserName(userName);
				if (userId != 0)
				{
					NancyCookie idNumber = new NancyCookie("userId", userId.ToString());
					return View["index.cshtml"].WithCookie(idNumber);
				}
				else
				{
					return View["new_user.cshtml"];
				}
			};
			//Test page
			Get["/test"] = _ =>
			{
				List<Beer> allBeers = Beer.GetAll();
				List<User> allUsers = User.GetAll();
				Dictionary<string, object> Model = new Dictionary<string, object>();
				Model["beers"] = allBeers;
				Model["users"] = allUsers;
				return View["algorithm_test_form.cshtml", Model];
			};
			Post["/test/result"] = _ =>
			{
				User guest = new User("Guest");
				int beerId = int.Parse(Request.Form["beer-id"]);
				List<Beer> recommendedBeers = guest.GetRecommendations(beerId);
				return View["algorithm_test_result.cshtml", recommendedBeers];
			};

			//User page
			Get["/users/profile"] = _ =>
			{
				int userId = int.Parse(Request.Cookies["userId"]);
				User foundUser = User.Find(userId);
				return View["user.cshtml", foundUser];
			};
			Get["/users/new"] = _ =>
			{
				return View["new_user.cshtml", false];
			};
			Post["/users/new/success"] = _ =>
			{
				string name = Request.Form["name"];
				if (!(User.UserExists(name)))
				{
					User newUser = new User(name);
					newUser.Save();
					return View["new_user_success.cshtml", newUser];
				}
				else
				{
					return View["new_user.cshtml", true];
				}
			};

			//Beer page
			Get["/beers"] = _ =>
			{
				List<Beer> allBeers = Beer.GetAll();
				return View["beers.cshtml", allBeers];
			};
			Get["/beers/new"] = _ =>
			{
				List<Brewery> allBreweries = Brewery.GetAll();
				return View["new_beer.cshtml", allBreweries];
			};
			Post["/beers/new/success"] = _ =>
			{
				string name = Request.Form["name"];
				double abv = (double) Request.Form["abv"];
				double ibu = (double) Request.Form["ibu"];
				Beer newBeer = new Beer(name, abv, ibu);
				newBeer.Save();
				return View["new_beer_success.cshtml", newBeer];
			};
			Get["/beers/{id}"] = parameters =>
			{
				Beer foundBeer = Beer.Find(parameters.id);
				return View["beer.cshtml", foundBeer];
			};

			//Brewery page
			Get["/breweries"] = _ =>
			{
				List<Brewery> allBreweries = Brewery.GetAll();
				return View["breweries.cshtml", allBreweries];
			};
			Get["/breweries/new"] = _ =>
			{
				return View["new_brewery.cshtml"];
			};
			Post["/breweries/new/success"] = _ =>
			{
				string name = Request.Form["name"];
				string location = Request.Form["location"];
				Brewery newBrewery = new Brewery(name, location);
				newBrewery.Save();
				return View["new_brewery_success.cshtml", newBrewery];
			};
			Get["/brewery/{id}"] = parameters =>
			{
				Brewery foundBrewery = Brewery.Find(parameters.id);
				return View["brewery.cshtml", foundBrewery];
			};
		}
	}
}

using System;
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

			//database
			Get["/database"] = _ =>
			{
				return View["database_tools.cshtml"];
			};
			Get["/database/beers/new"] = _ =>
			{
				List<Brewery> allBreweries = Brewery.GetAll();
				return View["new_beer.cshtml", allBreweries];
			};
			Post["/database/beers/new/success"] = _ =>
			{
				string name = Request.Form["name"];
				double abv = (double) Request.Form["abv"];
				double ibu = (double) Request.Form["ibu"];
				Beer newBeer = new Beer(name, abv, ibu);
				newBeer.Save();
				return View["new_beer_success.cshtml", newBeer];
			};
			Get["/database/breweries/new"] = _ =>
			{
				return View["new_brewery.cshtml"];
			};
			Post["/database/breweries/new/success"] = _ =>
			{
				string name = Request.Form["name"];
				string location = Request.Form["location"];
				Brewery newBrewery = new Brewery(name, location);
				newBrewery.Save();
				return View["new_brewery_success.cshtml", newBrewery];
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
					bool userExists = false;
					return View["login.cshtml", userExists];
				}
			};
			Get["/logout"] = _ =>
			{
				NancyCookie idNumber = new NancyCookie("userId", "0");
				return View["login.cshtml"].WithCookie(idNumber);
			};

			//Test page
			Get["/recommend"] = _ =>
			{
				List<Beer> allBeers = Beer.GetAll();
				List<User> allUsers = User.GetAll();
				Dictionary<string, object> Model = new Dictionary<string, object>();
				Model["beers"] = allBeers;
				Model["users"] = allUsers;
				return View["recommend_form.cshtml", Model];
			};
			Post["/recommend/results"] = _ =>
			{
				List<Beer> recommendedBeers = new List<Beer>{};
				if (!(Request.Cookies.ContainsKey("userId")) || (Request.Cookies["userId"] == "0"))
				{
					User guest = new User("Guest");
					int beerId = int.Parse(Request.Form["beer-id"]);
					int listSize = int.Parse(Request.Form["list-size"]);
					recommendedBeers = guest.GetRecommendations(beerId, listSize);
				}
				else
				{
					int userId = int.Parse(Request.Cookies["userId"]);
					User foundUser = User.Find(userId);
					int beerId = int.Parse(Request.Form["beer-id"]);
					int listSize = int.Parse(Request.Form["list-size"]);
					recommendedBeers = foundUser.GetRecommendations(beerId, listSize);
				}
				return View["recommend_result.cshtml", recommendedBeers];
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
				return View["new_user.cshtml"];
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
					bool userExists = true;
					return View["new_user.cshtml", userExists];
				}
			};

			//Beer page
			Get["/beers"] = _ =>
			{
				List<Beer> allBeers = Beer.GetAll();
				return View["beers.cshtml", allBeers];
			};

			Get["/beers/ordered/{col}"] = parameters =>
			{
				List<Beer> allBeers = Beer.GetAll(parameters.col);
				return View["beers.cshtml", allBeers];
			};

			Get["/beers/search"] = _ =>
			{
				return View["search_beers.cshtml"];
			};

			Post["/beers/search"] = _ =>
			{
				string searchBy = Request.Form["search-type"];
				List<Beer> foundBeers = new List<Beer> {};

				if (searchBy == "abv" || searchBy == "ibu")
				{
					double searchInput = Request.Form["search-input"];
					foundBeers = Beer.Search(searchBy, searchInput);
				}
				else
				{
					string searchInput = Request.Form["search-input"];
					foundBeers = Beer.Search(searchBy, searchInput);
				}
				return View["search_beers.cshtml", foundBeers];
			};
			Get["/beers/{id}"] = parameters =>
			{
				Beer foundBeer = Beer.Find(parameters.id);
				return View["beer.cshtml", foundBeer];
			};
			Post["/beers/{id}"] = parameters =>
			{
				int userId = int.Parse(Request.Cookies["userId"]);
				int beerId = int.Parse(parameters.id);
				int rating = int.Parse(Request.Form["beerRating"]);
				User foundUser = User.Find(userId);
				foundUser.RateBeer(beerId, rating);
				Beer foundBeer = Beer.Find(beerId);
				return View["beer.cshtml", foundBeer];
			};

			//Brewery page
			Get["/breweries"] = _ =>
			{
				List<Brewery> allBreweries = Brewery.GetAll();
				return View["breweries.cshtml", allBreweries];
			};
			Get["/breweries/{id}"] = parameters =>
			{
				Brewery foundBrewery = Brewery.Find(parameters.id);
				return View["brewery.cshtml", foundBrewery];
			};
		}
	}
}

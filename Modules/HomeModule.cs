using Nancy;
using System.Collections.Generic;
using BeerRecommendation.Objects;

namespace BeerRecommendation
{
	public class HomeModule : NancyModule
	{
		public HomeModule()
		{
			Get["/"] = _ =>
			{
				return View["index.cshtml"];
			};
		}
	}
}
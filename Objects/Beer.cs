using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BeerRecommendation.Objects
{
	public class Beer
	{
		private int _id;
		private string _name;
		private float _abv;
		private float _ibu;

		public Beer(string name, float abv, float ibu, int id = 0)
		{
			_name = name;
			_abv = abv;
			_ibu = ibu;
			_id = id;
		}

		//Static methods
		

		//Other mentods


		//Overrides


		//Getters & Setters

	}
}

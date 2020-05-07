using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amida
{
	public static class Cooking
	{
		public enum OilTemp
		{
			high,
			moderate,
			low
		}

		public enum FriedFoodReview
		{
			good,
			usually,
			raw,
			bad
		}

		public enum FoodType
		{
			beef,
			chicken,
			pork,
			shrimp
		}

		public static OilTemp ToOilTemp(this FoodType foodType)
		{
			switch (foodType)
			{
				case FoodType.beef:
					return OilTemp.moderate;
				case FoodType.chicken:
					return OilTemp.low;
				case FoodType.pork:
					return OilTemp.moderate;
				case FoodType.shrimp:
					return OilTemp.high;
				default:
					throw new InvalidOperationException("unknown food type");
			}
		}
	}
}

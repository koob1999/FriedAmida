using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class FriedFood
{
	public Cooking.FoodType FoodType { get; private set; }
	public Cooking.FriedFoodReview FriedFoodReview { get; private set; }
	public int Calorie { get; private set; }

	public FriedFood(Cooking.FriedFoodReview review, int calorie, Cooking.FoodType foodType)
	{
		FriedFoodReview = review;
		Calorie = calorie;
		FoodType = foodType;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class FriedFood
{
	public Cooking.FriedFoodReview FriedFoodReview { get; private set; }
	public int Calorie { get; private set; }

	public FriedFood(Cooking.FriedFoodReview review, int calorie)
	{
		this.FriedFoodReview = review;
		this.Calorie = calorie;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class StrongMob : Customer
{
	override protected void SaveScore(FriedFood friedFood)
	{
		switch (friedFood.FriedFoodReview)
		{
			case Cooking.FriedFoodReview.good:
				totalScore += 500;
				totalGage += 1;
				break;
			case Cooking.FriedFoodReview.usually:
				totalScore += 100;
				break;
			case Cooking.FriedFoodReview.raw:
				totalScore += 100;
				break;
			case Cooking.FriedFoodReview.bad:
				totalScore -= 500;
				break;
		}
	}
}

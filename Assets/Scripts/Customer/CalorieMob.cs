using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class CalorieMob : Customer
{
	const int ClearCalorie = 600;
	int currentCalorie = ClearCalorie;

	override protected void RetensionScore(FriedFood friedFood)
	{
		switch (friedFood.FriedFoodReview)
		{
			case Cooking.FriedFoodReview.good:
				totalScore += 300;
				totalGage += 1;
				break;
			case Cooking.FriedFoodReview.usually:
				totalScore += 100;
				break;
			case Cooking.FriedFoodReview.raw:
				totalScore += 100;
				break;
			case Cooking.FriedFoodReview.bad:
				totalScore -= friedFood.Calorie - 100;
				break;
		}
	}

	//関数名が微妙
	override protected void CheckClear()
	{
		if (currentCalorie <= 0)
		{
			IsClear = true;
		}
		else
		{
			DoAction();
		}
	}
}

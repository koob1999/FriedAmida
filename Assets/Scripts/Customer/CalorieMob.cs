using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class CalorieMob : Customer
{
	const int ClearCalorie = 600;
	int currentCalorie = ClearCalorie;

	override public void CustomerReact(FriedFood friedFood)
	{
		if (friedFood != null)
		{
			currentCalorie -= friedFood.Calorie;
			CalorieGageAction(ClearCalorie, currentCalorie);
		}

		base.CustomerReact(friedFood);
	}

	override protected void SaveScore(FriedFood friedFood)
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
	override public void CheckClear()
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

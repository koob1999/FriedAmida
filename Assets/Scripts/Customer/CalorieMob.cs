using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class CalorieMob : Customer
{
	const int clearCalorie = 600;
	int currentCalorie = clearCalorie;

	override public void CustomerReact(FriedFood friedFood, AddPointDelegate addPointDelegate)
	{
		switch (friedFood.FriedFoodReview)
		{
			case Cooking.FriedFoodReview.good:
				addPointDelegate(1, 300);
				break;
			case Cooking.FriedFoodReview.usually:
				addPointDelegate(0, 100);
				break;
			case Cooking.FriedFoodReview.raw:
				addPointDelegate(0, 100);
				break;
			case Cooking.FriedFoodReview.bad:
				addPointDelegate(0, -(friedFood.Calorie - 100));
				break;
		}

		currentCalorie -= friedFood.Calorie;
		calorieGageDelegate(clearCalorie, currentCalorie);

		//アニメーション
		StartCoroutine(AnimeReacion(friedFood));
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

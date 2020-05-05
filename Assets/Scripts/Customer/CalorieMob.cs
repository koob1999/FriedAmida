using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class CalorieMob : Customer
{
	const int ClearCalorie = 600;
	int currentCalorie = ClearCalorie;

	override public void CustomerReact(FriedFood friedFood, AddPointDelegate addPointDelegate)
	{
		//揚げ物ができていないなら再行動
		if (friedFood == null)
		{
			DoAction();
			return;
		}

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
		CalorieGageAction(ClearCalorie, currentCalorie);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class CalorieMob : Customer
{
	int clearCalorie = 600;

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

		clearCalorie -= friedFood.Calorie;

		//アニメーション
		StartCoroutine(AnimeReacion(friedFood));
	}

	//関数名が微妙
	override protected void CheckClear()
	{
		if (clearCalorie <= 0)
		{
			IsClear = true;
		}
		else
		{
			DoAction();
		}
	}
}

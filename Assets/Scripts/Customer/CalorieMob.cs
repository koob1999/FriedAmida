using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class CalorieMob : Customer
{
	int clearCalorie = 600;

	override public void CustomerReact(FriedFood friedFood)
	{
		//アニメーション
		StartCoroutine(AnimeReacion(friedFood));

		switch (friedFood.FriedFoodReview)
		{
			case Cooking.FriedFoodReview.good:
				//スコア加算
				//ラッシュゲージ加算
				break;
			case Cooking.FriedFoodReview.usually:
				//スコア加算	
				break;
			case Cooking.FriedFoodReview.raw:
				//スコア加算
				break;
			case Cooking.FriedFoodReview.bad:
				//スコア加算
				break;
		}

		clearCalorie -= friedFood.Calorie;

		//クリア判定
		CheckClear();
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

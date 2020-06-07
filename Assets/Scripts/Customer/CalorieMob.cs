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

	override protected void TurnEndAction()
	{
		//全ての食材を揚げ終わったときのみ判定
		if (cookedFoodNum != SynchroFoodNum)
		{
			return;
		}

		//全ての食材が揚げ物になっていない場合再行動
		if (successFriedFoodNum == SynchroFoodNum)
		{
			AddPointAction(totalGage, totalScore);

			if (currentCalorie <= 0)
			{
				animator.SetTrigger("end");
			}
			else
			{
				DoAction();
			}
		}
		else
		{
			DoAction();
		}

		cookedFoodNum = 0;
		successFriedFoodNum = 0;
	}
}

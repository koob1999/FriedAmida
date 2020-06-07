using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class FoodFighterBoss : Boss
{
	const int ClearCalorie = 1000;
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

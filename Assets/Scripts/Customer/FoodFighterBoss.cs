using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class FoodFighterBoss : Boss
{
	const int ClearCalorie = 3000;
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

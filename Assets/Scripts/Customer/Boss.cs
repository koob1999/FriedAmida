using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class Boss : Customer
{
	override public bool IsClear
	{
		get { return isClear; }
		//GameManagerにCutomerを倒したことを表すメソッドをデリゲートで渡す
		protected set
		{
			isClear = value;
			if (isClear)
			{
				AddPointAction(0, totalScore);
				KilledCustomerAction();
			}
		}
	}

	override protected void SaveScore(FriedFood friedFood)
	{
		switch (friedFood.FriedFoodReview)
		{
			case Cooking.FriedFoodReview.good:
				totalGage = 2;
				break;
			case Cooking.FriedFoodReview.usually:
				totalGage = 0;
				break;
			case Cooking.FriedFoodReview.raw:
				totalGage = 0;
				break;
			case Cooking.FriedFoodReview.bad:
				totalGage = 0;
				break;
		}

		//一度でもbadを取っていればスコアは-3000固定
		if (totalScore == -3000)
		{
			return;
		}
		//バッドは取っていないがミスをしている場合
		else if (totalScore == 1000)
		{
			totalScore = friedFood.FriedFoodReview == Cooking.FriedFoodReview.bad ? -3000 : totalScore;
			return;
		}



		switch (friedFood.FriedFoodReview)
		{
			case Cooking.FriedFoodReview.good:
				totalScore = 3000;
				break;
			case Cooking.FriedFoodReview.usually:
				totalScore = 1000;
				break;
			case Cooking.FriedFoodReview.raw:
				totalScore = 1000;
				break;
			case Cooking.FriedFoodReview.bad:
				totalScore = -3000;
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
			AddPointAction(totalGage, 0);
			CheckClear();
		}
		else
		{
			DoAction();
		}

		cookedFoodNum = 0;
		successFriedFoodNum = 0;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class CalorieMob : Customer
{
	int clearCalorie = 600;


	override protected IEnumerator Reacion(FriedFood friedFood)
	{
		Debug.Log("override");
		//アニメーション
		ReactionAnime(friedFood.FryStatus);

		switch (friedFood.FryStatus)
		{
			case Cooking.Status.good:
				//スコア加算
				//ラッシュゲージ加算
				break;
			case Cooking.Status.usually:
				//スコア加算	
				break;
			case Cooking.Status.raw:
				//スコア加算
				break;
			case Cooking.Status.bad:
				//スコア加算
				break;
		}

		yield return new WaitForSeconds(1);

		clearCalorie -= friedFood.Calorie;

		//クリア判定
		CheckClear();
	}

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class GameManager : MonoBehaviour
{
	//[SerializeField] GameObject stageManager;
	[SerializeField] StageManager stageManager;
	[SerializeField] FoodGenerater foodGenerater;
	[SerializeField] ItemGenerater itemGenerater;

	[System.NonSerialized] public List<Oil> Oils;
	[System.NonSerialized] public List<Trash> Trashes;
	GameObject currentEnemyObj;//現在戦闘中の敵
	Customer currentCustomer;//●変数名微妙●

	//●Playerクラスに分ける可能性あり●
	int rushGage;
	int score;

    // Start is called before the first frame update
    void Start()
    {
		foreach (Oil oil in Oils)
		{
			oil.completedFriedFoodDelegate = CompletedFriedFood;
		}

		foreach(Trash trash in Trashes)
		{
			trash.completedFriedFoodDelegate = CompletedFriedFood;
		}

		foodGenerater.oilAnimeDelegate = StartOilAnime;

		Invoke("GameStart", 0.5f);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	void GameStart()
	{
		//１：次の戦闘準備
		AppearNextEnemy();

		Debug.Log("b");
		//2：敵に応じた行動
		currentCustomer.DoAction();
	}

	//敵生成
	void AppearNextEnemy()
	{
		currentEnemyObj = stageManager.NextBattleStart();
		currentCustomer = currentEnemyObj.GetComponent<Customer>();

		currentCustomer.foodGenerater = this.foodGenerater;
		currentCustomer.itemGenerater = this.itemGenerater;
		currentCustomer.killedCustomerDelegate = KilledCustomer;
		Debug.Log("a");
	}

	void KilledCustomer()
	{
		Destroy(currentEnemyObj);

		if (stageManager.IsLastEnemy())
		{
			ClearGame();
			return;
		}

		AppearNextEnemy();
		currentCustomer.DoAction();
	}

	void CompletedFriedFood(FriedFood friedFood)
	{
		//3：揚げ物を敵に渡す
		// ：敵による揚げ物評価(スコア処理未実装）
		if (friedFood != null)
		{
			currentCustomer.CustomerReact(friedFood);
		}
		else
		{
			currentCustomer.DoAction();
		}
	}

	void ClearGame()
	{
		//８：スコア表示等

		//9：次のステージへ
		Debug.Log("clear");
	}

	void StartOilAnime(Cooking.OilTemp oilTemp)
	{
		foreach(Oil oil in Oils)
		{
			oil.DoTargetAmime(oil.oilTemp == oilTemp);
		}
	}
}

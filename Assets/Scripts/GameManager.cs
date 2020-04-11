using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	//[SerializeField] GameObject stageManager;
	[SerializeField] StageManager stageManager;
	[SerializeField] FoodGenerater foodGenerater;

	[System.NonSerialized] public GameObject[] OilObjs;
	GameObject currentEnemyObj;//現在戦闘中の敵
	Customer currentCustomer;//●変数名微妙●

	//●Playerクラスに分ける可能性あり●
	int rushGage;
	int score;

    // Start is called before the first frame update
    void Start()
    {
		Invoke("GameStart", 0.5f);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	void GameStart()
	{
		//１：次の戦闘準備
		currentEnemyObj = stageManager.NextBattleStart();
		currentCustomer = currentEnemyObj.GetComponent<Customer>();

		currentCustomer.killedCustomerDelegate = KilledCustomer;

		foreach (GameObject oilObj in OilObjs)
		{
			oilObj.GetComponent<Oil>().completedFriedFoodDelegate = CompletedFriedFood;
		}

		//2：敵に応じた食材生成
		foodGenerater.FoodsGenerate(currentCustomer.SynchroFoodNum);
	}

	void KilledCustomer()
	{
		Destroy(currentEnemyObj);

		if (stageManager.IsLastEnemy())
		{
			ClearGame();
			return;
		}

		currentEnemyObj = stageManager.NextBattleStart();
		currentCustomer = currentEnemyObj.GetComponent<Customer>();

		currentCustomer.killedCustomerDelegate = KilledCustomer;
	}

	void CompletedFriedFood(FriedFood friedFood)
	{
		//3：揚げ物を敵に渡す
		// ：敵による揚げ物評価(スコア処理未実装）
		if (friedFood != null)
		{
			currentCustomer.DoReaction(friedFood);
		}

		//2：敵に応じた食材生成
		foodGenerater.FoodsGenerate(currentCustomer.SynchroFoodNum);
	}

	void ClearGame()
	{
		//８：スコア表示等

		//9：次のステージへ
		Debug.Log("clear");
	}
}

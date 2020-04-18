using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class GameManager : MonoBehaviour
{
	//参照パス
	[SerializeField] StageManager stageManager;
	[SerializeField] FoodGenerater foodGenerater;
	[SerializeField] ItemGenerater itemGenerater;
	[SerializeField] LineCursol lineCursol;

	[System.NonSerialized] public List<Oil> Oils;
	[System.NonSerialized] public List<Trash> Trashes;
	GameObject currentEnemyObj;//現在戦闘中の敵
	Customer currentCustomer;//●変数名微妙●
	[System.NonSerialized] public HorizontalLine[,] AmidaLines;

	bool isRush = false;
	bool IsRush
	{
		get { return this.isRush; }

		set
		{
			isRush = value;
			itemGenerater.isRush = this.isRush;
			//●もっときれいにやりたい●
			foreach(Trash trash in Trashes)
			{
				trash.ChangeOil(value);
			}
			//BGM変更
			//ゲーム速度少し上昇
			//制限時間停止
		}
	}

	//●Playerクラスに分ける可能性あり●
	const int MaxDrawLineNum = 3;
	int rushGage;
	public int RushGage
	{
		get { return this.rushGage; }

		set
		{
			if (rushGage >= 10)
			{
				rushGage = 0;
				IsRush = true;
				//●デリゲートと秒数受け取って処理を遅らせる関数作ってもいいかも●
				Invoke("RushEnd", 5);
			}
		}
	}
	int score;
	public int Score
	{
		get { return this.score; }

		set
		{
			if (IsRush)
			{
				score += (int)(1.5 * value);
			}
			else
			{
				score = value;
			}
		}
	}

	int remainLines = MaxDrawLineNum;
	public int RemainLines
	{
		get { return this.remainLines; }

		set
		{
			if (value > MaxDrawLineNum)
			{
				remainLines = MaxDrawLineNum;
			}
			else if (value < 0)
			{
				remainLines = 0;
			}
			else
			{
				remainLines = value;
			}
		}
	}

	int combo = 0;
	int maxCombo;

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
		//揚げ物を敵に渡す
		//敵による揚げ物評価(スコア処理未実装）
		if (friedFood != null)
		{
			//●ここら辺もっときれいにできる気がする●
			currentCustomer.CustomerReact(friedFood, (int rushGage, int score)=> { RushGage += rushGage; Score += score; });

			//コンボ処理
			if (friedFood.FriedFoodReview == Cooking.FriedFoodReview.good)
			{
				combo++;
			}
			else
			{
				maxCombo = combo > maxCombo ? combo : maxCombo;
				combo = 0;
			}
		}
		else
		{
			currentCustomer.DoAction();
		}

		//あみだのリセット
		for (int i = 0; i < AmidaLines.GetLength(0); i++)
		{
			for (int j = 0; j < AmidaLines.GetLength(1); j++)
			{
				Debug.Log("OFF");
				AmidaLines[i, j].SetOnObjActivation(false);
			}
		}
	}

	void ClearGame()
	{
		//８：スコア表示等

		//9：次のステージへ
		Debug.Log("clear");
	}

	void RushEnd()
	{
		IsRush = false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Amida;
using System;

public class GameManager : MonoBehaviour
{
	//参照パス
	[SerializeField] StageManager stageManager;
	[SerializeField] FoodGenerater foodGenerater;
	[SerializeField] ItemGenerater itemGenerater;
	[SerializeField] LineCursol lineCursol;
	[SerializeField] PlayerController playerController;
	[SerializeField] Image calorieGageImage;

	[System.NonSerialized] public List<Oil> Oils;
	[System.NonSerialized] public List<Trash> Trashes;
	GameObject currentEnemyObj;//現在戦闘中の敵
	Customer currentCustomer;//●変数名微妙●
	[System.NonSerialized] public HorizontalLine[,] AmidaLines;

	bool isRush = false;
	public bool IsRush
	{
		get { return isRush; }

		set
		{
			isRush = value;
			itemGenerater.IsRush = isRush;
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

		playerController.rushDelegate = (bool isRush) =>
		{
			itemGenerater.IsRush = isRush;
			//●もっときれいにやりたい●
			foreach (Trash trash in Trashes)
			{
				trash.ChangeOil(isRush);
			}
			//BGM変更
			//ゲーム速度少し上昇
			//制限時間停止
		};

		Invoke("GameStart", 0.5f);
	}

    // Update is called once per frame
    void Update()
    {
		//倍速処理
		if (Input.GetKeyDown(KeyCode.X))
		{
			Time.timeScale = 2;
		}
		if (Input.GetKeyUp(KeyCode.X))
		{
			Time.timeScale = 1;
		}
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

		currentCustomer.foodGenerater = foodGenerater;
		currentCustomer.itemGenerater = itemGenerater;
		currentCustomer.killedCustomerDelegate = KilledCustomer;
		//客がカロリーゲージを持たない場合はゲージは非表示にする
		calorieGageImage.gameObject.transform.parent.gameObject.SetActive(currentCustomer.hasClalorie);
		calorieGageImage.fillAmount = 1;
		currentCustomer.calorieGageDelegate = (int clearCalorie, int currentCalorie) =>
		{
			calorieGageImage.fillAmount = (float)currentCalorie / clearCalorie;
		};
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
			currentCustomer.CustomerReact(friedFood,
				(int rushGage, int score) =>
				{
					playerController.Score += playerController.IsRush ? (int)(score * 1.5f) : score;
					playerController.RushGage += rushGage;
				});

			playerController.Combo = friedFood.FriedFoodReview == Cooking.FriedFoodReview.good ? playerController.Combo + 1 : 0;
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
}

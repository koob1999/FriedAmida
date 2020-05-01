﻿using System.Collections;
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
	[SerializeField] Text scoreText;
	[SerializeField] Text remainLinesText;
	[SerializeField] Text comboText;
	[SerializeField] Image rushGageImage;
	[SerializeField] Image calorieGageImage;
	[SerializeField] Text minuteText;
	[SerializeField] Text secandText;

	[System.NonSerialized] public List<Oil> Oils;
	[System.NonSerialized] public List<Trash> Trashes;
	GameObject currentEnemyObj;//現在戦闘中の敵
	Customer currentCustomer;//●変数名微妙●
	[System.NonSerialized] public HorizontalLine[,] AmidaLines;

	[SerializeField] int limitTime;
	bool IsTimeOver => limitTime <= 0;

	bool isRush = false;
	bool IsRush
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

	//●Playerクラスに分ける可能性あり●
	const int MaxDrawLineNum = 3;

	const float RushTime = 20;
	const int MaxRushGage = 1;
	int rushGage;
	public int RushGage
	{
		get { return rushGage; }

		set
		{
			if (IsRush)
			{
				return;
			}

			if (value >= MaxRushGage)
			{
				rushGage = MaxRushGage;
			}
			else
			{
				rushGage = value;
			}

			if (rushGage >= MaxRushGage)
			{
				Debug.Log("RushTime");
				StartCoroutine(RushTimeCoroutine());
			}
		}
	}
	int score;
	public int Score
	{
		get { return score; }

		set
		{
			score = value;
			scoreText.text = "SCORE:" + Score.ToString();
		}
	}

	int remainLines = MaxDrawLineNum;
	public int RemainLines
	{
		get { return remainLines; }

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
			remainLinesText.text = "残り本数" + RemainLines.ToString() + "/" + MaxDrawLineNum.ToString();
		}
	}

	int combo = 0;
	public int Combo
	{
		get { return combo; }

		set
		{
			combo = value;
			if (combo == 0)
			{
				comboText.gameObject.SetActive(false);
			}
			else
			{
				comboText.gameObject.SetActive(true);
				comboText.text = combo.ToString() + "コンボ";
			}
		}
	}
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

		StartCoroutine(CountTime());

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

		currentCustomer.FoodGenerater = foodGenerater;
		currentCustomer.ItemGenerater = itemGenerater;
		currentCustomer.killedCustomerDelegate = KilledCustomer;
		//客がカロリーゲージを持たない場合はゲージは非表示にする
		calorieGageImage.gameObject.transform.parent.gameObject.SetActive(currentCustomer.HasClalorie);
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
		//ゲームオーバーの時はここで止まる
		if (IsTimeOver)
		{
			return;
		}

		//揚げ物を敵に渡す
		//敵による揚げ物評価(スコア処理未実装）
		if (friedFood != null)
		{
			//●ここら辺もっときれいにできる気がする●
			currentCustomer.CustomerReact(friedFood,
				(int rushGage, int score) =>
				{
					Score += IsRush ? (int)(score * 1.5f) : score;
					RushGage += rushGage;
					Debug.Log(score);
				});

			//コンボ処理
			if (friedFood.FriedFoodReview == Cooking.FriedFoodReview.good)
			{
				Combo++;
			}
			else
			{
				maxCombo = Math.Max(maxCombo, Combo);
				Combo = 0;
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

	IEnumerator RushTimeCoroutine()
	{
		float time = 0;
		IsRush = true;

		while (time < RushTime)
		{
			time += Time.deltaTime;
			rushGageImage.fillAmount = (RushTime - time) / RushTime;
			yield return null;
		}

		rushGage = 0;
		rushGageImage.fillAmount = 1;
		IsRush = false;
	}

	IEnumerator CountTime()
	{
		DisplayTime();

		while (!IsTimeOver)
		{
			yield return new WaitForSeconds(1);
			limitTime--;
			DisplayTime();
		}
	}

	void DisplayTime()
	{
		int minute = limitTime / 60;
		int secand = limitTime - minute * 60;

		minuteText.text = minute.ToString("D2");
		secandText.text = secand.ToString("D2");
	}
}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Amida;

public class StageManager : MonoBehaviour
{
	enum Enemy
	{
		nomal,
		calorieMob,
		strongMob,
		foodFighterBoss,
		groumetBoss
	}


	[SerializeField] Text norumaText;

	//▼敵関連
	[SerializeField] Enemy[] enemies;//出現する敵の設定用
	[SerializeField] GameObject nomalMob;//敵のPrefab設定用
	[SerializeField] GameObject calorieMob;
	[SerializeField] GameObject strongMob;
	[SerializeField] GameObject foodFighterBoss;
	[SerializeField] GameObject groumetBoss;
	[SerializeField] GameObject enemyGeneratePlace;//敵の生成場所設定場所
	[SerializeField] StageGenerater stageGenerater;
	[NonSerialized] public List<Oil> Oils;
	[NonSerialized] public List<Trash> Trashes;
	[NonSerialized] public HorizontalLine[,] AmidaLines;

	int presentEnemyNum;//現在現れている敵が何番目か
	public int PresentEnemyNum
	{
		get { return this.presentEnemyNum; }

		set
		{
			presentEnemyNum = value;
			norumaText.text = "ノルマあと" + (enemies.Length - presentEnemyNum + 1).ToString() + "人";
		}
	}

	//●Playerクラスに分ける可能性あり●
	const int MaxDrawLineNum = 6;
	public Action<int,int> UpdateRemainLinesGage;
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
			UpdateRemainLinesGage(remainLines, MaxDrawLineNum);
		}
	}

    // Start is called before the first frame update
    void Start()
    {
		stageGenerater.GenerateStage(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void WaveStart()
	{
		PresentEnemyNum = 0;
	}

	//●返り値なしの関数にして変数に格納してもいい気がする●
	//次の敵を返り値に持つ
	public GameObject NextBattleStart()
	{
		//▼次の敵を設定
		GameObject nextEnemy = SelectEnemy();
		Customer customer = nextEnemy.GetComponent<Customer>();

		//▼敵生成
		GameObject enemy = Instantiate(nextEnemy, enemyGeneratePlace.transform.position, Quaternion.identity);

		//▼出現する敵の順番を1進める
		PresentEnemyNum++;

		return enemy;
	}


	GameObject SelectEnemy()
	{
		Enemy enemy = enemies[PresentEnemyNum];

		switch (enemy)
		{
			case Enemy.nomal:
				return nomalMob;
			case Enemy.calorieMob:
				return calorieMob;
			case Enemy.strongMob:
				return strongMob;
			case Enemy.foodFighterBoss:
				return foodFighterBoss;
			case Enemy.groumetBoss:
				return groumetBoss;
			default:
				return nomalMob;
		}
	}

	public bool IsLastEnemy()
	{
		return PresentEnemyNum == enemies.Length;
	}

	public void ResetAmidaLines()
	{
		//あみだのリセット
		for (int i = 0; i < AmidaLines.GetLength(0); i++)
		{
			for (int j = 0; j < AmidaLines.GetLength(1); j++)
			{
				AmidaLines[i, j].SetOnObjActivation(false);
			}
		}
	}

	public void SetCompletedActionToOil(Oil.CompletedFriedFoodDelegate completedFriedFood)
	{
		foreach (Oil oil in Oils)
		{
			oil.CompletedFriedFoodAction = completedFriedFood;
		}

		foreach (Trash trash in Trashes)
		{
			trash.CompletedFriedFoodAction = completedFriedFood;
		}
	}

	public void ChangeTrashToOil(bool isRush)
	{
		//●もっときれいにやりたい●
		foreach (Trash trash in Trashes)
		{
			trash.ChangeOil(isRush);
		}
	}

	void SetOilOutline(Cooking.OilTemp oilTemp)
	{
		Oils.FindAll(oil => oil.OilTemp == oilTemp).ForEach(oil => oil.SetOutline(true));
	}

	public void SetOilOutline(Cooking.OilTemp[] oilTemps)
	{
		foreach (Oil oil in Oils)
		{
			oil.SetOutline(false);
		}
		foreach (Cooking.OilTemp oilTemp in oilTemps)
		{
			SetOilOutline(oilTemp);
		}
	}

	public void SetOilOutline(Cooking.FoodType[] foodTypes)
	{
		SetOilOutline(Array.ConvertAll(foodTypes, foodType => foodType.ToOilTemp()));
	}
}

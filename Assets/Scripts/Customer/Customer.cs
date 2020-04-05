﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class Customer : MonoBehaviour
{	
	[System.SerializableAttribute]
	public class ItemNums
	{
		public int eggNum;
		public int komugikoNum;
		public int pankoNum;
		public int badItemNum;
	}

	//▼アイテム関連
	int synchroFoodNum = 1;
	public int SynchroFoodNum
	{
		get { return synchroFoodNum; }
		private set { synchroFoodNum = value; }
	}

	[SerializeField] ItemNums appearItemNums;
	public ItemNums AppearItemNum
	{
		get { return this.appearItemNums; }
		private set { appearItemNums = value; }
	}

	//▼アニメーション関連
	Animator animator;

	//▼クリア判定
	bool isClear = false;
	public bool IsClear
	{
		get { return isClear; }
		private set { isClear = value; }
	}

	// Start is called before the first frame update
	void Start()
    {
		animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void DoReaction(FriedFood friedFood)
	{
		switch (friedFood.FryStatus)
		{
			case Cooking.Status.good:
				//アニメーション
				//スコア加算
				//ラッシュゲージ加算
				break;
			case Cooking.Status.usually:
				//アニメーション
				//スコア加算	
				break;
			case Cooking.Status.raw:
				//アニメーション
				//スコア加算
				break;
			case Cooking.Status.bad:
				//アニメーション
				//スコア加算
				break;
		}

		//クリア判定
		IsClear = true;
	}
}

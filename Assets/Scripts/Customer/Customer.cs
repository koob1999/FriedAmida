using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class Customer : MonoBehaviour
{	
	[System.SerializableAttribute]
	public class ItemNums
	{
		public int egg;
		public int komugiko;
		public int panko;
		public int badItem;
	}
	//▼参照パス
	[System.NonSerialized] public FoodGenerater foodGenerater;
	[System.NonSerialized] public ItemGenerater itemGenerater;

	//▼アイテム関連
	//同時揚げの量
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

	[System.NonSerialized] public Cooking.FoodType[] foodTypes;

	//▼アニメーション関連
	Animator animator;

	//▼クリア判定
	bool isClear = false;
	public bool IsClear
	{
		get { return isClear; }
		//GameManagerにCutomerを倒したことを表すメソッドをデリゲートで渡す
		private set
		{
			isClear = value;
			if (isClear)
			{
				killedCustomerDelegate();
			}
		}
	}

	public delegate void KilledCustomerDelegate();
	public KilledCustomerDelegate killedCustomerDelegate;

	// Start is called before the first frame update
	void Awake()
    {
		animator = GetComponent<Animator>();
		foodTypes = new Cooking.FoodType[SynchroFoodNum];
		Debug.Log(SynchroFoodNum);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void DoAction()
	{
		itemGenerater.InitializeItems(AppearItemNum.egg, AppearItemNum.komugiko, AppearItemNum.panko, AppearItemNum.badItem);

		FoodTypesSelect();
		foodGenerater.FoodsGenerate(foodTypes);
	}

	void FoodTypesSelect()
	{
		int rand;

		for(int i = 0; i < SynchroFoodNum; i++)
		{
			rand = Random.Range(1, 5);

			switch (rand)
			{
				case 1:
					foodTypes[i] = Cooking.FoodType.beef;
					break;
				case 2:
					foodTypes[i] = Cooking.FoodType.chicken;
					break;
				case 3:
					foodTypes[i] = Cooking.FoodType.pork;
					break;
				case 4:
					foodTypes[i] = Cooking.FoodType.shrimp;
					break;
			}
		}
	}

	public void DoReaction(FriedFood friedFood)
	{
		StartCoroutine(Reacion(friedFood));
	}

	IEnumerator Reacion(FriedFood friedFood)
	{
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

		//クリア判定
		IsClear = true;
	}

	void ReactionAnime(Cooking.Status status)
	{
		Debug.Log("anime");
		switch (status)
		{
			case Cooking.Status.good:
				Debug.Log("good");
				animator.SetBool("good", true);
				animator.SetBool("angry", false);
				animator.SetBool("saitei", false);
				break;
			case Cooking.Status.usually:
				Debug.Log("usually");
				animator.SetBool("good", false);
				animator.SetBool("angry", true);
				animator.SetBool("saitei", false);
				break;
			case Cooking.Status.raw:
				Debug.Log("raw");
				animator.SetBool("good", false);
				animator.SetBool("angry", true);
				animator.SetBool("saitei", false);
				break;
			case Cooking.Status.bad:
				Debug.Log("bad");
				animator.SetBool("good", false);
				animator.SetBool("angry", false);
				animator.SetBool("saitei", true);
				break;
		}
	}
}

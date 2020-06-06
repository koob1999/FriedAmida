using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class Customer : MonoBehaviour
{	
	[SerializableAttribute]
	public class ItemNums
	{
		public int egg;
		public int komugiko;
		public int panko;
		public int badItem;
	}

	public delegate void AddPointDelegate(int rushGage, int score);
	public AddPointDelegate AddPointAction;
	public delegate void CalorieGageDelegate(int clearCalorie, int currentCalorie);
	public CalorieGageDelegate CalorieGageAction;
	public delegate void AmidaResetDelegate();
	public AmidaResetDelegate AmidaResetAction;
	public bool HasClalorie;
	protected int cookedFoodNum = 0;   //一回の行動で調理した食材の数
	int CookedFoodNum
	{
		get { return cookedFoodNum; }

		set
		{
			cookedFoodNum = value;

			TurnEndAction();
		}
	}
	protected int successFriedFoodNum = 0;	//実際に揚がった揚げ物の数
	protected int totalScore = 0;
	protected int totalGage = 0;

	//▼参照パス
	[NonSerialized] public FoodGenerater FoodGenerater;
	[NonSerialized] public ItemGenerater ItemGenerater;
	[NonSerialized] public StageManager stageManager;
	//▼アイテム関連
	//同時揚げの量
	[SerializeField] int synchroFoodNum;
	public int SynchroFoodNum
	{
		get { return synchroFoodNum; }
		protected set { synchroFoodNum = value; }
	}

	[SerializeField] ItemNums appearItemNums;
	public ItemNums AppearItemNum
	{
		get { return this.appearItemNums; }
		private set { appearItemNums = value; }
	}

	[NonSerialized] public Cooking.FoodType[] FoodTypes;

	//▼アニメーション関連
	protected Animator animator;

	//▼クリア判定
	protected bool isClear = false;
	virtual public bool IsClear
	{
		get { return isClear; }
		//GameManagerにCutomerを倒したことを表すメソッドをデリゲートで渡す
		protected set
		{
			isClear = value;
			if (isClear)
			{
				KilledCustomerAction();
			}
		}
	}

	public delegate void KilledCustomerDelegate();
	public KilledCustomerDelegate KilledCustomerAction;

	// Start is called before the first frame update
	void Awake()
    {
		animator = GetComponent<Animator>();
		FoodTypes = new Cooking.FoodType[SynchroFoodNum];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	//関数名が微妙
	public void DoAction()
	{
		ItemGenerater.InitializeItems(AppearItemNum.egg, AppearItemNum.komugiko, AppearItemNum.panko, AppearItemNum.badItem);
		AmidaResetAction();

		FoodTypesSelect();
		stageManager.SetOilOutline(FoodTypes);
		FoodGenerater.FoodsGenerate(FoodTypes);
	}

	void FoodTypesSelect()
	{
		int rand;

		for(int i = 0; i < SynchroFoodNum; i++)
		{
			rand = UnityEngine.Random.Range(1, 5);

			switch (rand)
			{
				case 1:
					FoodTypes[i] = Cooking.FoodType.beef;
					break;
				case 2:
					FoodTypes[i] = Cooking.FoodType.chicken;
					break;
				case 3:
					FoodTypes[i] = Cooking.FoodType.pork;
					break;
				case 4:
					FoodTypes[i] = Cooking.FoodType.shrimp;
					break;
			}
		}
	}

	virtual public void CustomerReact(FriedFood friedFood)
	{
		if (friedFood != null)
		{
			successFriedFoodNum++;
			SaveScore(friedFood);

			//アニメーション
			StartCoroutine(AnimeReacion(friedFood));
		}
	}

	protected IEnumerator AnimeReacion(FriedFood friedFood)
	{
		//アニメーション

		switch (friedFood.FriedFoodReview)
		{
			case Cooking.FriedFoodReview.good:
				animator.SetBool("good", true);
				animator.SetBool("angry", false);
				animator.SetBool("saitei", false);
				break;
			case Cooking.FriedFoodReview.usually:
				animator.SetBool("good", false);
				animator.SetBool("angry", true);
				animator.SetBool("saitei", false);
				break;
			case Cooking.FriedFoodReview.raw:
				animator.SetBool("good", false);
				animator.SetBool("angry", true);
				animator.SetBool("saitei", false);
				break;
			case Cooking.FriedFoodReview.bad:
				animator.SetBool("good", false);
				animator.SetBool("angry", false);
				animator.SetBool("saitei", true);
				break;
		}
		yield return new WaitForSeconds(1);

		CookedFoodNum++;
	}

	virtual protected void TurnEndAction()
	{
		//全ての食材を揚げ終わったときのみ判定
		if (cookedFoodNum != SynchroFoodNum)
		{
			return;
		}
			
		//全ての食材が揚げ物になっていない場合再行動
		if (successFriedFoodNum == SynchroFoodNum)
		{
			AddPointAction(totalGage, totalScore);
			//CheckClear();
			animator.SetTrigger("end");
		}
		else
		{
			DoAction();
		}
	
		cookedFoodNum = 0;
		successFriedFoodNum = 0;
	}

	virtual protected void SaveScore(FriedFood friedFood)
	{
		switch (friedFood.FriedFoodReview)
		{
			case Cooking.FriedFoodReview.good:
				totalScore += 300;
				totalGage += 1;
				break;
			case Cooking.FriedFoodReview.usually:
				totalScore += 100;
				break;
			case Cooking.FriedFoodReview.raw:
				totalScore += 100;
				break;
			case Cooking.FriedFoodReview.bad:
				totalScore -= 500;
				break;
		}
	}

	virtual public void CheckClear()
	{
		IsClear = true;
	}
}

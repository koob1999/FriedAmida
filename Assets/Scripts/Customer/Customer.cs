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

	public delegate void AddPointDelegate(int rushGage, int score);
	public delegate void CalorieGageDelegate(int clearCalorie, int currentCalorie);
	public CalorieGageDelegate CalorieGageAction;
	public delegate void AmidaResetDelegate();
	public AmidaResetDelegate AmidaResetAction;
	public bool HasClalorie;

	//▼参照パス
	[System.NonSerialized] public FoodGenerater FoodGenerater;
	[System.NonSerialized] public ItemGenerater ItemGenerater;

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

	[System.NonSerialized] public Cooking.FoodType[] FoodTypes;

	//▼アニメーション関連
	Animator animator;

	//▼クリア判定
	bool isClear = false;
	public bool IsClear
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
		FoodGenerater.FoodsGenerate(FoodTypes);
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

	virtual public void CustomerReact(FriedFood friedFood, AddPointDelegate addPointDelegate)
	{
		//揚げ物ができていないなら再行動
		if (friedFood == null)
		{
			DoAction();
			return;
		}

		switch (friedFood.FriedFoodReview)
		{
			case Cooking.FriedFoodReview.good:
				addPointDelegate(1, 300);
				break;
			case Cooking.FriedFoodReview.usually:
				addPointDelegate(0, 100);
				break;
			case Cooking.FriedFoodReview.raw:
				addPointDelegate(0, 100);
				break;
			case Cooking.FriedFoodReview.bad:
				addPointDelegate(0, -500);
				break;
		}

		//アニメーション
		StartCoroutine(AnimeReacion(friedFood));
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

		//クリア判定
		if (friedFood.IsLastFood)
		{
			CheckClear();
		}
	}

	virtual protected void CheckClear()
	{
		IsClear = true;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class Food : MonoBehaviour
{
	//▼移動関連
	int xDirction = 0;
	float speed = 0.02f;

	//▼レシピ
	[SerializeField] int clearKomugikoNum;
	[SerializeField] int clearEggNum;
	[SerializeField] int clearPankoNum;
	public Cooking.OilTemp oilTemp;

	[SerializeField] int calorie;

	//▼所持アイテム
	int komugikoCount = 0;
	int eggCount = 0;
	int pankoCount = 0;
	int badItemCount = 0;
	int rushItemCount = 0;

	//▼落下してるか
	[System.NonSerialized] public bool isFall = false;

	//▼アニメーション
	Animator animator;

	public int XDirection
	{
		set
		{
			if (value <= -1)
			{
				xDirction = -1;
			}
			else if (value >= 1)
			{
				xDirction = 1;
			}
			else
			{
				xDirction = 0;
			}
		}

		get
		{
			return xDirction;
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (isFall)
		{
			//▼移動
			if (xDirction == 0)
			{
				transform.Translate(0, -1 * speed, 0);
			}
			else
			{
				transform.Translate(xDirction * speed, 0, 0);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		string tag = col.gameObject.tag;

		switch (tag)
		{
			//あみだの横線
			case "right":
				transform.position = col.gameObject.transform.position;
				ChangeDirection(-1);
				break;
			case "left":
				transform.position = col.gameObject.transform.position;
				ChangeDirection(1);
				break;

			//アイテム（パン粉等）
			case "egg":
				eggCount++;
				break;
			case "komugiko":
				komugikoCount++;
				break;
			case "panko":
				pankoCount++;
				break;
			case "badItem":
				badItemCount++;
				break;
			case "rushItem":
				rushItemCount++;
				break;
		}
	}

	//あみだでの交差点での処理に使用
	void ChangeDirection(int x)
	{
		XDirection = XDirection != 0 ? 0 : x;
	}

	public FriedFood DoFry(Cooking.OilTemp oil)
	{
		FriedFood friedFood;

		//ラッシュアイテムを取得していれば必ずGOOD
		if (rushItemCount >= 1)
		{
			Debug.Log("rush");
			friedFood = new FriedFood(Cooking.FriedFoodReview.good, this.calorie);
		}
		//マイナスアイテムを取得していれば腐り状態
		else if (badItemCount >= 1)
		{
			Debug.Log("bad");
			friedFood = new FriedFood(Cooking.FriedFoodReview.bad, this.calorie);

		}
		else
		{
			//油の温度が適正、材料全てがノルマ以上取得していれば揚げ成功
			if (oil == oilTemp && eggCount >= clearEggNum && komugikoCount >= clearKomugikoNum && pankoCount >= clearPankoNum)
			{
				Debug.Log("good");
				friedFood = new FriedFood(Cooking.FriedFoodReview.good, this.calorie);
			}
			//材料を全く取得していなければ素揚げ、生？
			else if (eggCount == 0 && komugikoCount == 0 && pankoCount == 0)
			{
				Debug.Log("row");
				friedFood = new FriedFood(Cooking.FriedFoodReview.raw, this.calorie);
			}
			//それ以外なら揚げ失敗
			else
			{
				Debug.Log("usually");
				friedFood = new FriedFood(Cooking.FriedFoodReview.usually, this.calorie);
			}
		}
		return friedFood;
	}

	public void AnimeFlash()
	{
		animator.SetBool("flash", true);
	}

	public void StartFall()
	{
		isFall = true;
	}
}

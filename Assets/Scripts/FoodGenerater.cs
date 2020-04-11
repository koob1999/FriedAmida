using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class FoodGenerater : MonoBehaviour
{
	[SerializeField] GameObject beef;
	[SerializeField] GameObject chiken;
	[SerializeField] GameObject pork;
	[SerializeField] GameObject shrimp;

	[System.NonSerialized] public GameObject[] GeneratePlaces;
	[SerializeField] int generateSpan;
	public delegate void OilAnimeDelegate(Cooking.OilStatus oilStatus);
	public OilAnimeDelegate oilAnimeDelegate;

    // Start is called before the first frame update
    void Start()
    {
		
	}

    // Update is called once per frame
    void Update()
    {

    }

	//●単純にずらして食材を生成しているだけなので改良の必要あり●
	//●オーバーロードっぽく見えるのも気になる●
	public void FoodsGenerate(Cooking.FoodType[] foodTypes)
	{
		StartCoroutine(FoodsGeneraterCoroutine(foodTypes));
	}

	IEnumerator FoodsGeneraterCoroutine(Cooking.FoodType[] foodTypes)
	{
		foreach(Cooking.FoodType foodType in foodTypes)
		{
			FoodGenerate(foodType);
			yield return new WaitForSeconds(3);
		}
	}

	void FoodGenerate(Cooking.FoodType foodType)
	{
		GameObject food;
		GameObject place;

		//▼生成食材の決定
		switch (foodType)
		{
			case Cooking.FoodType.beef:
				food = beef;
				break;
			case Cooking.FoodType.chicken:
				food = chiken;
				break;
			case Cooking.FoodType.pork:
				food = pork;
				break;
			case Cooking.FoodType.shrimp:
				food = shrimp;
				break;
			default:
				food = beef;
				break;
		}

		//▼生成位置の決定
		int rand = Random.Range(0, GeneratePlaces.Length);
		place = GeneratePlaces[rand];

		//▼目標油のアニメーション再生
		oilAnimeDelegate(food.GetComponent<Food>().oilTemp);

		//▼生成
		Instantiate(food, place.transform.position, Quaternion.identity);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerater : MonoBehaviour
{
	[SerializeField] GameObject[] foods;
	[System.NonSerialized] public GameObject[] GeneratePlaces;
	[SerializeField] int generateSpan;

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
	public void FoodsGenerate(int genetateNum)
	{
		for(int i = 0; i < genetateNum; i++)
		{
			Invoke("FoodGenerate", i * generateSpan);
		}
	}

	void FoodGenerate()
	{
		GameObject food;
		GameObject place;

		//▼生成食材の決定
		int rand = Random.Range(0, foods.Length);
		food = foods[rand];

		//▼生成位置の決定
		rand = Random.Range(0, GeneratePlaces.Length);
		place = GeneratePlaces[rand];

		//▼生成
		Instantiate(food, place.transform.position, Quaternion.identity);
	}
}

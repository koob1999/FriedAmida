using System;
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

	[NonSerialized] public GameObject[] GeneratePlaces;
	[SerializeField] int generateSpan;

	public bool IsRush;

    // Start is called before the first frame update
    void Start()
    {
		
	}

    // Update is called once per frame
    void Update()
    {

    }

	//●オーバーロードっぽく見えるのも気になる●
	public void FoodsGenerate(Cooking.FoodType[] foodTypes)
	{
		GameObject[] createdObjs = new GameObject[foodTypes.Length];

		//1:引数分食材を生成
		GameObject[] generatePlaces = GeneratePlaces.Shuffle();
		for(int i = 0; i < foodTypes.Length; i++)
		{
			createdObjs[i] = FoodGenerate(foodTypes[i], generatePlaces[i].transform.position);
			if (IsRush)
			{
				createdObjs[i].GetComponent<Food>().speed *= 2;
			}
		}
		//x軸を元にソート
		Array.Sort(createdObjs, (obj1, obj2) =>
			{
				if (obj1.transform.position.x > obj2.transform.position.x)
				{
					return 1;
				}
				else if(obj1.transform.position.x < obj2.transform.position.x)
				{
					return -1;
				}
				else
				{
					return 0;
				}
			});
		//2:左から順にアニメーションを再生
		StartCoroutine(AnimateFoodCoroutine(createdObjs));
	}

	IEnumerator AnimateFoodCoroutine(GameObject[] objs)
	{
		foreach(GameObject obj in objs)
		{
			obj.GetComponent<Animator>().SetBool("flashing", true);
			yield return new WaitForSeconds(3);
		}
	}

	GameObject FoodGenerate(Cooking.FoodType foodType, Vector3 pos)
	{
		GameObject food;

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

		//▼生成
		return(Instantiate(food, pos, Quaternion.identity));
	}
}

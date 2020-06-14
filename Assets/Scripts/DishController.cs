using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class DishController : MonoBehaviour
{
	Customer currentCustomer;
	FriedFood friedFood;
	[SerializeField] Sprite friedBeef;
	[SerializeField] Sprite friedChicken;
	[SerializeField] Sprite friedPork;
	[SerializeField] Sprite friedShrimp;

	//▼参照パス
	[SerializeField] SpriteRenderer food;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SetData(Customer customer,FriedFood friedFood)
	{
		currentCustomer = customer;
		this.friedFood = friedFood;
		switch (friedFood.FoodType)
		{
			case Cooking.FoodType.beef:
				food.sprite = friedBeef;
				break;
			case Cooking.FoodType.chicken:
				food.sprite = friedChicken;
				break;
			case Cooking.FoodType.pork:
				food.sprite = friedPork;
				break;
			case Cooking.FoodType.shrimp:
				food.sprite = friedShrimp;
				break;
		}
	}

	public void GiveFoodToCustomer()
	{
		currentCustomer.CustomerReact(friedFood);
	}

	public void DesTroy()
	{
		Destroy(gameObject);
	}
}

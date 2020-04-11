using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class Oil : MonoBehaviour
{
	public Cooking.OilStatus oilStatus;

	public delegate void CompletedFriedFoodDelegate(FriedFood friedFood);
	public CompletedFriedFoodDelegate completedFriedFoodDelegate;

    // Start is called before the first frame update
    void Start()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void DoTargetAmime(bool isTarget)
	{
		if (oilStatus != Cooking.OilStatus.trash)
		{
			if (isTarget)
			{
				GetComponent<Animator>().SetBool("target", true);
			}
			else
			{
				GetComponent<Animator>().SetBool("target", false);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "food")
		{
			Food food;
			food = collision.gameObject.GetComponent<Food>();
			FriedFood friedFood;
			friedFood = food.DoFry(oilStatus);
			completedFriedFoodDelegate(friedFood);
			Destroy(collision.gameObject);
		}
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
	public Oil.CompletedFriedFoodDelegate CompletedFriedFoodAction;

	[SerializeField] GameObject HighOil;
	[SerializeField] GameObject ModerateOil;
	[SerializeField] GameObject LowOil;

	GameObject createdOil;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "food")
		{
			CompletedFriedFoodAction(null);
			Destroy(collision.gameObject);
		}
	}

	public void ChangeOil(bool isOil)
	{
		if (isOil)
		{
			gameObject.SetActive(false);

			int rand = Random.Range(0, 3);
			switch (rand)
			{
				case 0:
					createdOil = Instantiate(HighOil, transform.position, Quaternion.identity);
					break;
				case 1:
					createdOil = Instantiate(ModerateOil, transform.position, Quaternion.identity);
					break;
				case 2:
					createdOil = Instantiate(LowOil, transform.position, Quaternion.identity);
					break;
			}
			//●めっちゃ微妙な処理●
			createdOil.GetComponent<Oil>().CompletedFriedFoodAction = CompletedFriedFoodAction;
		}
		else
		{
			Destroy(createdOil);
			gameObject.SetActive(true);
		}
	}
}

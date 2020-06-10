using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class Oil : MonoBehaviour
{
	public Cooking.OilTemp OilTemp;

	public delegate void CompletedFriedFoodDelegate(FriedFood friedFood);
	public CompletedFriedFoodDelegate CompletedFriedFoodAction;

	[SerializeField] GameObject outline;

	AudioSource audioSource;
	public AudioClip FrySound;

    // Start is called before the first frame update
    void Start()
    {
		audioSource = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SetOutline(bool isActive)
	{
		outline.SetActive(isActive);
	}

	public void DoTargetAmime(bool isTarget)
	{
		GetComponent<Animator>().SetBool("target", isTarget);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "food")
		{
			Food food;
			food = collision.gameObject.GetComponent<Food>();
			food.IsFall = false;
			food.FriedFoodAction = () =>
			{
				FriedFood friedFood;
				friedFood = food.DoFry(OilTemp);
				CompletedFriedFoodAction(friedFood);
				Destroy(collision.gameObject);
			};

			collision.gameObject.GetComponent<Animator>().SetTrigger("fry");
			audioSource.PlayOneShot(FrySound);
		}
	}
}

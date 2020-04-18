using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalLine : MonoBehaviour
{
	[SerializeField] GameObject OnObj;
	[SerializeField] GameObject OffObj;
	float time = 0;
	bool foodStay = false;
	public delegate void PlusRemainLinesDelegate();
	public PlusRemainLinesDelegate plusRemainLinesDelegate;
	public delegate void MinusRemainLinesDelegate();
	public MinusRemainLinesDelegate minusRemainLinesDelegate;
	public delegate bool IsDrawLineDelegate();
	public IsDrawLineDelegate isDrawLineDelegate;

	void Update()
	{
		/*if (OnObj.activeSelf)
		{
			time += Time.deltaTime;
			if (time > 10 && !foodStay)
			{
				time = 0;
				SetOnObjActivation(false);
			}
		}*/
	}

	public void SetOnObjActivation(bool isActive)
	{
		//線を引く
		if (isActive)
		{
			if (isDrawLineDelegate())
			{
				OnObj.SetActive(isActive);
				minusRemainLinesDelegate();
			}
		}
		//線を消す
		else
		{
			OnObj.SetActive(isActive);
			plusRemainLinesDelegate();
		}
	}

	public bool IsOnObjActive()
	{
		return OnObj.activeSelf;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "food")
		{
			foodStay = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "food")
		{
			foodStay = false;
		}
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalLine : MonoBehaviour
{
	[SerializeField] GameObject OnObj;
	[SerializeField] GameObject OffObj;
	float time = 0;
	bool foodStay = false;
	public delegate void PlusRemainLinesDelegate();
	public PlusRemainLinesDelegate PlusRemainLinesAction;
	public delegate void MinusRemainLinesDelegate();
	public MinusRemainLinesDelegate MinusRemainLinesAction;
	public delegate bool IsDrawLineDelegate();
	public IsDrawLineDelegate IsDrawLineAction;
	AudioSource audioSource;
	public AudioClip DrawSound;

	void Awake()
	{
		audioSource = GetComponent<AudioSource>();	
	}

	void Update()
	{

	}

	public void SetOnObjActivation()
	{
		SetOnObjActivation(!OnObj.activeSelf);
	}

	public void SetOnObjActivation(bool isActive)
	{
		//線を引く
		if (isActive)
		{
			if (IsDrawLineAction())
			{
				OnObj.SetActive(isActive);
				MinusRemainLinesAction();

				audioSource.PlayOneShot(DrawSound);
			}
		}
		//線を消す
		else
		{
			if (!foodStay)
			{
				OnObj.SetActive(isActive);
				PlusRemainLinesAction();
			}
		}
	}

	/*public bool IsOnObjActive()
	{
		return OnObj.activeSelf;
	}*/

	private void OnTriggerStay2D(Collider2D collision)
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

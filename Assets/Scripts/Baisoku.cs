using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Baisoku : MonoBehaviour
{
	[SerializeField] Sprite nomal;
	[SerializeField] Sprite baisoku;

	Image image;

	void Awake()
	{
		image = GetComponent<Image>();
	}

	bool isBaisoku = false;
	public bool IsBaisoku
	{
		get { return isBaisoku; }

		set
		{
			isBaisoku = value;

			if (isBaisoku)
			{
				image.sprite = baisoku;
				Time.timeScale = 2;
			}
			else
			{
				image.sprite = nomal;
				Time.timeScale = 1;
			}
		}
	}

	public void ChangeGameSpeed()
	{
		IsBaisoku = !IsBaisoku;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	[SerializeField] Text scoreText;
	[SerializeField] Text remainLinesText;
	[SerializeField] Text comboText;
	[SerializeField] Image rushGageImage;

	const int MaxDrawLineNum = 3;

	public delegate void RushDelegate(bool isRush);
	public RushDelegate rushDelegate;
	bool isRush = false;
	public bool IsRush
	{
		get { return isRush; }

		set
		{
			isRush = value;
			rushDelegate(value);
		}
	}
	const float RushTime = 20;
	const int MaxRushGage = 1;
	int rushGage;
	public int RushGage
	{
		get { return rushGage; }

		set
		{
			if (IsRush)
			{
				return;
			}

			if (value >= MaxRushGage)
			{
				rushGage = MaxRushGage;
			}
			else
			{
				rushGage = value;
			}

			if (rushGage >= MaxRushGage)
			{
				StartCoroutine(RushTimeCoroutine());
			}
		}
	}
	int score;
	public int Score
	{
		get { return score; }

		set
		{
			score = value;
			scoreText.text = "SCORE:" + Score.ToString();
		}
	}

	int remainLines = MaxDrawLineNum;
	public int RemainLines
	{
		get { return remainLines; }

		set
		{
			if (value > MaxDrawLineNum)
			{
				remainLines = MaxDrawLineNum;
			}
			else if (value < 0)
			{
				remainLines = 0;
			}
			else
			{
				remainLines = value;
			}
			remainLinesText.text = "残り本数" + RemainLines.ToString() + "/" + MaxDrawLineNum.ToString();
		}
	}

	int combo = 0;
	public int Combo
	{
		get { return combo; }

		set
		{
			combo = value;
			if (combo == 0)
			{
				comboText.gameObject.SetActive(false);
			}
			else
			{
				comboText.gameObject.SetActive(true);
				comboText.text = combo.ToString() + "コンボ";
			}
		}
	}
	int maxCombo;

	IEnumerator RushTimeCoroutine()
	{
		float time = 0;
		IsRush = true;

		while (time < RushTime)
		{
			time += Time.deltaTime;
			rushGageImage.fillAmount = (RushTime - time) / RushTime;
			yield return null;
		}

		rushGage = 0;
		rushGageImage.fillAmount = 1;
		IsRush = false;
	}
}

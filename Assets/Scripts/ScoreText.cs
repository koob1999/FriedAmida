using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
	[SerializeField] Text scoreObj;
	[SerializeField] Text comboScoreObj;
	[SerializeField] Text totalScoreObj;

	public void SetText(string scoreText, string comboScoreText, string totalScoreText)
	{
		scoreObj.text = scoreText;
		comboScoreObj.text = comboScoreText;
		totalScoreObj.text = totalScoreText;
	}
}

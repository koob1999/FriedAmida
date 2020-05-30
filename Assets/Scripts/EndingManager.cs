using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
	[SerializeField] Text noonScore;
	[SerializeField] Text eveningScore;
	[SerializeField] Text midnightScore;

	void Start()
	{
		noonScore.text = "昼ステージ：" + ScoreData.NoonScore.ToString();
		eveningScore.text = "夕方ステージ：" + ScoreData.EveningScore.ToString();
		midnightScore.text = "夜ステージ：" + ScoreData.MidnightScore.ToString();
	}
}

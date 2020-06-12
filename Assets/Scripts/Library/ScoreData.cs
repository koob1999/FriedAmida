using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScoreData 
{
	public static int NoonScore = 0;
	public static int EveningScore = 0;
	public static int MidnightScore = 0;

	public static void ResetScore()
	{
		NoonScore = 0;
		EveningScore = 0;
		MidnightScore = 0;
	}

	public static void SaveScore(int score)
	{
		switch (SceneManager.GetActiveScene().name)
		{
			case "Noon":
				NoonScore = score;
				break;
			case "Evening":
				EveningScore = score;
				break;
			case "Midnight":
				MidnightScore = score;
				break;
			default:
				break;
		}
	}
}

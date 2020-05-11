using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class StageChange
{
	public static void MoveNextStage()
	{
		string stageName = SceneManager.GetActiveScene().name;
		string nextStageName;

		switch (stageName)
		{
			case "Noon":
				nextStageName = "Evening";
				break;
			case "Evening":
				nextStageName = "Night";
				break;
			default:
				nextStageName = "Noon";
				break;
		}

		SceneManager.LoadScene(nextStageName);
	}
}

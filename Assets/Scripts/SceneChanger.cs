using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger
{
	public void MoveNextStage()
	{
		switch (SceneManager.GetActiveScene().name)
		{
			case "Noon":
				SceneManager.LoadScene("Evening");
				break;
			case "Evening":
				SceneManager.LoadScene("Midnight");
				break;
			default:
				SceneManager.LoadScene("Noon");
				break;
		}
	}
}

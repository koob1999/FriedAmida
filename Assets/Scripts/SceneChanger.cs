﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
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
			case "Midnight":
				SceneManager.LoadScene("Ending");
				break;
			default:
				SceneManager.LoadScene("Noon");
				break;
		}
	}

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	public void ReLoadCurrentScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}

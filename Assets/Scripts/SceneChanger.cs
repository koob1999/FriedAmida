using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	AudioSource audioSource;
	public AudioClip StartSound;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();	
	}

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

	public void GameStart()
	{
		StartCoroutine(GameStartCoroutine());
	}

	IEnumerator GameStartCoroutine()
	{
		audioSource.PlayOneShot(StartSound);

		float time = 0;

		while (time < 2)
		{
			time += Time.deltaTime;
			yield return null;
		}

		SceneManager.LoadScene("Noon");
	}
}

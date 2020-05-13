using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameQuit : MonoBehaviour
{
	public void Quit()
	{
		#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
		#elif UNITY_STANDALONE
			Application.Quit();
		#endif
	}
}

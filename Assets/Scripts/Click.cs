using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour,IPointerClickHandler
{
	[SerializeField] UnityEvent unityEvent;

	public void OnButtonPressed()
	{

	}

	public void OnPointerClick(PointerEventData pointerData)
	{
		unityEvent.Invoke();
		Debug.Log("クリックした");
	}
}

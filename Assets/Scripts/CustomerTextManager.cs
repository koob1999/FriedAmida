using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerTextManager : MonoBehaviour
{
	[SerializeField] Text customerText;
	[SerializeField] Animator anim;

	public void SetText(string text)
	{
		anim.SetTrigger("text");
		customerText.text = text;
	}
}

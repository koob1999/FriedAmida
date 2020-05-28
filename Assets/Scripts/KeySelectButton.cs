using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeySelectButton : MonoBehaviour
{
	Selectable sel;

	// Start is called before the first frame update
	void Start()
	{
		sel = GetComponent<Selectable>();
		sel.Select();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void ButtoneSelect()
	{
		sel.Select();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGage : MonoBehaviour
{
	[SerializeField] GameObject[] blockes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void DisplayBlockes(int remainNum, int maxNum)
	{
		for(int i = 0; i < maxNum; i++)
		{
			blockes[i].SetActive(remainNum > i);
		}
	}
}

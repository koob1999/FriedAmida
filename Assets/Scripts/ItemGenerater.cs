using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerater : MonoBehaviour
{
	//▼生成するアイテム
	[SerializeField] GameObject egg;
	[SerializeField] GameObject komugiko;
	[SerializeField] GameObject panko;
	[SerializeField] GameObject badItem;
	[SerializeField] GameObject rushItem;

	[System.NonSerialized] public GameObject[,] GeneratePlaces;
	GameObject[,] generateBoxes;//アイテムデータの格納場所

	[System.NonSerialized] public bool isRush = false;

    // Start is called before the first frame update
    void Start()
    {
		generateBoxes = new GameObject[GeneratePlaces.GetLength(0),GeneratePlaces.GetLength(1)];
	}

    // Update is called once per frame
    void Update()
    {

    }

	//●関数名が微妙●
	public void InitializeItems(int eggNum, int komugikoNum, int pankoNum, int badItemNum)
	{
		//▼マップ上のアイテムを全破棄
		for(int i = 0; i < generateBoxes.GetLength(0); i++)
		{
			for(int j = 0; j < generateBoxes.GetLength(1); j++)
			{
				Destroy(generateBoxes[i, j]);
			}
		}

		//▼アイテム生成
		if (isRush)
		{
			ItemGenerate(rushItem, eggNum + komugikoNum + pankoNum + badItemNum);
		}
		else
		{
			ItemGenerate(egg, eggNum);
			ItemGenerate(komugiko, komugikoNum);
			ItemGenerate(panko, pankoNum);
			ItemGenerate(badItem, badItemNum);
		}
	}

	void ItemGenerate(GameObject createObj, int createNum)
	{
		//三重ループになっちゃった、許ちてくだちゃい
		for (int i = 0; i < createNum; i++)
		{
			//▼生成場所の決定
			List<int> xEmptyDatas = new List<int>();
			List<int> yEmptyDatas = new List<int>();

			for (int j = 0; j < generateBoxes.GetLength(0); j++)
			{
				for (int k = 0; k < generateBoxes.GetLength(1); k++)
				{
					if (generateBoxes[j, k] == null)
					{
						xEmptyDatas.Add(j);
						yEmptyDatas.Add(k);
					}
				}
			}

			if (xEmptyDatas.Count != 0)
			{
				int rand = Random.Range(0, xEmptyDatas.Count);
				int xGenerateNum = xEmptyDatas[rand];
				int yGenerateNum = yEmptyDatas[rand];

				//▼生成
				generateBoxes[xGenerateNum, yGenerateNum] = Instantiate(createObj, GeneratePlaces[xGenerateNum, yGenerateNum].transform.position, Quaternion.identity);
			}
		}
	}
}

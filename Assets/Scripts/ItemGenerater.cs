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

	[SerializeField] GameObject[] generatePlaces;//アイテム生成場所
	GameObject[] generateBoxes;//アイテムデータの格納場所
	[SerializeField] float geneateSpan = 3;

    // Start is called before the first frame update
    void Start()
    {
		generateBoxes = new GameObject[generatePlaces.Length];
	}

    // Update is called once per frame
    void Update()
    {

    }

	//！関数名が微妙！
	public void InitializeItems(int eggNum, int komugikoNum, int pankoNum, int badItemNum)
	{
		//▼マップ上のアイテムを全破棄
		foreach(GameObject obj in generateBoxes)
		{
			if (obj != null)
			{
				Destroy(obj);
			}
		}

		//▼アイテム生成
		ItemGenerate(egg, eggNum);
		ItemGenerate(komugiko, komugikoNum);
		ItemGenerate(panko, pankoNum);
		ItemGenerate(badItem, badItemNum);
	}

	void ItemGenerate(GameObject createObj, int createNum)
	{
		for (int j = 0; j < createNum; j++)
		{
			//▼生成場所の決定
			List<int> emptyDatas = new List<int>();

			int i = 0;
			foreach (GameObject generateBox in generateBoxes)
			{
				if (generateBox == null)
				{
					emptyDatas.Add(i);
				}
				i++;
			}

			if (emptyDatas.Count != 0)
			{
				int rand = Random.Range(0, emptyDatas.Count);
				int generateNum = emptyDatas[rand];

				//▼生成
				generateBoxes[generateNum] = Instantiate(createObj, generatePlaces[generateNum].transform.position, Quaternion.identity);
			}
		}
	}
}

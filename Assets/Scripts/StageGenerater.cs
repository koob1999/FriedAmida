using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amida;

public class StageGenerater : MonoBehaviour
{
	enum UnderBoxType{
		highTempOil,
		moderateTempOil,
		lowTempOil,
		trash
	}

	//プレハブ
	//線
	[SerializeField] GameObject HorizontalLine;
	[SerializeField] GameObject VerticalLine;
	//油
	[SerializeField] GameObject HighOil;
	[SerializeField] GameObject ModerateOil;
	[SerializeField] GameObject LowOil;
	[SerializeField] GameObject TrashBox;
	//変数名に難あり？
	//ステージデータ
	[SerializeField] UnderBoxType[] underBoxTypes;
	[SerializeField, Range(3, 8)] int HorizontalLinesNum;
	float maxXPos = 4;
	float minXPos = -7;
	float maxYPos = 4.5f;
	float minYPos = -3;
	//参照パス
	[SerializeField] LineCursol lineCursol;
	[SerializeField] GameManager gameManager;
	[SerializeField] FoodGenerater foodGenerater;
	[SerializeField] ItemGenerater itemGenerater;

	private void Awake()
	{
		GenerateStage();
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void GenerateStage()
	{
		float xLength = maxXPos - minXPos;
		float xInterval = 2;
		float yLength = maxYPos - minYPos;
		float yInterval=yLength/(HorizontalLinesNum + 1);

		//▼縦線の生成
		for(int i = 0; i < underBoxTypes.Length; i++)
		{
			Instantiate(VerticalLine, new Vector3(minXPos + (xInterval * i), 0.5f, 0), Quaternion.identity);
		}

		//▼横線の生成
		HorizontalLine[,] amidaLines = new HorizontalLine[underBoxTypes.Length - 1, HorizontalLinesNum];

		for(int i = 0; i < underBoxTypes.Length - 1; i++)
		{
			for(int j = 0; j < HorizontalLinesNum; j++)
			{
				amidaLines[i, j] = Instantiate(HorizontalLine,
												new Vector3(minXPos + (xInterval * i) + 1, maxYPos - ((yInterval * j) + (i % 2 * yInterval / 2)) - 1, 0),
												Quaternion.identity).GetComponent<HorizontalLine>();

				amidaLines[i, j].minusRemainLinesDelegate = () => { gameManager.RemainLines--; };
				amidaLines[i, j].plusRemainLinesDelegate = () => { gameManager.RemainLines++; };
				amidaLines[i, j].isDrawLineDelegate = () => { return gameManager.RemainLines > 0; };

			}
		}

		lineCursol.AmidaLines = amidaLines;
		gameManager.AmidaLines = amidaLines;

		//▼油の生成
		List<Oil> oils = new List<Oil>();
		List<Trash> trashes = new List<Trash>();
		for (int i = 0; i < underBoxTypes.Length; i++)
		{
			switch (underBoxTypes[i])
			{
				case UnderBoxType.highTempOil:
					oils.Add(Instantiate(HighOil, new Vector3(minXPos + (xInterval * i), minYPos - 1, 0), Quaternion.identity).GetComponent<Oil>());
					break;
				case UnderBoxType.moderateTempOil:
					oils.Add(Instantiate(ModerateOil, new Vector3(minXPos + (xInterval * i), minYPos - 1, 0), Quaternion.identity).GetComponent<Oil>());
					break;
				case UnderBoxType.lowTempOil:
					oils.Add(Instantiate(LowOil, new Vector3(minXPos + (xInterval * i), minYPos - 1, 0), Quaternion.identity).GetComponent<Oil>());
					break;
				case UnderBoxType.trash:
					trashes.Add(Instantiate(TrashBox, new Vector3(minXPos + (xInterval * i), minYPos - 1, 0), Quaternion.identity).GetComponent<Trash>());
					break;
			}
		}

		gameManager.Oils = oils;
		gameManager.Trashes = trashes;

		//▼食材生成場所の生成
		//ここGameObjectよりVector３で渡したほうが良い
		GameObject[] foodGeneratePlaces = new GameObject[underBoxTypes.Length];
		for (int i = 0; i < underBoxTypes.Length; i++)
		{
			foodGeneratePlaces[i] = Instantiate(new GameObject("foodGeneratePlace"), new Vector3(minXPos + (xInterval * i), maxYPos, 0), Quaternion.identity);
		}

		foodGenerater.GeneratePlaces = foodGeneratePlaces;

		//▼アイテム生成場所の生成
		//ここGameObjectよりVector３で渡したほうが良い
		GameObject[,] itemGeneraterPlaces = new GameObject[underBoxTypes.Length, HorizontalLinesNum - 2];
		for (int i = 0; i < underBoxTypes.Length; i++)
		{
			for (int j = 0; j < HorizontalLinesNum - 2; j++)
			{
				itemGeneraterPlaces[i, j] = Instantiate(new GameObject("itemGeneraterPlace"), new Vector3(minXPos + (xInterval * i), maxYPos - (yInterval * j) - 1, 0), Quaternion.identity);
			}
		}

		itemGenerater.GeneratePlaces = itemGeneraterPlaces;
	}

	public void TrashToOil(Trash trash)
	{
		int rand = Random.Range(0, 3);

		switch (rand)
		{
			case 0:
				Instantiate(HighOil, trash.gameObject.transform.position, Quaternion.identity);
				break;
			case 1:
				Instantiate(ModerateOil, trash.gameObject.transform.position, Quaternion.identity);
				break;
			case 2:
				Instantiate(LowOil, trash.gameObject.transform.position, Quaternion.identity);
				break;
		}

		Destroy(trash.gameObject);
	}
}

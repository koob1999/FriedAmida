﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
	enum OilTemp
	{
		high,
		moderate,
		low
	}

	int xDirction = 0;
	float speed = 0.02f;
	[SerializeField] int clearKomugikoNum;
	[SerializeField] int clearEggNum;
	[SerializeField] int clearPankoNum;
	[SerializeField] int calorieNum;
	[SerializeField] OilTemp oilTemp;
	int komugikoCount = 0;
	int eggCount = 0;
	int pankoCount = 0;
	int badItemCount = 0;

	public int XDirection
	{
		set
		{
			if (value <= -1)
			{
				xDirction = -1;
			}
			else if (value >= 1)
			{
				xDirction = 1;
			}
			else
			{
				xDirction = 0;
			}
		}

		get
		{
			return xDirction;
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (xDirction == 0)
		{
			transform.Translate(0, -1 * speed, 0);
		}
		else
		{
			transform.Translate(xDirction * speed, 0, 0);
		}
    }

	private void OnTriggerEnter2D(Collider2D col)
	{
		string tag = col.gameObject.tag;

		switch (tag)
		{
			//あみだの横線
			case "right":
				transform.position = col.gameObject.transform.position;
				ChangeDirection(-1);
				break;
			case "left":
				transform.position = col.gameObject.transform.position;
				ChangeDirection(1);
				break;

			//油
			case "box":
				Destroy(gameObject);
				break;
			case "lowOil":
				DoFry(OilTemp.low);
				break;
			case "moderateOil":
				DoFry(OilTemp.moderate);
				break;
			case "highOil":
				DoFry(OilTemp.high);
				break;

			//アイテム（パン粉等）
			case "egg":
				eggCount++;
				break;
			case "komugiko":
				komugikoCount++;
				break;
			case "panko":
				pankoCount++;
				break;
			case "badItem":
				badItemCount++;
				break;
		}
	}

	void ChangeDirection(int x)
	{
		XDirection = XDirection != 0 ? 0 : x;
	}

	void DoFry(OilTemp oil)
	{
		//マイナスアイテムを取得していれば腐り状態
		if (badItemCount >= 1)
		{
			Debug.Log("bad");
		}
		else
		{
			//油の温度が適正、材料全てがノルマ以上取得していれば揚げ成功
			if (oil == oilTemp && eggCount >= clearEggNum && komugikoCount >= clearKomugikoNum && pankoCount >= clearPankoNum)
			{
				Debug.Log("good");
			}
			//材料を全く取得していなければ素揚げ、生？
			else if (eggCount == 0 && komugikoCount == 0 && pankoCount == 0)
			{
				Debug.Log("row");
			}
			//それ以外なら揚げ失敗
			else
			{
				Debug.Log("usually");
			}
		}

		Destroy(gameObject);
	}
}

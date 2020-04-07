using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
	enum Enemy
	{
		nomal,
	}


	[SerializeField] ItemGenerater itemGenerater;

	//▼敵関連
	[SerializeField] Enemy[] enemies;//出現する敵の設定用
	[SerializeField] GameObject nomalMob;//敵のPrefab設定用
	[SerializeField] GameObject enemyGeneratePlace;//敵の生成場所設定場所

	int presentEnemyNum;//現在現れている敵が何番目か

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void WaveStart()
	{
		presentEnemyNum = 0;
	}

	//●返り値なしの関数にして変数に格納してもいい気がする●
	//次の敵を返り値に持つ
	public GameObject NextBattleStart()
	{
		//▼次の敵を設定
		GameObject nextEnemy = SelectEnemy();
		Customer customer = nextEnemy.GetComponent<Customer>();

		//▼敵に応じたアイテム生成
		//●ここ可読性ゴミでは？ここだけちゃうけど●
		if (itemGenerater != null && customer != null)
		{
			Debug.Log("a");
			itemGenerater.InitializeItems(customer.AppearItemNum.egg, customer.AppearItemNum.komugiko, customer.AppearItemNum.panko, customer.AppearItemNum.badItem);
		}

		//▼敵生成
		GameObject enemy = Instantiate(nextEnemy, enemyGeneratePlace.transform.position, Quaternion.identity);

		//▼出現する敵の順番を1進める
		presentEnemyNum++;

		return enemy;
	}


	GameObject SelectEnemy()
	{
		Enemy enemy = enemies[presentEnemyNum];

		switch (enemy)
		{
			case Enemy.nomal:
				return nomalMob;
			default:
				return nomalMob;
		}
	}

	public bool IsLastEnemy()
	{
		return presentEnemyNum == enemies.Length - 1;
	}
}

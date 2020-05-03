using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
	enum Enemy
	{
		nomal,
		calorieMob,
		strongMob,
	}


	[SerializeField] Text customerNumText;

	//▼敵関連
	[SerializeField] Enemy[] enemies;//出現する敵の設定用
	[SerializeField] GameObject nomalMob;//敵のPrefab設定用
	[SerializeField] GameObject calorieMob;
	[SerializeField] GameObject enemyGeneratePlace;//敵の生成場所設定場所
	[SerializeField] StageGenerater stageGenerater;
	[System.NonSerialized] public HorizontalLine[,] AmidaLines;

	int presentEnemyNum;//現在現れている敵が何番目か
	public int PresentEnemyNum
	{
		get { return this.presentEnemyNum; }

		set
		{
			presentEnemyNum = value;
			customerNumText.text = (presentEnemyNum).ToString() + "人目";
		}
	}

    // Start is called before the first frame update
    void Start()
    {
		stageGenerater.GenerateStage(this);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	void WaveStart()
	{
		PresentEnemyNum = 0;
	}

	//●返り値なしの関数にして変数に格納してもいい気がする●
	//次の敵を返り値に持つ
	public GameObject NextBattleStart()
	{
		//▼次の敵を設定
		GameObject nextEnemy = SelectEnemy();
		Customer customer = nextEnemy.GetComponent<Customer>();

		//▼敵生成
		GameObject enemy = Instantiate(nextEnemy, enemyGeneratePlace.transform.position, Quaternion.identity);

		//▼出現する敵の順番を1進める
		PresentEnemyNum++;

		return enemy;
	}


	GameObject SelectEnemy()
	{
		Enemy enemy = enemies[PresentEnemyNum];

		switch (enemy)
		{
			case Enemy.nomal:
				return nomalMob;
			case Enemy.calorieMob:
				return calorieMob;
			default:
				return nomalMob;
		}
	}

	public bool IsLastEnemy()
	{
		return PresentEnemyNum == enemies.Length - 1;
	}

	public void ResetAmidaLines()
	{
		//あみだのリセット
		for (int i = 0; i < AmidaLines.GetLength(0); i++)
		{
			for (int j = 0; j < AmidaLines.GetLength(1); j++)
			{
				AmidaLines[i, j].SetOnObjActivation(false);
			}
		}
	}
}

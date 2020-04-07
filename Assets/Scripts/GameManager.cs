using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	//[SerializeField] GameObject stageManager;
	[SerializeField] StageManager stageManager;
	[SerializeField] FoodGenerater foodGenerater;

	GameObject currentEnemyObj;//現在戦闘中の敵
	Customer currentCustomer;//●変数名微妙●
	public FriedFood MadeFriedFood;

	//●Playerクラスに分ける可能性あり●
	int rushGage;
	int score;

    // Start is called before the first frame update
    void Start()
    {
		Invoke("GameStart", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void GameStart()
	{
		StartCoroutine(gameStart());
	}

	IEnumerator gameStart()
	{
		//消したい
		//敵が全員倒されるまでループ
		while (true)
		{
			//１：次の戦闘準備
			currentEnemyObj = stageManager.NextBattleStart();
			currentCustomer = currentEnemyObj.GetComponent<Customer>();

			//敵が倒されるまでループ
			while (true)
			{
				MadeFriedFood = null;

				//2：敵に応じた食材生成
				foodGenerater.FoodsGenerate(currentCustomer.SynchroFoodNum);

				//消したい
				//3：揚げ物を受け取るまでループ
				while (MadeFriedFood == null)
				{
					yield return null;
				}

				//4：揚げ物を敵に渡す
				//5：敵による揚げ物評価(スコア処理未実装）
				currentCustomer.DoReaction(MadeFriedFood);

				//客のリアクションを見せるため1秒止める
				yield return new WaitForSeconds(1);

				//6：敵が倒れればループを抜ける
				if (currentCustomer.IsClear)
				{
					Destroy(currentEnemyObj);
					break;
				}
			}

			//７：現在の敵が最後の敵であればループを抜ける
			if (stageManager.IsLastEnemy())
			{
				break;
			}
		}

		//８：スコア表示等

		//9：次のステージへ
		Debug.Log("clear");
	}
}

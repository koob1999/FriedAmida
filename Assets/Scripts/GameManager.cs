using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Amida;
using System;

public class GameManager : MonoBehaviour
{
	//参照パス
	[SerializeField] StageManager stageManager;
	[SerializeField] FoodGenerater foodGenerater;
	[SerializeField] ItemGenerater itemGenerater;
	[SerializeField] LineCursol lineCursol;
	[SerializeField] Text scoreText;
	[SerializeField] Text remainLinesText;
	[SerializeField] Text comboText;
	[SerializeField] Image rushGageImage;
	[SerializeField] Image calorieGageImage;
	[SerializeField] Text minuteText;
	[SerializeField] Text secandText;

	[SerializeField] GameObject scoreTextObj;
	GameObject currentEnemyObj;//現在戦闘中の敵
	Customer currentCustomer;//●変数名微妙●

	[SerializeField] int limitTime;
	bool IsTimeOver => limitTime <= 0;

	bool isClear = false;
	bool IsClear
	{
		get { return isClear; }

		set
		{
			isClear = value;
			lineCursol.IsClear = value;
		}
	}

	bool isRush = false;
	bool IsRush
	{
		get { return isRush; }

		set
		{
			isRush = value;
			itemGenerater.IsRush = isRush;
			stageManager.ChangeTrashToOil(isRush);
			//BGM変更
			//ゲーム速度少し上昇
			//制限時間停止
		}
	}

	const float RushTime = 20;
	const int MaxRushGage = 1;
	int rushGage;
	public int RushGage
	{
		get { return rushGage; }

		set
		{
			if (IsRush)
			{
				return;
			}

			if (value >= MaxRushGage)
			{
				rushGage = MaxRushGage;
			}
			else
			{
				rushGage = value;
			}

			if (rushGage >= MaxRushGage)
			{
				Debug.Log("RushTime");
				StartCoroutine(RushTimeCoroutine());
			}
		}
	}
	int score;
	public int Score
	{
		get { return score; }

		set
		{
			score = value;
			scoreText.text = "SCORE:" + Score.ToString();
		}
	}

	int combo = 0;
	public int Combo
	{
		get { return combo; }

		set
		{
			combo = value;
			if (combo == 0)
			{
				comboText.gameObject.SetActive(false);
			}
			else
			{
				comboText.gameObject.SetActive(true);
				comboText.text = combo.ToString() + "コンボ";
			}
		}
	}
	int maxCombo;

	// Start is called before the first frame update
	void Start()
    {
		Time.timeScale = 1;

		stageManager.SetCompletedActionToOil(CompletedFriedFood);

		stageManager.UpdateRemainLinesText = text =>
		{
			remainLinesText.text = text;
		};

		StartCoroutine(CountTime());

		Invoke("GameStart", 0.5f);
	}

    // Update is called once per frame
    void Update()
    {
		if (IsClear)
		{
			return;
		}

		//倍速処理
		if (Input.GetKeyDown(KeyCode.X))
		{
			Time.timeScale = 2;
		}
		if (Input.GetKeyUp(KeyCode.X))
		{
			Time.timeScale = 1;
		}
    }

	void GameStart()
	{
		//１：次の戦闘準備
		AppearNextEnemy();

		//2：敵に応じた行動
		currentCustomer.DoAction();
	}

	//敵生成
	void AppearNextEnemy()
	{
		currentEnemyObj = stageManager.NextBattleStart();
		currentCustomer = currentEnemyObj.GetComponent<Customer>();

		currentCustomer.FoodGenerater = foodGenerater;
		currentCustomer.ItemGenerater = itemGenerater;
		currentCustomer.stageManager = stageManager;
		currentCustomer.KilledCustomerAction = KilledCustomer;
		currentCustomer.AmidaResetAction = stageManager.ResetAmidaLines;
		//客がカロリーゲージを持たない場合はゲージは非表示にする
		calorieGageImage.gameObject.transform.parent.gameObject.SetActive(currentCustomer.HasClalorie);
		calorieGageImage.fillAmount = 1;
		currentCustomer.CalorieGageAction = (int clearCalorie, int currentCalorie) =>
		{
			calorieGageImage.fillAmount = (float)currentCalorie / clearCalorie;
		};
		currentCustomer.AddPointAction = (int rushGage, int score) =>
		{
			Score += IsRush ? (int)(score * 1.5f) : score;
			RushGage += rushGage;
			Debug.Log(score);
		};
	}

	void KilledCustomer()
	{
		Destroy(currentEnemyObj);

		if (stageManager.IsLastEnemy())
		{
			ClearGame();
			return;
		}

		AppearNextEnemy();
		currentCustomer.DoAction();
	}

	void CompletedFriedFood(FriedFood friedFood)
	{
		//ゲームオーバーの時はここで止まる
		if (IsTimeOver)
		{
			return;
		}

		//コンボ処理
		if (friedFood != null && friedFood.FriedFoodReview == Cooking.FriedFoodReview.good)
		{
			Combo++;
		}
		else
		{
			maxCombo = Math.Max(maxCombo, Combo);
			Combo = 0;
		}

		//揚げ物ができていないなら評価せず再行動
		/*if (friedFood == null)
		{
			currentCustomer.DoAction();
			return;
		}*/

		//●ここら辺もっときれいにできる気がする●
		currentCustomer.CustomerReact(friedFood);
	}

	void ClearGame()
	{
		IsClear = true;
		//８：スコア表示等
		GameObject obj = Instantiate(scoreTextObj, new Vector3(0, 0, 0), Quaternion.identity);
		obj.GetComponent<ScoreText>().SetText(
			Score.ToString(),
			Combo.ToString() + "×100",
			"合計スコア" + (Score + Combo * 100).ToString() + "点！");
	}

	void RushEnd()
	{
		IsRush = false;
	}

	IEnumerator RushTimeCoroutine()
	{
		float time = 0;
		IsRush = true;

		while (time < RushTime)
		{
			time += Time.deltaTime;
			rushGageImage.fillAmount = (RushTime - time) / RushTime;
			yield return null;
		}

		rushGage = 0;
		rushGageImage.fillAmount = 1;
		IsRush = false;
	}

	IEnumerator CountTime()
	{
		DisplayTime();

		while (!IsTimeOver)
		{
			if (IsClear)
			{
				yield break;
			}

			yield return new WaitForSeconds(1);
			limitTime--;
			DisplayTime();
		}
	}

	void DisplayTime()
	{
		int minute = limitTime / 60;
		int secand = limitTime - minute * 60;

		minuteText.text = minute.ToString("D2");
		secandText.text = secand.ToString("D2");
	}
}

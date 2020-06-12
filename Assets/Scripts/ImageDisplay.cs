using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageDisplay : MonoBehaviour
{
	Image image;
	[SerializeField] Sprite[] sprites;
	int currentSpriteNum = 0;
	public int CurrentSpriteNum
	{
		get { return currentSpriteNum; }

		set
		{
			if (value <= 0)
			{
				currentSpriteNum = 0;
			}
			else if (value >= sprites.Length - 1)
			{
				currentSpriteNum = sprites.Length - 1;
			}
			else
			{
				currentSpriteNum = value;
			}

			image.sprite = sprites[currentSpriteNum];
		}
	}

    // Start is called before the first frame update
    void Start()
    {
		image = GetComponent<Image>();
    }

	public void MoveSprite(int num)
	{
		Debug.Log(CurrentSpriteNum);
		CurrentSpriteNum += num;
	}

	public void MoveScene(string sceneName)
	{
		if (CurrentSpriteNum != sprites.Length - 1)
		{
			return;
		}

		SceneChanger sceneChanger = new SceneChanger();
		sceneChanger.LoadScene(sceneName);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCursol : MonoBehaviour
{
	class Position
	{
		int x = 0;
		int y = 0;
		GameObject[,] amidaLines;

		public int X
		{
			set
			{
				if (value < 0)
				{
					x = 0;
				}
				else if (value >= amidaLines.GetLength(0) - 1)
				{
					x = amidaLines.GetLength(0) - 1;
				}
				else
				{
					x = value;
				}
			}

			get
			{
				return x;
			}
		}

		public int Y
		{
			set
			{
				if (value < 0)
				{
					y = 0;
				}
				else if (value >= amidaLines.GetLength(1) - 1)
				{
					y = amidaLines.GetLength(1) - 1;
				}
				else
				{
					y = value;
				}
			}

			get
			{
				return y;
			}
		}

		public Position(GameObject[,] amidaLines)
		{
			this.amidaLines = amidaLines;
		}
	}

	public GameObject[,] AmidaLines;

	Position position;

	enum Direction
	{
		Up = 1,
		Down = 2,
		Right = 3,
		Left = 4
	}

    // Start is called before the first frame update
    void Start()
    {
		position = new Position(AmidaLines);
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = AmidaLines[position.X, position.Y].transform.position;

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			MoveCursol(Direction.Up);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			MoveCursol(Direction.Down);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			MoveCursol(Direction.Right);
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			MoveCursol(Direction.Left);
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			DrawHLine();
		}
	}

	void DrawHLine()
	{
		AmidaLines[position.X, position.Y].GetComponent<HorizontalLine>().OnObjActivation();
	}

	void MoveCursol(Direction dir)
	{
		switch (dir)
		{
			case Direction.Up:
				position.Y--;
				break;
			case Direction.Down:
				position.Y++;
				break;
			case Direction.Right:
				position.X++;
				break;
			case Direction.Left:
				position.X--;
				break;
		}
	}
}

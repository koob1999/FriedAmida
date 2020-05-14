using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayExtensions
{

	//配列をシャッフルする
	public static void Shuffle<T>(this T[] types)
	{
		for (int i = 0; i < types.Length * 10; i++) 
		{
			int rand1 = Random.Range(0, types.Length);
			int rand2 = Random.Range(0, types.Length);

			T temp = types[rand1];
			types[rand1] = types[rand2];
			types[rand2] = temp;
		}
	}
}

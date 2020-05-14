using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public static class ArrayExtensions
{

	//配列をシャッフルする
	public static void Shuffle<T>(this T[] types)
	{
		T[] types2 = types.OrderBy(i => Guid.NewGuid()).ToArray();
	
		for(int i=0; i < types.Length; i++)
		{
			types[i] = types2[i];
		}
	}
}

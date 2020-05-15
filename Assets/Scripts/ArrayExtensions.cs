using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public static class ArrayExtensions
{

	//配列をシャッフルする
	public static T[] Shuffle<T>(this T[] types)
	{
		return types.OrderBy(i => Guid.NewGuid()).ToArray();
	}
}

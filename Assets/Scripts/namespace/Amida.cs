using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amida
{
	public class Cooking
	{
		public enum OilTemp
		{
			high,
			moderate,
			low
		}

		public enum FriedFoodReview
		{
			good,
			usually,
			raw,
			bad
		}

		public enum FoodType
		{
			beef,
			chicken,
			pork,
			shrimp
		}
	}

	//●なんかいいクラス名あったら教えて●
	public class MyClass
	{
		/// <summary>
		/// 第二引数分だけ第一引数の配列からランダム、重複なしで値を抽出した配列を返す
		/// </summary>
		/// <typeparam name="Type"></typeparam>
		/// <param name="types"></param>
		/// <param name="num"></param>
		/// <returns></returns>
		public static Type[] GetRandomArray<Type>(Type[] types, int num)
		{
			Type[] returnTypes = new Type[num];

			for (int i = 0, len = types.Length; i < num; i++, len--)
			{
				int rand = Random.Range(0, len);

				returnTypes[i] = types[rand];
				types[rand] = types[len - 1];
			}

			return returnTypes;
		}
	}
}

using System.Collections.Generic;
using UnityEngine;

namespace TestTask.Utility
{
	public static class ListUtility
	{
		public static List<Sprite> Shuffle(List<Sprite> sprites)
		{
			List<Sprite> shuffledSprites = new List<Sprite>(sprites);
			for (int i = 0; i < shuffledSprites.Count; i++)
			{
				Sprite temp = shuffledSprites[i];
				int randomIndex = Random.Range(i, shuffledSprites.Count);
				shuffledSprites[i] = shuffledSprites[randomIndex];
				shuffledSprites[randomIndex] = temp;
			}
			return shuffledSprites;
		}

		public static List<Sprite> ExpandList(List<Sprite> sprites)
		{
			List<Sprite> expandedList = new List<Sprite>(sprites.Count * 2);
			int index = 0;
			for (int i = 0; i < expandedList.Capacity; i++)
			{
				if (index == sprites.Count)
				{
					index = 0;
				}
				expandedList.Add(sprites[index]);
				index++;
			}
			return expandedList;
		}
	}
}
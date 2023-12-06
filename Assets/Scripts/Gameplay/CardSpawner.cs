using System.Collections.Generic;
using UnityEngine;

namespace TestTask.Gameplay
{
	public class CardSpawner : MonoBehaviour
	{
		[SerializeField] private Card _cardPrefab;

		public List<Card> SpawnCards(List<Sprite> faces, Sprite back)
		{
			List<Card> cards = new List<Card>();
			for (int i = 0; i < faces.Count; i++)
			{
				Card card = Instantiate(_cardPrefab, transform);
				card.Init(faces[i], back);
				cards.Add(card);
			}
			return cards;
		}
	}
}
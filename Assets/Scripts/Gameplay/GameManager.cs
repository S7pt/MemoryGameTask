using System.Collections;
using System.Collections.Generic;
using TestTask.Data;
using TestTask.UI;
using TestTask.Utility;
using UnityEngine;

namespace TestTask.Gameplay
{
	public class GameManager : MonoBehaviour
	{
		private const int MAX_PAIRS = 3;
		private const int MAX_ACTIVE_CARDS = 2;
		private const float INITIAL_DELAY = 1;
		private const float CARDS_SHOWCASE_DELAY = 1;
		[SerializeField] private GameUI _gameUI;
		[SerializeField] private CardSpawner _spawner;
		[SerializeField] private JSONParser _parser;
		private CardsData _data;
		private List<Sprite> _shuffledSprites;
		private List<Card> _cards;
		private List<Card> _selectedCards;
		private bool _isFlipping;
		private int _pairsCompleted = 0;

		private void Start()
		{
			StartCoroutine(nameof(StartGame));
		}

		private void OnDestroy()
		{
			foreach (Card card in _cards)
			{
				card.CardSelected -= OnCardSelected;
				card.CardFlipped -= OnCardFlipped;
			}
		}

		private IEnumerator StartGame()
		{
			_gameUI.SetEndGameScreenActive(false);
			_gameUI.SetCounterText(_pairsCompleted);
			_gameUI.SetLoadingScreenActive(true);
			yield return _parser.GetDataRoutine();
			_data = _parser.Data;
			_selectedCards = new List<Card>(MAX_ACTIVE_CARDS);
			_shuffledSprites = ListUtility.Shuffle(ListUtility.ExpandList(_data.Faces));
			_cards = _spawner.SpawnCards(_shuffledSprites, _data.Back);
			_gameUI.SetLoadingScreenActive(false);
			yield return CardInitializationRoutine();
		}

		private IEnumerator CardInitializationRoutine()
		{
			yield return new WaitForSeconds(INITIAL_DELAY);
			foreach (Card card in _cards)
			{
				card.FlipToBack();
			}
			yield return new WaitForSeconds(INITIAL_DELAY);
			foreach (Card card in _cards)
			{
				card.CardSelected += OnCardSelected;
				card.CardFlipped += OnCardFlipped;
			}
		}

		private void OnCardFlipped(Card card)
		{
			_isFlipping = false;
			if (_selectedCards.Count == MAX_ACTIVE_CARDS)
			{
				ValidateSelected();
			}
		}

		private void ValidateSelected()
		{
			StartCoroutine(nameof(ValidateRoutine));
		}

		private IEnumerator ValidateRoutine()
		{
			yield return new WaitForSeconds(CARDS_SHOWCASE_DELAY);
			if (_selectedCards[0].Face == _selectedCards[1].Face)
			{
				_selectedCards[0].Destroy();
				_selectedCards[1].Destroy();
				_pairsCompleted++;
				_gameUI.SetCounterText(_pairsCompleted);
				if (_pairsCompleted == MAX_PAIRS)
				{
					_gameUI.SetEndGameScreenActive(true);
				}
			}
			else
			{
				_selectedCards[0].FlipToBack();
				_selectedCards[1].FlipToBack();
			}
			_selectedCards.Clear();
		}

		private void OnCardSelected(Card card)
		{
			if (_selectedCards.Count < MAX_ACTIVE_CARDS && !_isFlipping)
			{
				card.FlipToFace();
				_selectedCards.Add(card);
				_isFlipping = true;
			}
		}
	}
}
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

namespace TestTask.Gameplay
{
	public class Card : MonoBehaviour
	{
		[SerializeField] private Button _cardButton;
		[SerializeField] private Image _cardImage;
		private const float ROTATION_DELAY = 1f;
		private Sprite _cardFace;
		private Sprite _cardBack;

		public event Action<Card> CardSelected;
		public event Action<Card> CardFlipped;

		public Sprite Face => _cardFace;

		private void OnDestroy()
		{
			_cardButton.onClick.RemoveListener(OnCardClicked);
		}

		public void Init(Sprite face, Sprite back)
		{
			_cardFace = face;
			_cardBack = back;
			ChangeCardSprite(_cardFace);
			_cardButton.onClick.AddListener(OnCardClicked);
		}

		public void Destroy()
		{
			Destroy(_cardImage);
			_cardImage = null;
		}

		public void FlipToFace()
		{
			StartCoroutine(Flip(_cardFace, false));
		}

		public void FlipToBack()
		{
			StartCoroutine(Flip(_cardBack, true));
		}

		private IEnumerator Flip(Sprite side, bool isFlippingToBack)
		{
			Tween _edgeRotate = transform.DORotate(new Vector2(0, 90), ROTATION_DELAY);
			_edgeRotate.Play();
			yield return _edgeRotate.WaitForCompletion();
			ChangeCardSprite(side);
			transform.DORotate(Vector3.zero, ROTATION_DELAY);
			if (!isFlippingToBack)
			{
				CardFlipped?.Invoke(this);
			}
		}

		private void ChangeCardSprite(Sprite sprite)
		{
			_cardImage.sprite = sprite;
		}

		private void OnCardClicked()
		{
			CardSelected?.Invoke(this);
		}
	}
}
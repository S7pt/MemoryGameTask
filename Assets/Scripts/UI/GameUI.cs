using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TestTask.UI
{
	public class GameUI : MonoBehaviour
	{
		[SerializeField] private GameObject _loadingScreen;
		[SerializeField] private GameObject _gameEndScreen;
		[SerializeField] private TMP_Text _pairCounter;
		[SerializeField] private Button _restartButton;
		[SerializeField] private Button _exitButton;

		private void Awake()
		{
			_restartButton.onClick.AddListener(OnRestartButtonClicked);
			_exitButton.onClick.AddListener(OnExitButtonClicked);
		}

		private void OnExitButtonClicked()
		{
			Application.Quit();
		}

		private void OnRestartButtonClicked()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		public void SetLoadingScreenActive(bool isActive)
		{
			_loadingScreen.SetActive(isActive);
		}

		public void SetCounterText(int value)
		{
			_pairCounter.text = value.ToString();
		}

		public void SetEndGameScreenActive(bool isActive)
		{
			_gameEndScreen.SetActive(isActive);
		}

		private void OnDestroy()
		{
			_restartButton.onClick.RemoveListener(OnRestartButtonClicked);
			_exitButton.onClick.RemoveListener(OnExitButtonClicked);
		}
	}
}
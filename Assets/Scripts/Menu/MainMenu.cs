using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;


namespace RepairCrew
{
	public class MainMenu : SubmenuWithNavigationToDifferentSubmenu
	{
		[SerializeField] private Button startGameButton = null;
		[SerializeField] private Button howToPlayButton = null;
		[SerializeField] private Submenu howToPlayMenu = null;


		protected override void Awake()
		{
			base.Awake();
			Assert.IsNotNull(startGameButton, "Missing startGameButton on: " + gameObject.name);
			Assert.IsNotNull(howToPlayButton, "Missing howToPlayButton on: " + gameObject.name);
			startGameButton.onClick.AddListener(OnStartGameButtonClicked);
			howToPlayButton.onClick.AddListener(OnHowToPlayButtonClicked);
		}

		private void OnStartGameButtonClicked()
		{
			SceneManager.LoadScene("Gameplay");
		}

		private void OnHowToPlayButtonClicked()
		{
			RequestTransitionToSubmenu?.Invoke(howToPlayMenu);
		}
	}
}

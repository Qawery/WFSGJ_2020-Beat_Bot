using UnityEngine;
using UnityEngine.Assertions;


namespace WFS
{
	public class EndScreen : MonoBehaviour
	{
		[Zenject.Inject] private Board board;
		[SerializeField] private TMPro.TextMeshProUGUI message = null;
		private const string VICTORY_MESSAGE = "Victory!";
		private const string DEFEAT_MESSAGE = "Defeat!";
		

		private void Awake()
		{
			Assert.IsNotNull(message);
		}

		private void Start()
		{
			if (board.HasCombatEnded)
			{
				Show();
			}
			else
			{
				gameObject.SetActive(false);
				board.OnGameEnded += Show;
			}
		}

		private void Show()
		{
			gameObject.SetActive(true);
			message.text = board.HasPlayerWon ? VICTORY_MESSAGE : DEFEAT_MESSAGE;
		}
	}
}
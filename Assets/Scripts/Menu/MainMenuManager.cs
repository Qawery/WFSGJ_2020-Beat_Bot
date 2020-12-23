using UnityEngine;


namespace RepairCrew
{
	public class MainMenuManager : MenuManager
	{
		protected override void ExitFromMenu()
		{
			Application.Quit();
		}
	}
}
using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;


namespace WFS
{
	public class ComputerPlayer : MonoBehaviour
	{
		[Zenject.Inject] private Board board = null;
		[SerializeField, Range(0.1f, 10.0f)] private float timeBetweenActions = 2.0f;
		private float timer = 0.0f;


		private void Start()
		{
			timer = timeBetweenActions;
		}

		private void Update()
		{
			if (board.Combat.ComputerTeam.NumberOfUnits() > 0)
			{
				if (timer > 0.0f)
				{
					timer -= Time.deltaTime;
				}
				else
				{
					foreach (var unit in board.Combat.ComputerTeam.UnitsInTeam)
					{
						var availableActions = unit.unit.AvailableActions;
						var actionVariants = new List<CombatAction>();
						foreach (var availableAction in unit.unit.AvailableActions)
						{
							actionVariants.AddRange(availableAction.AllVariants);
						}
						if (actionVariants.Count > 0)
						{
							var selectedAction = actionVariants[Random.Range(0, actionVariants.Count)];
							Assert.IsTrue(selectedAction.IsValid);
							selectedAction.Execute();
						}
					}
					timer = timeBetweenActions;
				}
			}
		}
	}
}
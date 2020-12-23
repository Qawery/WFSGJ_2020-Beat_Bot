using UnityEngine;
using UnityEngine.Assertions;


namespace WFS
{
	public class Board : MonoBehaviour
	{
		[SerializeField] private Material playerMaterial = null;
		[SerializeField] private Material computerMaterial = null;
		[SerializeField] private BoardPositions playerTeamPositions = null;
		[SerializeField] private BoardPositions computerTeamPositions = null;
		[SerializeField] private UnitComponent unitPrefab = null;
		[Zenject.Inject] private IWorld world = null;
		private Combat combat = new Combat();
		private bool hasCombatEnded = false;


		public event System.Action OnGameEnded;


		public Combat Combat => combat;
		public bool HasCombatEnded => hasCombatEnded;
		public bool HasPlayerWon => combat.PlayerTeam?.NumberOfUnits() > 0 && combat.ComputerTeam?.NumberOfUnits() == 0;


		private void Awake()
		{
			Assert.IsNotNull(playerMaterial);
			Assert.IsNotNull(computerMaterial);
			Assert.IsNotNull(playerTeamPositions);
			Assert.IsNotNull(computerTeamPositions);
			Assert.IsNotNull(unitPrefab);
			combat.PlayerTeam = new Team();
			combat.ComputerTeam = new Team();
			combat.PlayerTeam.OnTeamDestroyed += OnPlayerTeamDestroyed;
			combat.ComputerTeam.OnTeamDestroyed += OnComputerTeamDestroyed;
		}

		private void Start()
		{
			var newUnit = world.InstantiatePrefab<UnitComponent>(unitPrefab, playerTeamPositions.Positions[0].transform.position, 
				playerTeamPositions.Positions[0].transform.rotation, playerTeamPositions.Positions[0].transform);
			combat.PlayerTeam.AddUnit(newUnit.Unit);
			newUnit.UnitColorPainter.ColorWithMaterial(playerMaterial);
			newUnit = world.InstantiatePrefab<UnitComponent>(unitPrefab, computerTeamPositions.Positions[0].transform.position,
				computerTeamPositions.Positions[0].transform.rotation, computerTeamPositions.Positions[0].transform);
			combat.ComputerTeam.AddUnit(newUnit.Unit);
			newUnit.UnitColorPainter.ColorWithMaterial(computerMaterial);
		}

		private void OnPlayerTeamDestroyed()
		{
			hasCombatEnded = true;
			OnGameEnded?.Invoke();
		}

		private void OnComputerTeamDestroyed()
		{
			hasCombatEnded = true;
			OnGameEnded?.Invoke();
		}
	}
}
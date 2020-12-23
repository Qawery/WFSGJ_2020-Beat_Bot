using UnityEngine;
using Zenject;

namespace WFS
{
	public class PlayerController : MonoBehaviour
	{
		[Inject] private InputMapping inputMapping = null;
		[Inject] private Board board = null;
		[Inject] private NoteSequenceSet noteSequenceSet = null;
		[Inject] private ActionMapper actionMapper = null;

		private int selectedUnitIndex = -1;

		private NoteSequenceSet currentSequenceSet;
		private NoteSequenceSet CurrentSequenceSet
		{
			get => currentSequenceSet;
			set
			{
				if (currentSequenceSet != null)
				{
					currentSequenceSet.OnSequenceSucceeded -= OnCurrentSequenceSucceeded;
					currentSequenceSet.OnAllSequencesFailed -= OnAllSequencesFailed;
				}

				currentSequenceSet = value;

				if (currentSequenceSet != null)
				{
					currentSequenceSet.OnSequenceSucceeded += OnCurrentSequenceSucceeded;
					currentSequenceSet.OnAllSequencesFailed += OnAllSequencesFailed;
				}
			}
		}

		public NoteSequenceSet NoteSequenceSet => noteSequenceSet;
		public event System.Action<int> OnUnitSelected;
		public event System.Action OnSelectionCleared;

		private void SelectUnit(int unitIndex)
		{
			if (selectedUnitIndex != -1)
			{
				return;
			}
			
			if (noteSequenceSet.TryInitiateSequences())
			{
				CurrentSequenceSet = noteSequenceSet;
				selectedUnitIndex = unitIndex;
				OnUnitSelected?.Invoke(unitIndex);
			}
			else
			{
				CurrentSequenceSet = null;
			}
		}

		private void OnAllSequencesFailed()
		{
			OnSelectionCleared?.Invoke();
			CurrentSequenceSet = null;
			selectedUnitIndex = -1;
		}

		private void OnCurrentSequenceSucceeded(INoteSequence sequence)
		{
			Debug.Log($"Sequence succeeded, performing action {sequence.Name}");
			if (board.Combat.PlayerTeam.UnitsInTeam.Count > selectedUnitIndex)
			{
				var availableAction = board.Combat.PlayerTeam.UnitsInTeam[selectedUnitIndex].unit.AvailableActions
					.Find(action => actionMapper.GetSequenceForActionType(action.GetType()) == sequence);
				var variants = availableAction.AllVariants;
				if (variants.Count > 0)
				{
					variants[0].Execute();
				}
			}
			selectedUnitIndex = -1;
		}

		private void Update()
		{
			for (int scaleDegree = 1; scaleDegree <= 8; ++scaleDegree)
			{
				KeyCode scaleDegreeKey = inputMapping.GetKeyForScaleDegree(scaleDegree);
				if (Input.GetKeyDown(scaleDegreeKey))
				{
					SelectUnit(0);
				}
			}

			if (CurrentSequenceSet != null)
			{
				for (int scaleDegree = 1; scaleDegree <= 8; ++scaleDegree)
				{
					KeyCode scaleDegreeKey = inputMapping.GetKeyForScaleDegree(scaleDegree);
					if (Input.GetKeyDown(scaleDegreeKey))
					{
						CurrentSequenceSet.RegisterNotePlayed(scaleDegree);
					}
				}
			}
		}
	}
}

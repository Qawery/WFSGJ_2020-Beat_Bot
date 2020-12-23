using System.Collections.Generic;
using UnityEngine;

namespace WFS
{
	public class UnitActionPanel : MonoBehaviour
	{
		[SerializeField] private List<NoteSequenceSetVisualizer> sequenceSetVisualizers = null;
		private PlayerController playerController = null;

		[Zenject.Inject] 
		private void InjectionMethod(PlayerController playerController)
		{
			this.playerController = playerController;
			playerController.OnSelectionCleared += OnSelectionCleared;
		}

		private void OnSelectionCleared()
		{
			foreach (var visualizer in sequenceSetVisualizers)
			{
				visualizer.Clear();
			}
		}

		private void Start()
		{
			OnSelectionCleared();
			foreach (var sequenceSetVisualizer in sequenceSetVisualizers)
			{
				sequenceSetVisualizer.SetNoteSequenceSet(playerController.NoteSequenceSet);
			}
		}
	}
}


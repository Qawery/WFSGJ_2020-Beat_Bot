using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace WFS
{
	public class NoteSequenceVisualizer : MonoBehaviour
	{
		[SerializeField] private NoteVisualizer noteVisualizerPrefab = null;
		[SerializeField] private TextMeshProUGUI sequenceName = null;
		[Inject] private IWorld world = null;
		private List<NoteVisualizer> spawnedNoteVisualizers = new List<NoteVisualizer>();
		
		public void SetNoteSequenceChecker(INoteSequenceChecker noteSequenceChecker)
		{
			sequenceName.text = noteSequenceChecker.NoteSequence.Name;
			foreach (var note in noteSequenceChecker.NoteSequence.Notes)
			{
				var noteVisualizer = world.InstantiatePrefab(noteVisualizerPrefab, transform);
				noteVisualizer.SetNote(note);
				spawnedNoteVisualizers.Add(noteVisualizer);
			}

			noteSequenceChecker.OnNoteHit += noteIndex =>
			{
				spawnedNoteVisualizers[noteIndex].VisualizeHit();
			};

			noteSequenceChecker.OnNoteSequenceFinished += result =>
			{
				if (result == NoteSequenceResult.Succeeded)
				{
					//TODO: visualize success
				}
				else
				{
					//TODO: visualize failure
				}
			};
		}

		public void Clear()
		{
			foreach (var note in spawnedNoteVisualizers)
			{
				note.Clear();
			}
		}
	}
}


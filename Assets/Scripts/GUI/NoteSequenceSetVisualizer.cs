using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace WFS
{
	public class NoteSequenceSetVisualizer : MonoBehaviour
	{
		[Inject] private IWorld world = null;
		[SerializeField] private NoteSequenceVisualizer noteSequenceVisualizerPrefab = null;
		List<NoteSequenceVisualizer> spawnedSequenceVisualizers = new List<NoteSequenceVisualizer>();
		
		public void SetNoteSequenceSet(NoteSequenceSet noteSequenceSet)
		{
			foreach (var noteSequenceChecker in noteSequenceSet.NoteSequenceCheckers)
			{
				var sequenceVisualizer = world.InstantiatePrefab(noteSequenceVisualizerPrefab, transform);
				sequenceVisualizer.SetNoteSequenceChecker(noteSequenceChecker);
				spawnedSequenceVisualizers.Add(sequenceVisualizer);
			}
		}

		public void Clear()
		{
			foreach (var viz in spawnedSequenceVisualizers)
			{
				viz.Clear();
			}
		}
	}
}

using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace WFS
{
	public class NoteSequenceSet : MonoBehaviour
	{
		[SerializeField] private float initialNoteTolerance = 0.1f;

		private IBeatProvider beatProvider = null;
		private int failureCounter = 0;
		private List<INoteSequenceChecker> noteSequenceCheckers;
		private float lastBeatTimestamp = 0.0f;
		public List<INoteSequenceChecker> NoteSequenceCheckers => noteSequenceCheckers;
		public event System.Action<INoteSequence> OnSequenceSucceeded;
		public event System.Action OnAllSequencesFailed;

		public bool TryInitiateSequences()
		{
			//if we're minimally late
			if (Time.time - lastBeatTimestamp < initialNoteTolerance)
			{
				InitiateSequences(lastBeatTimestamp);
				return true;
			}
			
			//or minimally early
			if (beatProvider.GetBeatTimestamp(0) - Time.time < initialNoteTolerance)
			{
				InitiateSequences(beatProvider.GetBeatTimestamp(0));
				return true;
			}
			
			Debug.Log("Failed to initialize sequence");
			return false;
		}
		
		public void InitiateSequences(float startTimestamp)
		{
			failureCounter = 0;
			foreach (var seq in noteSequenceCheckers)
			{
				seq.InitiateSequence(startTimestamp);
			}
		}

		public void RegisterNotePlayed(int scaleDegree)
		{
			foreach (var seq in noteSequenceCheckers)
			{
				seq.RegisterNotePlayed(scaleDegree);
			}
		}
		
		[Inject]
		private void InjectionMethod(IBeatProvider beatProvider, 
			List<INoteSequenceChecker> noteSequenceCheckers)
		{
			this.beatProvider = beatProvider;
			beatProvider.OnBeat += beatNumber => lastBeatTimestamp = Time.time;
			
			this.noteSequenceCheckers = noteSequenceCheckers;
			foreach (var sequenceChecker in noteSequenceCheckers)
			{
				var seqCopy = sequenceChecker;
				sequenceChecker.OnNoteSequenceFinished += result =>
				{
					if (result == NoteSequenceResult.Succeeded)
					{
						OnSequenceSucceeded?.Invoke(seqCopy.NoteSequence);
					}
					else
					{
						++failureCounter;
						if (failureCounter == noteSequenceCheckers.Count)
						{
							OnAllSequencesFailed?.Invoke();
						}
					}
				};
			}
		}
	}
}


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;


namespace WFS
{
	public enum NoteSequenceResult
	{
		Succeeded,
		Failed,
	}

	public interface INoteSequenceChecker
	{
		INoteSequence NoteSequence { get; }
		event System.Action<int> OnNoteHit;
		event System.Action<NoteSequenceResult> OnNoteSequenceFinished;
		void InitiateSequence(float startTimestamp);
		void RegisterNotePlayed(int scaleDegree);
	}

	public class NoteSequenceChecker : MonoBehaviour, INoteSequenceChecker
	{
		[SerializeField] private float noteTolerance = 0.1f;
		[SerializeField] private NoteSequence noteSequenceSO = null;
		[Inject] private IBeatProvider beatProvider = null;
		private List<(float timestamp, int scaleDegree)> noteTimestamps = new List<(float timestamp, int scaleDegree)>();
		private INoteSequence noteSequence;
		private bool sequenceInitiated = false;
		private int lastNoteHit = -1;
		private float sequenceStartTimestamp = 0.0f;
		
		public INoteSequence NoteSequence
		{
			get => noteSequence;
			set
			{
				noteSequence = value;
				noteTimestamps.Clear();
				Assert.IsNotNull(NoteSequence);
				foreach (var note in noteSequence.Notes)
				{
					float noteTimestamp = beatProvider.GetRelativeTimestampOfNote(noteSequence.Subdivision, note.Start);
					noteTimestamps.Add((noteTimestamp, note.ScaleDegree));
				}
				OnNoteSequenceSet?.Invoke(noteTimestamps);
			}
		}

		public float NoteTolerance => noteTolerance;

		public event System.Action<IReadOnlyList<(float timestamp, int scaleDegree)>> OnNoteSequenceSet;
		public event System.Action<int> OnNoteHit;
		public event System.Action<NoteSequenceResult> OnNoteSequenceFinished;

		public void InitiateSequence(float startTimestamp)
		{
			sequenceInitiated = true;
			sequenceStartTimestamp = startTimestamp;
			lastNoteHit = -1;
		}
		
		public void RegisterNotePlayed(int scaleDegree)
		{
			if (!sequenceInitiated)
			{
				return;
			}
			
			var (timestamp, targetScaleDegree) = noteTimestamps[lastNoteHit + 1];
			if (Mathf.Abs(sequenceStartTimestamp + timestamp - Time.time) < noteTolerance)
			{
				if (targetScaleDegree == scaleDegree)
				{
					lastNoteHit++;
					OnNoteHit?.Invoke(lastNoteHit);
					if (lastNoteHit == noteTimestamps.Count - 1)
					{
						OnNoteSequenceFinished?.Invoke(NoteSequenceResult.Succeeded);
						sequenceInitiated = false;
					}
				}
				else
				{
					OnNoteSequenceFinished?.Invoke(NoteSequenceResult.Failed);
					sequenceInitiated = false;
				}
			}
			else
			{
				OnNoteSequenceFinished?.Invoke(NoteSequenceResult.Failed);
				sequenceInitiated = false;
			}
		}
		
		private void Awake()
		{
			if (noteSequenceSO != null)
			{
				NoteSequence = noteSequenceSO;
			}
		}

		//TODO: port to update manager 
		private void Update()
		{
			if (!sequenceInitiated)
			{
				return;
			}

			var (timestamp, targetScaleDegree) = noteTimestamps[lastNoteHit + 1];
			//we're to late for current sequence note
			if (Time.time - sequenceStartTimestamp - timestamp > noteTolerance)
			{
				OnNoteSequenceFinished?.Invoke(NoteSequenceResult.Failed);
				sequenceInitiated = false;
			}
		}
	}
}

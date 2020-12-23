using System.Collections.Generic;
using UnityEngine;

namespace WFS
{
	[System.Serializable]
	public class Note
	{
		[SerializeField] private int start = 1;
		[SerializeField] private int scaleDegree = 1;

		public int Start => start;
		public int ScaleDegree => scaleDegree;

		public Note()
		{
		}

		public Note(int start, int scaleDegree)
		{
			this.start = start;
			this.scaleDegree = scaleDegree;
		}
	}

	public interface INoteSequence
	{
		int Subdivision { get; }
		IReadOnlyList<Note> Notes { get; }	
		string Name { get; }
	}
	
	[CreateAssetMenu(menuName = "NoteSequence", fileName = "NoteSequence")]
	public class NoteSequence : ScriptableObject, INoteSequence
	{
		[SerializeField] private int subdivision = 4;
		[SerializeField] private List<Note> notes = new List<Note>();
		public int Subdivision => subdivision;
		public IReadOnlyList<Note> Notes => notes;
		public string Name => name;
	}
}

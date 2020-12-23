using System.Collections.Generic;
using UnityEngine;

namespace WFS
{
	public class InputMapping
	{
		private List<(KeyCode keyCode, int scaleDegree)> keyMapping =
			new List<(KeyCode keyCode, int scaleDegree)>()
			{
				(KeyCode.Q, 1),
				(KeyCode.W, 2),
				(KeyCode.E, 3),
				(KeyCode.R, 4),
				(KeyCode.A, 5),
				(KeyCode.S, 6),
				(KeyCode.D, 7),
				(KeyCode.F, 8),
			};

		public KeyCode[] UnitSelectionKeys => new[] {KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3};
		
		public KeyCode GetKeyForScaleDegree(int scaleDegree)
		{
			return keyMapping.Find(tuple => tuple.scaleDegree == scaleDegree).keyCode;
		}

		public int GetScaleDegreeForKeyCode(KeyCode keyCode)
		{
			int index = keyMapping.FindIndex(tuple => tuple.keyCode == keyCode);
			if (index < 0)
			{
				return -1;
			}

			return keyMapping[index].scaleDegree;
		}
	}
}


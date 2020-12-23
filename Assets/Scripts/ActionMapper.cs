using System;
using System.Collections.Generic;
using UnityEngine;

namespace WFS
{
	[CreateAssetMenu(menuName = "ActionMapper", fileName = "ActionMapper")]
	public class ActionMapper : ScriptableObject
	{
		[SerializeField] private NoteSequence attackSequence = null;
		[SerializeField] private NoteSequence moveSequence = null;
		[SerializeField] private NoteSequence healSequence = null;

		private Dictionary<System.Type, NoteSequence> actionToSequenceMapping = null;

		public INoteSequence GetSequenceForActionType(System.Type actionType)
		{
			if (actionToSequenceMapping == null)
			{
				actionToSequenceMapping = new Dictionary<Type, NoteSequence>()
				{
					[typeof(BasicAttack)] = attackSequence,
					[typeof(HealAction)] = healSequence,
				};
			}

			return actionToSequenceMapping[actionType];
		}
	}
 }

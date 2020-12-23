using UnityEngine.Assertions;
using System.Collections.Generic;


namespace WFS
{
	public abstract class CombatAction
	{
		public abstract string AnimationName { get; }
		public abstract bool IsValid { get; }
		public abstract List<CombatAction> AllVariants { get; }


		public void Execute()
		{
			Assert.IsTrue(IsValid);
			ApplyEffect();
			Cleanup();
		}

		protected abstract void ApplyEffect();
		protected abstract void Cleanup();
	}


	public abstract class UnitAction : CombatAction, IUnitIssuedCombatAction
	{
		protected Unit issuer = null;


		public Unit Issuer
		{
			get => issuer;
		}


		public UnitAction(Unit issuer)
		{
			Assert.IsNotNull(issuer);
			this.issuer = issuer;
		}
	}

	public abstract class UnitOnUnitAction : UnitAction, IUnitTargetCombatAction
	{
		protected Unit target = null;


		public override List<CombatAction> AllVariants
		{
			get
			{
				var result = new List<CombatAction>();
				foreach (var potentialTarget in PotentialTargets)
				{				
					var newVariant = MemberwiseClone() as UnitOnUnitAction;
					newVariant.target = potentialTarget;
					if (newVariant.IsValid)
					{
						result.Add(newVariant);
					}
				}
				return result;
			}
		}

		public abstract List<Unit> PotentialTargets { get; }

		public Unit Target
		{
			get => target;
			set
			{
				target = value;
			}
		}


		public UnitOnUnitAction(Unit issuer) : base(issuer)
		{
		}

		public override bool IsValid => issuer.IsAlive && target != null;
	}
}
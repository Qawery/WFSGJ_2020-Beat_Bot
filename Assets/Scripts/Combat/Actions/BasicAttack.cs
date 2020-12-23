using UnityEngine.Assertions;
using System.Collections.Generic;


namespace WFS
{
	public class BasicAttack : UnitOnUnitAction
	{
		public override string AnimationName => UnitAnimationController.ATTACK;

		public override List<Unit> PotentialTargets
		{
			get
			{
				var result = new List<Unit>();
				if (issuer.Team != null && issuer.Team.Combat != null)
				{
					foreach (var unitAndPosition in issuer.Team.Combat.GetOpposingTeam(issuer.Team).UnitsInTeam)
					{
						result.Add(unitAndPosition.unit);
					}
				}
				return result;
			}
		}


		public BasicAttack(Unit issuer) : base(issuer)
		{
		}

		protected override void ApplyEffect()
		{
			Assert.IsTrue(issuer.IsAlive);
			Assert.IsNotNull(target);
			target.ApplyDamage(issuer.BaseDamage);
			issuer.OnActionExecuted?.Invoke(this);
		}

		protected override void Cleanup()
		{
			issuer = null;
			target = null;
		}
	}
}
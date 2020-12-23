using System.Collections.Generic;


namespace WFS
{
	public class HealAction : UnitAction
	{
		public override string AnimationName => UnitAnimationController.DEFENCE;
		public override bool IsValid => issuer.IsAlive;
		public override List<CombatAction> AllVariants => new List<CombatAction>{new HealAction(issuer)};


		public HealAction(Unit issuer) : base(issuer)
		{
		}

		protected override void ApplyEffect()
		{
			issuer.ApplyHeal(issuer.BaseHealing);
			issuer.OnActionExecuted?.Invoke(this);
		}

		protected override void Cleanup()
		{
			issuer = null;
		}
	}
}

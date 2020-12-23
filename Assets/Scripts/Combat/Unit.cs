using System;
using UnityEngine.Assertions;
using System.Collections.Generic;


namespace WFS
{
	public class Unit
	{
		private int baseDamage = 1;
		private int baseHealing = 1;
		private int maxHealth = 1;
		private int currentHealth = 1;
		private Team team = null;


		public event Action OnHealed;
		public event Action OnDamaged;
		public event Action<Unit> OnDeath;
		public Action<CombatAction> OnActionExecuted;


		public int BaseDamage => baseDamage;
		public int BaseHealing => baseHealing;
		public int MaxHealth => maxHealth;
		public int CurrentHealth => currentHealth;
		public bool IsAlive => currentHealth > 0;
		public float HealthPercentage => (float) currentHealth / maxHealth;
		public Team Team
		{
			get => team;

			set
			{
				team = value;
			}
		}

		public List<CombatAction> AvailableActions => new List<CombatAction>() { new BasicAttack(this), new HealAction(this) };


		public Unit(int maxHealth, int baseDamage, int baseHealing)
		{
			Assert.IsTrue(maxHealth >= 1);
			this.maxHealth = maxHealth;
			currentHealth = maxHealth;
			Assert.IsTrue(baseDamage >= 1);
			this.baseDamage = baseDamage;
			Assert.IsTrue(baseHealing >= 1);
			this.baseHealing = baseHealing;
		}

		public void ApplyDamage(int damage)
		{
			Assert.IsTrue(damage >= 0);
			if (IsAlive)
			{
				currentHealth = Math.Max(0, currentHealth - damage);
				OnDamaged?.Invoke();
				if (!IsAlive)
				{
					OnDeath?.Invoke(this);
				}
			}
		}

		public void ApplyHeal(int heal)
		{
			Assert.IsTrue(heal >= 0);
			if (IsAlive)
			{
				currentHealth = Math.Min(maxHealth, currentHealth + heal);
				OnHealed?.Invoke();
			}
		}
	}
}
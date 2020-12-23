using UnityEngine;
using UnityEngine.Assertions;


namespace WFS
{
	public class UnitComponent : MonoBehaviour
	{
		[SerializeField, Range(1, 1000)] private int maxHealth = 1;
		[SerializeField, Range(1, 1000)] private int baseDamage = 1;
		[SerializeField, Range(1, 1000)] private int baseHealing = 1;
		private Unit unit = null;
		private UnitColorPainter unitColorPainter = null;


		public Unit Unit => unit;
		public UnitColorPainter UnitColorPainter => unitColorPainter;


		private void Awake()
		{
			unit = new Unit(maxHealth, baseDamage, baseHealing);
			unit.OnDeath += OnDeath;
			unitColorPainter = GetComponent<UnitColorPainter>();
			Assert.IsNotNull(unitColorPainter);
		}

		private void OnDeath(Unit deadUnit)
		{
			unit = null;
			Destroy(gameObject);
		}
	}
}
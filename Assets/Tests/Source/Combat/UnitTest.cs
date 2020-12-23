using NUnit.Framework;


namespace WFS
{
    namespace Tests
    {
        public class UnitTest
        {
            private const int UNIT_MAX_HEALTH = UNIT_HALF_HEALTH * 2;
            private const int UNIT_HALF_HEALTH = 2;
            private const int UNIT_BASE_DAMAGE = UNIT_HALF_HEALTH;


            [Test]
            public void HealthTest()
            {
                //Initialization
                var unit = new Unit(UNIT_MAX_HEALTH, UNIT_BASE_DAMAGE, 0);
                Assert.IsTrue(unit.CurrentHealth == UNIT_MAX_HEALTH);
                Assert.IsTrue(unit.MaxHealth == UNIT_MAX_HEALTH);
                Assert.IsTrue(unit.IsAlive);
                Assert.IsTrue(unit.HealthPercentage == 1.0f);

                bool wasDamaged = false, wasHealed = false, hasDied = false;
                unit.OnDamaged += () => { wasDamaged = true; };
                unit.OnHealed += () => { wasHealed = true; };
                unit.OnDeath += (Unit unitKilled) => { hasDied = true; };

                //Damage
                unit.ApplyDamage(UNIT_HALF_HEALTH);
                Assert.IsTrue(wasDamaged);
                Assert.IsTrue(unit.CurrentHealth == UNIT_HALF_HEALTH);
                Assert.IsTrue(unit.HealthPercentage == 0.5f);

                //Heal
                unit.ApplyHeal(UNIT_HALF_HEALTH);
                Assert.IsTrue(wasHealed);
                Assert.IsTrue(unit.CurrentHealth == UNIT_MAX_HEALTH);
                Assert.IsTrue(unit.HealthPercentage == 1.0f);

                //Excessive heal
                unit.ApplyHeal(UNIT_HALF_HEALTH);
                Assert.IsTrue(unit.CurrentHealth == UNIT_MAX_HEALTH);
                Assert.IsTrue(unit.HealthPercentage == 1.0f);

                //Mortal damage
                Unit deadUnit = null;
                unit.OnDeath += (Unit unitKilled) => { deadUnit = unitKilled; };
                unit.ApplyDamage(UNIT_MAX_HEALTH);
                Assert.IsTrue(hasDied);
                Assert.IsTrue(unit.CurrentHealth == 0);
                Assert.IsFalse(unit.IsAlive);
                Assert.IsTrue(unit.HealthPercentage == 0.0f);
                Assert.IsTrue(deadUnit == unit);

                //Excessive damage
                wasDamaged = false;
                hasDied = false;
                unit.ApplyDamage(UNIT_MAX_HEALTH);
                Assert.IsFalse(wasDamaged);
                Assert.IsFalse(hasDied);
                Assert.IsTrue(unit.CurrentHealth == 0);
                Assert.IsFalse(unit.IsAlive);
                Assert.IsTrue(unit.HealthPercentage == 0.0f);

                //Healing dead
                wasHealed = false;
                unit.ApplyHeal(UNIT_HALF_HEALTH);
                Assert.IsFalse(wasHealed);
                Assert.IsTrue(unit.CurrentHealth == 0);
                Assert.IsFalse(unit.IsAlive);
                Assert.IsTrue(unit.HealthPercentage == 0.0f);
            }


            [Test]
            public void CombatActionTest()
            {
                var playerUnit = new Unit(UNIT_MAX_HEALTH, UNIT_BASE_DAMAGE, 0);

                //Provided actions
                Assert.IsTrue(playerUnit.AvailableActions.Count == 1);
                bool isBasicAttackPresent = false;
                foreach (var combatAction in playerUnit.AvailableActions)
                {
                    if (combatAction is BasicAttack)
                    {
                        Assert.IsFalse(isBasicAttackPresent);
                        isBasicAttackPresent = true;
                    }
                }
                Assert.IsTrue(isBasicAttackPresent);
            }
        }
    }
}

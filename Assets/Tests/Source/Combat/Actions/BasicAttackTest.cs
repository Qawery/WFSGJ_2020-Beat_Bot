using NUnit.Framework;


namespace WFS
{
    namespace Tests
    {
        public class BasicAttackTest
        {
            private const int UNIT_MAX_HEALTH = UNIT_BASE_DAMAGE * 3;
            private const int UNIT_BASE_DAMAGE = 2;


            [Test]
            public void DamagingAndTargetAcquisitionTest()
            {
                var playerUnit = new Unit(UNIT_MAX_HEALTH, UNIT_BASE_DAMAGE, 0);
                var computerUnit = new Unit(UNIT_MAX_HEALTH, UNIT_BASE_DAMAGE, 0);

                //Direct references
                var basicAttack = new BasicAttack(playerUnit);
                Assert.IsFalse(basicAttack.IsValid);
                basicAttack.Target = computerUnit;
                Assert.IsTrue(basicAttack.IsValid);
                basicAttack.Execute();
                Assert.IsTrue(computerUnit.CurrentHealth == computerUnit.MaxHealth - playerUnit.BaseDamage);
                Assert.IsNull(basicAttack.Issuer);
                Assert.IsNull(basicAttack.Target);

                //Setting up combat
                var combat = new Combat();
                combat.PlayerTeam = new Team();
                combat.ComputerTeam = new Team();
                combat.PlayerTeam.AddUnit(playerUnit);
                combat.ComputerTeam.AddUnit(computerUnit);
                
                //Choosing from potential targets
                basicAttack = new BasicAttack(playerUnit);
                var potentialTargets = basicAttack.PotentialTargets;
                Assert.IsTrue(potentialTargets.Count == 1);
                Assert.IsTrue(potentialTargets[0] == computerUnit);
                basicAttack.Target = potentialTargets[0];
                basicAttack.Execute();
                Assert.IsTrue(computerUnit.CurrentHealth == computerUnit.MaxHealth - (playerUnit.BaseDamage * 2));

                //Retaliation
                basicAttack = new BasicAttack(computerUnit);
                potentialTargets = basicAttack.PotentialTargets;
                Assert.IsTrue(potentialTargets.Count == 1);
                Assert.IsTrue(potentialTargets[0] == playerUnit);
                basicAttack.Target = potentialTargets[0];
                basicAttack.Execute();
                Assert.IsTrue(playerUnit.CurrentHealth == playerUnit.MaxHealth - computerUnit.BaseDamage);

                //Variant
                basicAttack = new BasicAttack(computerUnit);
                var basicAttackVariants = basicAttack.AllVariants;
                Assert.IsTrue(basicAttackVariants.Count == 1);
                basicAttackVariants[0].Execute();
                Assert.IsTrue(playerUnit.CurrentHealth == playerUnit.MaxHealth - (computerUnit.BaseDamage * 2));
            }
        }
    }
}
using NUnit.Framework;


namespace WFS
{
    namespace Tests
    {
        public class TeamTest
        {
            private const int UNIT_MAX_HEALTH = 4;


            [Test]
            public void UnitManagementTest()
            {
                var team = new Team();

                //Adding Units
                var unit_1 = new Unit(UNIT_MAX_HEALTH, 1, 0);
                team.AddUnit(unit_1);
                Assert.IsTrue(team.NumberOfUnits() == 1);
                Assert.IsTrue(team.ContainsUnit(unit_1));
                Assert.IsTrue(unit_1.Team == team);
                Assert.IsTrue(team.UnitsInTeam.Count == 1);
                Assert.IsTrue(team.UnitsInTeam[0].unit == unit_1);
                Assert.IsTrue(team.UnitsInTeam[0].position == Team.Position.Middle);

                var unit_2 = new Unit(UNIT_MAX_HEALTH, 1, 0);
                team.AddUnit(unit_2, Team.Position.Right);
                Assert.IsTrue(team.NumberOfUnits() == 2);
                Assert.IsTrue(team.UnitsInTeam.Count == 2);
                foreach (var unitAndPosition in team.UnitsInTeam)
                {
                    if (unitAndPosition.unit == unit_1)
                    {
                        Assert.IsTrue(unitAndPosition.position == Team.Position.Middle);
                    }
                    else if (unitAndPosition.unit == unit_2)
                    {
                        Assert.IsTrue(unitAndPosition.position == Team.Position.Right);
                    }
                }

                //Swaping units
                team.Swap(Team.Position.Middle, Team.Position.Right);
                foreach (var unitAndPosition in team.UnitsInTeam)
                {
                    if (unitAndPosition.unit == unit_1)
                    {
                        Assert.IsTrue(unitAndPosition.position == Team.Position.Right);
                    }
                    else if (unitAndPosition.unit == unit_2)
                    {
                        Assert.IsTrue(unitAndPosition.position == Team.Position.Middle);
                    }
                }

                //Unit death
                bool teamDestroyed = false;
                team.OnTeamDestroyed += () => { teamDestroyed = true; };
                unit_1.ApplyDamage(UNIT_MAX_HEALTH);
                unit_2.ApplyDamage(UNIT_MAX_HEALTH);
                Assert.IsTrue(team.NumberOfUnits() == 0);
                Assert.IsFalse(team.ContainsUnit(unit_1));
                Assert.IsTrue(unit_1.Team == null);
                Assert.IsTrue(teamDestroyed);
            }
        }
    }
}

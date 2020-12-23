
using NUnit.Framework;


namespace WFS
{
    namespace Tests
    {
        public class CombatTest
        {
            [Test]
            public void TeamManagementTest()
            {
                var combat = new Combat();
                var team_1 = new Team();
                var team_2 = new Team();

                //Swaping player team
                combat.PlayerTeam = team_1;
                combat.PlayerTeam = team_2;
                Assert.IsTrue(team_1.Combat == null);
                Assert.IsTrue(team_2.Combat == combat);
                combat.PlayerTeam = team_1;
                Assert.IsTrue(team_1.Combat == combat);
                Assert.IsTrue(team_2.Combat == null);
                combat.ComputerTeam = team_2;
                Assert.IsTrue(team_1.Combat == combat);
                Assert.IsTrue(team_2.Combat == combat);

                //Getting opposing team
                Assert.IsTrue(combat.GetOpposingTeam(team_1) == team_2);
                Assert.IsTrue(combat.GetOpposingTeam(team_2) == team_1);
            }
        }
    }
}
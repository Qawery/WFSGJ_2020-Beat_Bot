using UnityEngine.Assertions;


namespace WFS
{
    public class Combat
    {
        private const int PLAYER_TEAM_INDEX = 0;
        private const int COMPUTER_TEAM_INDEX = 1;
        private Team[] teams = { null, null };


        public Team PlayerTeam
        {
            get => teams[PLAYER_TEAM_INDEX];

            set
            {
                SwapTeam(PLAYER_TEAM_INDEX, value);
            }
        }

        public Team ComputerTeam
        {
            get => teams[COMPUTER_TEAM_INDEX];

            set
            {
                SwapTeam(COMPUTER_TEAM_INDEX, value);
            }
        }


        public Team GetOpposingTeam(Team myTeam)
        {
            Assert.IsNotNull(myTeam);
            if (myTeam == PlayerTeam)
            {
                return ComputerTeam;
            }
            else
            {
                return PlayerTeam;
            }
        }

        private void SwapTeam(int teamIndex, Team newTeam)
        {
            Assert.IsTrue(teamIndex >= 0 && teamIndex < teams.Length);
            if (teams[teamIndex] != null)
            {
                teams[teamIndex].Combat = null;
            }
            teams[teamIndex] = newTeam;
            if (teams[teamIndex] != null)
            {
                teams[teamIndex].Combat = this;
            }
            for(int i = 0; i < teams.Length; ++i)
            {
                if (teams[i] != null)
                {
                    for (int j = i + 1; j < teams.Length; ++j)
                    {
                        Assert.IsTrue(teams[i] != teams[j]);
                    }
                }
            }
        }
    }
}
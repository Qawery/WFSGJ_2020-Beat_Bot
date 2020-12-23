using System.Collections.Generic;


namespace WFS
{
    //Issuers
    public interface ITeamIssuedCombatAction
    {
        Team Issuer { get; }
    }


    public interface IUnitIssuedCombatAction
    {
        Unit Issuer { get; }
    }


    //Targets
    public interface ICombatTargetCombatAction
    {
    }


    public interface ITeamTargetCombatAction
    {
        List<Team> PotentialTargets { get; }
        Team Target { get; set; }
    }


    public interface IUnitTargetCombatAction
    {
        List<Unit> PotentialTargets { get; }
        Unit Target { get; set; }
    }
}
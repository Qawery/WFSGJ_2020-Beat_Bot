using UnityEngine.Assertions;
using System.Collections.Generic;
using System;


namespace WFS
{
    public class Team
    {
        public enum Position
        {
            Middle = 0, Left = 1, Right = 2
        }


        private Unit[] units;
        private Combat combat = null;


        public event Action OnTeamDestroyed;


        public static int MaxUnitsInTeam => Enum.GetNames(typeof (Position)).Length;
        public List<(Unit unit, Position position)> UnitsInTeam
        {
            get
            {
                var result = new List<(Unit unit, Position position)>();
                for (int i = 0; i < units.Length; ++i)
                {
                    if (units[i] != null)
                    {
                        result.Add((units[i], (Position) i));
                    }
                }
                return result;
            }
        }

        public Combat Combat
        {
            get => combat;

            set
            {
                combat = value;
            }
        }


        public Team()
        {
            units = new Unit[MaxUnitsInTeam];
        }

        public void AddUnit(Unit unit)
        {
            Assert.IsTrue(NumberOfUnits() < MaxUnitsInTeam);
            for (int i = 0; i < units.Length; ++i)
            {
                if (units[i] == null)
                {
                    AddUnit(unit, (Position) i);
                    return;
                }
            }
        }

        public void AddUnit(Unit unit, Position position)
        {
            Assert.IsFalse(ContainsUnit(unit));
            Assert.IsNull(units[(int) position]);
            units[(int)position] = unit;
            unit.Team = this;
            unit.OnDeath += OnUnitDied;
        }

        public void Swap(Position position_1, Position position_2)
        {
            Assert.IsTrue(position_1 != position_2);
            Assert.IsTrue(units[(int) position_1] != null || units[(int) position_2] != null);
            var unit = units[(int)position_1];
            units[(int)position_1] = units[(int)position_2];
            units[(int)position_2] = unit;
        }

        public void RemoveUnit(Unit unit)
        {
            Assert.IsTrue(ContainsUnit(unit));
            for (int i = 0; i < units.Length; ++i)
            {
                if (units[i] == unit)
                {
                    units[i].Team = null;
                    units[i].OnDeath -= OnUnitDied;
                    units[i] = null;
                }
            }
        }

        public bool ContainsUnit(Unit unit)
        {
            Assert.IsNotNull(unit);
            for (int i = 0; i < units.Length; ++i)
            {
                if (units[i] == unit)
                {
                    return true;
                }
            }
            return false;
        }

        public int NumberOfUnits()
        {
            int result = 0;
            for (int i = 0; i < units.Length; ++i)
            {
                if (units[i] != null)
                {
                    ++result;
                }
            }
            return result;
        }

        private void OnUnitDied(Unit deadUnit)
        {
            RemoveUnit(deadUnit);
            if (NumberOfUnits() == 0)
            {
                OnTeamDestroyed?.Invoke();
            }
        }
    }
}

using UnityEngine;
using UnityEngine.Assertions;


namespace WFS
{
    public class BoardPositions : MonoBehaviour
    {
        [SerializeField] private Transform[] positions;


        public Transform[] Positions => positions;


        private void Awake()
        {
            Assert.IsTrue(positions.Length == Team.MaxUnitsInTeam);
            for (int i = 0; i < positions.Length; ++i)
            {
                Assert.IsNotNull(positions[i]);
                for (int j = i + 1; j < positions.Length; ++j)
                {
                    Assert.IsFalse(positions[i] == positions[j]);
                }
            }
        }
    }
}
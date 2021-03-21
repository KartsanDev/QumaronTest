using UnityEngine;

namespace Kartsan.Text.Qumaron
{
    [System.Serializable]
    public class MoveData
    {
        [SerializeField] private float _minMoveSpeed;
        [SerializeField] private float _maxMoveSpeed;
        [SerializeField] private float _boostSpeed;

        public float GetMinMoveSpeed => _minMoveSpeed;
        public float GetMaxMoveSpeed => _maxMoveSpeed;
        public float GetBoostSpeed => _boostSpeed;
    }
}
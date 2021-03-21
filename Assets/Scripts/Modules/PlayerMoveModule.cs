using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kartsan.Text.Qumaron
{
    public class PlayerMoveModule : MoveModule
    {
        private const float MOVE_ACCURACY = 0.005f;

        private MonoBehaviour _playersMonoBehaviour;
        private List<Vector3> _actualMovePointsList;
        private float _boostDistance;
        private float _lastMaxSpeed;
        private float _actualSpeed;
        private bool _moving;

        public PlayerMoveModule(Transform playerTransform, ref MoveData moveData) :
        base(playerTransform, ref moveData)
        {
            _playersMonoBehaviour = playerTransform.GetComponent<MonoBehaviour>();
            _actualMovePointsList = new List<Vector3>();
            _moving = false;
        }

        public override void AddMovePoint(Vector2 pointPos)
        {
            _actualMovePointsList.Add(pointPos);

            if (!_moving) _playersMonoBehaviour.StartCoroutine(MoveProcessCoroutine());
        }

        private IEnumerator MoveProcessCoroutine()
        {
            float distanceToPoint;
            var startPosition = playerTransform.position;

            _actualSpeed = moveData.GetMinMoveSpeed;
            _lastMaxSpeed = _actualSpeed;
            _boostDistance = 0;
            _moving = true;

            for (int pointIndex = 0; pointIndex < _actualMovePointsList.Count; pointIndex++)
            {
                do
                {
                    playerTransform.position = GetNextMovePoint(startPosition, pointIndex);
                    distanceToPoint = Vector2.Distance(playerTransform.position, _actualMovePointsList[pointIndex]);

                    yield return null;
                }
                while (distanceToPoint > MOVE_ACCURACY);
            }

            _actualMovePointsList.Clear();
            _moving = false;
        }

        private Vector2 GetNextMovePoint(Vector2 startPos, int actualIndex)
        {
            float fullDistance = DistanceCalculator.CalcFullDistance(startPos, _actualMovePointsList);
            float readyDistance = DistanceCalculator.CalcReadyDistance(startPos, playerTransform.position, _actualMovePointsList, actualIndex);

            if (fullDistance - readyDistance <= _boostDistance) Deceleration();
            else Acceleration();

            var nextMovePoint = Vector2.MoveTowards(playerTransform.position, _actualMovePointsList[actualIndex], _actualSpeed * Time.deltaTime);

            if (_lastMaxSpeed < _actualSpeed)
            {
                _lastMaxSpeed = _actualSpeed;
                _boostDistance += Vector2.Distance(playerTransform.position, nextMovePoint);
            }

            return nextMovePoint;
        }

        private void Acceleration()
        {
            _actualSpeed = _actualSpeed >= moveData.GetMaxMoveSpeed
                                         ? moveData.GetMaxMoveSpeed
                                         : _actualSpeed + moveData.GetBoostSpeed;
        }

        private void Deceleration()
        {
            _actualSpeed = _actualSpeed <= moveData.GetMinMoveSpeed
                                         ? moveData.GetMinMoveSpeed
                                         : _actualSpeed - moveData.GetBoostSpeed;
        }
    }
}
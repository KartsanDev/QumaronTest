using System.Collections.Generic;
using UnityEngine;

namespace Kartsan.Text.Qumaron
{
    public static class DistanceCalculator
    {
        /// <summary>
        /// ������� ��������� ��������� �� ���� ������ �� ��������� �������
        /// </summary>
        /// <param name="startPos">���������� ��������� �������</param>
        /// <param name="actualIndex">������ �����, � ������� � ������ ������ �������� ������</param>
        /// <param name="movePoints">���������� ����� ����������</param>
        public static float CalcReadyDistance(Vector2 startPos, Vector2 playerPos, List<Vector3> movePoints, int actualIndex)
        {
            if (actualIndex == 0)
                return Vector2.Distance(startPos, playerPos);

            var distance = Vector2.Distance(startPos, movePoints[0]);

            distance += CalcIntermediatePointsDistance(actualIndex, movePoints);

            if (actualIndex >= 1)
                distance += Vector2.Distance(movePoints[actualIndex - 1], playerPos);

            return distance;
        }

        /// <summary>
        /// ������� ������ ��������� �� ���� ������ �� ��������� �������
        /// </summary>
        /// <param name="startPos">���������� ��������� �������</param>
        /// <param name="movePoints">���������� ����� ����������</param>
        public static float CalcFullDistance(Vector2 startPos, List<Vector3> movePoints)
        {
            float distance = Vector2.Distance(startPos, movePoints[0]);

            distance += CalcIntermediatePointsDistance(movePoints.Count, movePoints);

            return distance;
        }

        /// <summary>
        /// ������� ���������� ����� �������������� ������� (�� ������� ������)
        /// </summary>
        /// <param name="pointsCount">���������� ������������� �����</param>
        /// <param name="movePoints">���������� ����� ����������</param>
        public static float CalcIntermediatePointsDistance(int pointsCount, List<Vector3> movePoints)
        {
            float distance = 0;

            if (movePoints.Count < pointsCount) return distance;

            for (int i = 1; i < pointsCount; i++)
                distance += Vector2.Distance(movePoints[i - 1], movePoints[i]);

            return distance;
        }
    }
}
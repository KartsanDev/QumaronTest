using System.Collections.Generic;
using UnityEngine;

namespace Kartsan.Text.Qumaron
{
    public static class DistanceCalculator
    {
        /// <summary>
        /// Рассчет пройденой дистанции по всем точкам от стартовой позиции
        /// </summary>
        /// <param name="startPos">Координаты стартовой позиции</param>
        /// <param name="actualIndex">Индекс точки, к которой в данный момент движется объект</param>
        /// <param name="movePoints">Актуальные точки следования</param>
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
        /// Рассчет полной дистанции по всем точкам от стартовой позиции
        /// </summary>
        /// <param name="startPos">Координаты стартовой позиции</param>
        /// <param name="movePoints">Актуальные точки следования</param>
        public static float CalcFullDistance(Vector2 startPos, List<Vector3> movePoints)
        {
            float distance = Vector2.Distance(startPos, movePoints[0]);

            distance += CalcIntermediatePointsDistance(movePoints.Count, movePoints);

            return distance;
        }

        /// <summary>
        /// Рассчет расстояния между промежуточными точками (не включет первую)
        /// </summary>
        /// <param name="pointsCount">Количество промежуточных точек</param>
        /// <param name="movePoints">Актуальные точки следования</param>
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
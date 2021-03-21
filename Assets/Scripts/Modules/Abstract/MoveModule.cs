using UnityEngine;

namespace Kartsan.Text.Qumaron
{
    public abstract class MoveModule
    {
        protected Transform playerTransform;
        protected MoveData moveData;

        public MoveModule(Transform playerTransform, ref MoveData moveData)
        {
            this.playerTransform = playerTransform;
            this.moveData = moveData;
        }

        public abstract void AddMovePoint(Vector2 pointPos);
    }
}
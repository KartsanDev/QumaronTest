using UnityEngine;

namespace Kartsan.Text.Qumaron
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private MoveData _moveData;

        private MoveModule _moveModule;

        private void Awake() => _moveModule = new PlayerMoveModule(transform, ref _moveData);

        private void OnEnable() => InputManager.OnLeftMouseButtonClick += LeftMouseButtonClickHandler;

        private void OnDisable() => InputManager.OnLeftMouseButtonClick -= LeftMouseButtonClickHandler;

        private void LeftMouseButtonClickHandler(Vector2 mousePosition) => _moveModule.AddMovePoint(mousePosition);
    }
}
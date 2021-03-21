using System;
using UnityEngine;

namespace Kartsan.Text.Qumaron
{
    public class InputManager : MonoBehaviour
    {
        public static Action<Vector2> OnLeftMouseButtonClick;

        [SerializeField] private Camera _camera;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                LeftMouseButtonClick();
        }

        private void LeftMouseButtonClick()
        {
            Vector2 worldClickPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            OnLeftMouseButtonClick?.Invoke(worldClickPosition);
        }
    }
}
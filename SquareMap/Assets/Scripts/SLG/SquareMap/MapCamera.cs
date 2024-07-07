using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace JoyNow.SLG
{
    public class MapCamera : MonoBehaviour
    {
        public const float moveSpeed = 0.1f;
        public const float cameraMinSize = 5;
        public const float cameraMaxSize = 100;
        // 缩放时同时旋转角度，越远越平
        public const float swivelMinZoom = 45;
        public const float swivelMaxZoom = 45;
        private float zoom = 0.2f;
        public SquareGrid grid;
        
        private Transform swivel;
        private Transform stick;

        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
            swivel = transform.GetChild(0);
            stick = swivel.GetChild(0);
            AdjustZoom(zoom);
        }
        
        private void Update()
        {
            var zoomDelta = Input.GetAxis("Mouse ScrollWheel");
            if (Math.Abs(zoomDelta) > 0.0001f)
            {
                zoom = Mathf.Clamp01(zoom + zoomDelta);
                AdjustZoom(zoom);
            }
            
            float xDelta = Input.GetAxis("Horizontal");
            float yDelta = Input.GetAxis("Vertical");
            if (xDelta != 0 || yDelta != 0)
            {
                AdjustPosition(xDelta, yDelta);
            }
        }
        
        private void AdjustZoom(float zoom)
        {
            float size = Mathf.Lerp(cameraMinSize, cameraMaxSize, zoom);
            mainCamera.orthographicSize = size;
            
            float angle = Mathf.Lerp(swivelMinZoom, swivelMaxZoom, zoom);
            swivel.localRotation = Quaternion.Euler(angle, -45f, 0);
        }
        
        private void AdjustPosition(float xDelta, float yDelta)
        {
            Vector3 offset = new Vector3(xDelta, yDelta, 0) * (mainCamera.orthographicSize * moveSpeed);
            Vector3 position = stick.localPosition + offset;
            stick.localPosition = ClampPosition(position);
        }
        
        private Vector3 ClampPosition(Vector3 position)
        {
            float xMin = -MapMetrics.HalfCellDiagonalLength;
            float xMax = Mathf.Sqrt(grid.GridWidth * grid.GridWidth + grid.GridHeight * grid.GridHeight) - MapMetrics.HalfCellDiagonalLength;
            position.x = Mathf.Clamp(position.x, xMin, xMax);
            return position;
        }
    }
}
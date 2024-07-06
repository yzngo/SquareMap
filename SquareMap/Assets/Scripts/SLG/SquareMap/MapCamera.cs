using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace JoyNow.SLG
{
    public class MapCamera : MonoBehaviour
    {
        public float moveSpeed = 1;
        public float cameraMinSize = 10;
        public float cameraMaxSize = 33;
        public float swivelMinZoom = 70;
        public float swivelMaxZoom = 45;
        public float zoom = 0.2f;
        
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
            stick.localPosition += new Vector3(xDelta, yDelta, 0) * moveSpeed;
        }
    }
}
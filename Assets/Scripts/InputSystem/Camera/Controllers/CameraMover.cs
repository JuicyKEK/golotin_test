using InputSystem.CameraControllers.Interfaces;
using InputSystem.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace InputSystem.CameraControllers
{
    public class CameraMover : MonoBehaviour
    {
        private const float TAP_TIME_THRESHOLD = 0.2f;
        private const float TAP_DISTANCE_THRESHOLD = 50f;
        private const float DRUG_MAGNITUDE = 0.1f;
        private const float ZOOM_COEFICENT = 0.1f;
        
        public event UnityAction<Vector3> m_OnScreenTap;
    
        [Header("Camera Settings")]
        [SerializeField] private float m_MoveSpeed = 10f;
        [SerializeField] private float m_VerticalMoveMultiplier = 2f;
        [SerializeField] private float m_HorizontalMoveMultiplier = 1f;
        [SerializeField] private float m_ZoomSpeed = 2f;
        [SerializeField] private float m_MinZoom = 2f;
        [SerializeField] private float m_MaxZoom = 8f;
        [SerializeField] private Quaternion m_InitialRotation = Quaternion.Euler(45f, 0, 0f);
        [SerializeField] private bool m_IsOrthographic;
        
        [Header("Perspective Camera Settings")]
        [SerializeField] private float m_MinFieldOfView = 20f;
        [SerializeField] private float m_MaxFieldOfView = 80f;

        [Header("Camera Bounds")]
        [SerializeField] private bool m_UseBounds = true;
        [SerializeField] private Vector2 m_BoundsX = new Vector2(-50f, 50f);
        [SerializeField] private Vector2 m_BoundsZ = new Vector2(-50f, 50f);
    
        private Camera m_CameraComponent;
        private Vector3 m_LastScreenPosition;
        private bool m_IsDragging;
        private float m_TouchStartTime;
        private Vector3 m_TouchStartPosition;
        
        private ICameraMovementAction m_CameraMovementAction;
        private IScreenTapAction m_ScreenTapAction;
        
        public void Init(Camera camera, ICameraMovementAction cameraMovementAction, IScreenTapAction tapAction)
        {
            m_CameraComponent = camera;
            m_CameraMovementAction = cameraMovementAction;
            m_ScreenTapAction = tapAction;
            SetupIsometricView();
            SubscribeInputEvents();
        }
    
        private void SetupIsometricView()
        {
            transform.rotation = m_InitialRotation;
            m_CameraComponent.orthographic = m_IsOrthographic;
            
            if (m_IsOrthographic)
            {
                m_CameraComponent.orthographicSize = m_MaxZoom;
            }
            else
            {
                m_CameraComponent.fieldOfView = m_MaxFieldOfView;
            }
        }
        
        private void SubscribeInputEvents()
        {
            m_CameraMovementAction.OnTouchStartSubscribe(OnTouchBegan);
            m_CameraMovementAction.OnTouchEndSubscribe(OnTouchEnded);
            m_CameraMovementAction.OnDragSubscribe(OnTouchMoved);
            m_CameraMovementAction.OnScrollSubscribe(ZoomCamera);
        }
        
        private void OnTouchBegan(Vector3 screenPosition)
        {
            m_TouchStartTime = Time.time;
            m_TouchStartPosition = screenPosition;
            m_LastScreenPosition = screenPosition;
            m_IsDragging = false;
        }
    
        private void OnTouchMoved(Vector3 screenPosition)
        {
            Vector3 screenDelta = m_LastScreenPosition - screenPosition;
            
            if (Vector3.Distance(m_TouchStartPosition, screenPosition) > TAP_DISTANCE_THRESHOLD)
            {
                m_IsDragging = true;
                
                if (screenDelta.magnitude > DRUG_MAGNITUDE)
                {
                    Vector3 worldDelta = ScreenDeltaToWorldDelta(screenDelta);
                    MoveCamera(worldDelta);
                }
            }
            
            m_LastScreenPosition = screenPosition;
        }
    
        private void OnTouchEnded(Vector3 screenPosition)
        {
            float touchDuration = Time.time - m_TouchStartTime;
            float touchDistance = Vector3.Distance(m_TouchStartPosition, screenPosition);
            
            if (!m_IsDragging && touchDuration < TAP_TIME_THRESHOLD && touchDistance < TAP_DISTANCE_THRESHOLD)
            {
                HandleScreenTap(screenPosition);
            }
        
            m_IsDragging = false;
        }
    
        private void HandleScreenTap(Vector3 screenPosition)
        {
            m_ScreenTapAction.OnScreenTap(screenPosition);
        }
    
        private void MoveCamera(Vector3 delta)
        {
            Vector3 newPosition = m_CameraComponent.transform.position + delta;

            if (m_UseBounds)
            {
                newPosition.x = Mathf.Clamp(newPosition.x, m_BoundsX.x, m_BoundsX.y);
                newPosition.z = Mathf.Clamp(newPosition.z, m_BoundsZ.x, m_BoundsZ.y);
            }
        
            m_CameraComponent.transform.position = newPosition;
        }
    
        private void ZoomCamera(float zoomDelta)
        {
            if (m_IsOrthographic)
            {
                float newSize = m_CameraComponent.orthographicSize + zoomDelta * m_ZoomSpeed;
                m_CameraComponent.orthographicSize = Mathf.Clamp(newSize, m_MinZoom, m_MaxZoom);
            }
            else
            {
                float newFieldOfView = m_CameraComponent.fieldOfView + zoomDelta * m_ZoomSpeed * 10f;
                m_CameraComponent.fieldOfView = Mathf.Clamp(newFieldOfView, m_MinFieldOfView, m_MaxFieldOfView);
            }
        }
        
        private Vector3 ScreenDeltaToWorldDelta(Vector3 screenDelta)
        {
            float worldUnitsPerScreenPixel;
            float screenHeight = Screen.height;
            
            if (m_IsOrthographic)
            {
                float orthographicSize = m_CameraComponent.orthographicSize;
                worldUnitsPerScreenPixel = (orthographicSize * 2f) / screenHeight;
            }
            else
            {
                float distanceToPlane = m_CameraComponent.transform.position.y;
                float halfFOV = m_CameraComponent.fieldOfView * 0.5f * Mathf.Deg2Rad;
                float halfHeight = distanceToPlane * Mathf.Tan(halfFOV);
                worldUnitsPerScreenPixel = (halfHeight * 2f) / screenHeight;
            }
            
            float deltaX = screenDelta.x * worldUnitsPerScreenPixel * m_MoveSpeed * ZOOM_COEFICENT * m_HorizontalMoveMultiplier;
            float deltaY = screenDelta.y * worldUnitsPerScreenPixel * m_MoveSpeed * ZOOM_COEFICENT * m_VerticalMoveMultiplier;

            Vector3 worldDelta = new Vector3(
                deltaX,
                0,
                deltaY
            );

            return worldDelta;
        }

        private void OnDestroy()
        {
            m_OnScreenTap = null;
        }
    }
}

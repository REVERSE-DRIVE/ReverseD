using Cinemachine;
using UnityEngine;

namespace MainLoby
{
    public class CameraManager : MonoSingleton<CameraManager>
    {
        [SerializeField] private GameObject _followTarget;
        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        public bool IsOn { get; set; }

        private void Awake()
        {
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Raycast();
            }
        }

        private void Raycast()
        {
            if (IsOn) return;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            _followTarget.transform.localPosition = hit.point;
        }
    }

}

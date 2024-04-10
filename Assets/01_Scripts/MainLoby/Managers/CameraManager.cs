using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace MainLoby
{
    public class CameraManager : MonoSingleton<CameraManager>
    {
        [SerializeField] private GameObject _followTarget;
        private CinemachineVirtualCamera cinemachineVirtualCamera;
        public bool IsOn { get; set; }

        private void Awake()
        {
            cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
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
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            _followTarget.transform.localPosition = hit.point;
        }
    }

}

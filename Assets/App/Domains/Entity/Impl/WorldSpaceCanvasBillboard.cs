using System;
using UnityEngine;

namespace App.Domains.Entity.Impl
{
    public class WorldSpaceCanvasBillboard : MonoBehaviour
    {
        private Camera? _targetCamera;

        private void Start() =>
            _targetCamera = Camera.main;

        private void LateUpdate()
        {
            if (_targetCamera == null)
            {
                return;
            }

            Vector3 direction = transform.position - _targetCamera.transform.position;

            if (direction.sqrMagnitude > 0.0001f)
            {
                transform.rotation = Quaternion.LookRotation(direction, _targetCamera.transform.up);
            }
        }
    }
}
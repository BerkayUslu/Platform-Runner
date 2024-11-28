using System.Collections;
using UnityEngine;

namespace PlatformRunner
{
    public class OnRotatingPlatform : MonoBehaviour
    {
        [SerializeField] private float _rayLength = 1f;
        [SerializeField] private int _checkInterval = 5;

        private Transform _transform;
        private WaitForSeconds _parentNullDelay;
        private Coroutine _setParentNull;
        private int _frameCounter;
        private RaycastHit _hit;

        private void Start()
        {
            _transform = transform;
            _parentNullDelay = new WaitForSeconds(0.1f);
            _frameCounter = 0;
        }

        private void FixedUpdate()
        {
            _frameCounter++;

            if (_frameCounter >= _checkInterval)
            {
                _frameCounter = 0;
                CheckGround();
            }
        }

        private void CheckGround()
        {
            if (Physics.Raycast(_transform.position, Vector3.down, out _hit, _rayLength))
            {
                if (_hit.collider.CompareTag(Tags.RotatingPlatform))
                {
                    if (_setParentNull != null)
                    {
                        StopCoroutine(_setParentNull);
                    }
                    _transform.parent = _hit.transform;
                }
                else if (_transform.parent != null)
                {
                    _setParentNull = StartCoroutine(SetTransformNull());
                }
            }
            else if (_transform.parent != null)
            {
                _setParentNull = StartCoroutine(SetTransformNull());
            }
        }

        private IEnumerator SetTransformNull()
        {
            yield return _parentNullDelay;
            _transform.parent = null;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, Vector3.down * _rayLength);
        }
    }
}

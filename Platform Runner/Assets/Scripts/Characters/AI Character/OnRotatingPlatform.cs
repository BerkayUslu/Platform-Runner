using System.Collections;
using UnityEngine;

namespace PlatformRunner
{
    public class OnRotatingPlatform : MonoBehaviour
    {
        [SerializeField] private float _rayLength = 1f;
        [SerializeField] private int _checkInterval = 5;

        private Transform _transform;
        private Transform _initialParent;
        private WaitForSeconds _initialParentDelay;
        private Coroutine _setParentInitial;
        private int _frameCounter;
        private RaycastHit _hit;

        private void Start()
        {
            _initialParent = transform.parent;
            _transform = transform;
            _initialParentDelay = new WaitForSeconds(0.1f);
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
                    if (_setParentInitial != null)
                    {
                        StopCoroutine(_setParentInitial);
                    }
                    _transform.parent = _hit.transform;
                }
                else if (_transform.parent != null)
                {
                    _setParentInitial = StartCoroutine(SetParentToInitial());
                }
            }
            else if (_transform.parent != null)
            {
                _setParentInitial = StartCoroutine(SetParentToInitial());
            }
        }

        private IEnumerator SetParentToInitial()
        {
            yield return _initialParentDelay;
            _transform.parent = _initialParent;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, Vector3.down * _rayLength);
        }
    }
}

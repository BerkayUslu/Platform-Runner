using System.Collections;
using System.Collections.Generic;
using PlatformRunner;
using UnityEngine;

public class OnRotatingPlatform : MonoBehaviour

{
    private Transform _transform;
    private WaitForSeconds _waitForSeconds;
    private Coroutine _setParentNull;

    private void Start()
    {
        _transform = transform;
        _waitForSeconds = new WaitForSeconds(0.1f);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(Tags.RotatingPlatform))
        {
            if (_setParentNull != null)
                StopCoroutine(_setParentNull);
            _transform.parent = collision.transform;
        }

    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag(Tags.RotatingPlatform))
        {
            _setParentNull = StartCoroutine("SetTransformNull");
        }
    }

    private IEnumerator SetTransformNull()
    {
        yield return _waitForSeconds;
        _transform.parent = null;
    }
}

using UnityEngine;

public static class TransformExtensions
{
    public static void SetX(this Transform transform, float x)
    {
        Vector3 pos = transform.position;
        pos.x = x;
        transform.position = pos;
    }
}
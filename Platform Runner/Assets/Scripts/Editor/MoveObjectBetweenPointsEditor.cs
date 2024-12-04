using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PlatformRunner
{
    [CustomEditor(typeof(MoveObjectBetweenPoints)), CanEditMultipleObjects]
    public class MoveObjectBetweenPointsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            MoveObjectBetweenPoints script = (MoveObjectBetweenPoints)target;

            if (GUILayout.Button("Restart Movement"))
            {
                script.RestartMovement();
            }
        }
    }
}
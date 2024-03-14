using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerCustomEditor : Editor
{
    private VariableJoystick _joystick;
    private PlayerController _playerController;
    
    private void OnEnable()
    {
        _playerController = (PlayerController)target;
        _joystick = _playerController.Joystick;
    }
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        if (GUILayout.Button("Joystick Find"))
        {
            _joystick = FindObjectOfType<VariableJoystick>();
            _playerController.Joystick = _joystick;
        }
    }
}

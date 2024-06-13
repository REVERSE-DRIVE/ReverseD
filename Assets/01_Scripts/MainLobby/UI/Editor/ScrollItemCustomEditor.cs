using UnityEditor;

[CustomEditor(typeof(ScrollItem))]
public class ScrollItemCustomEditor : Editor
{
    private SerializedProperty _partType;
    private SerializedProperty _axisType;
    private SerializedProperty _bodyType;
    private SerializedProperty _legType;
    
    private void OnEnable()
    {
        _partType = serializedObject.FindProperty("_partType");
        _axisType = serializedObject.FindProperty("_axisType");
        _bodyType = serializedObject.FindProperty("_bodyType");
        _legType = serializedObject.FindProperty("_legType");
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_partType);
        
        switch ((PartType)_partType.enumValueIndex)
        {
            case PartType.Axis:
                EditorGUILayout.PropertyField(_axisType);
                break;
            case PartType.Body:
                EditorGUILayout.PropertyField(_bodyType);
                break;
            case PartType.Leg:
                EditorGUILayout.PropertyField(_legType);
                break;
        }
        serializedObject.ApplyModifiedProperties();
    }
}
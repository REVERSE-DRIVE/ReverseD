using UnityEditor;
using UnityEngine;

namespace UIManage
{

    [CustomEditor(typeof(UIInfo))]
    [CanEditMultipleObjects]
    public class UIInfoInspector : Editor
    {
        private Texture2D m_Logo;
        private Color m_Color = new Color(0.2f, 0.2f, 0.2f);

        #region bool

        private bool showUI = true;
        private bool showPos = true;
        private bool showButton = true;
        private bool showColor = true;
        private bool showScale = true;

        #endregion

        public override void OnInspectorGUI()
        {
            // EditorGUI.BeginHorizontal();과 EditorGUI.EndHorizontal(); 사이에 들어가는 GUI들은 가로로 정렬
            // EditorGUI.BeginVertical();과 EditorGUI.EndVertical(); 사이에 들어가는 GUI들은 세로로 정렬
            UIInfo uiInfo = (UIInfo)target;
            Rect rect = EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();

            #region Color

            EditorGUI.DrawRect(rect, m_Color);

            #endregion

            #region GUIStyle

            GUIStyle labelStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                normal =
                {
                    textColor = new Color(.6f, .6f, .6f)
                }
            };

            #endregion

            #region Logo

            // EditorGUIUtility.Load : 이미지를 불러옴
            m_Logo = EditorGUIUtility.Load("Assets/Resources/SJH.png") as Texture2D;
            // GUILayout.Label : 이미지 또는 글자를 띄워줌
            GUILayout.Label(new GUIContent(m_Logo), GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label("UI Manager <Made By vcsHB>", labelStyle, GUILayout.Height(50));
            GUILayout.Space(10);

            #endregion

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginVertical();

            #region Property List

            // EditorGUILayout.Foldout : 접기/펼치기 버튼
            showUI = EditorGUILayout.Foldout(showUI, "  \u25bc UI Information", true, labelStyle);
            if (showUI)
            {
                // EditorGUILayout.PropertyField(serializedObject.FindProperty("UIName")); // UIName 변수를 찾아서 PropertyField로 만들어줌
                EditorGUILayout.PropertyField(serializedObject.FindProperty("UIName"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("UIRectObject"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("UITrnsObject"));
                // serializedObject.ApplyModifiedProperties(); : 변경된 값을 적용
                serializedObject.ApplyModifiedProperties();
            }

            // EditorGUILayout.Space(10); : 10만큼 띄워줌
            EditorGUILayout.Space(10);

            showPos = EditorGUILayout.Foldout(showPos, "  \u25bc UI PositionEffect", true, labelStyle);
            if (showPos)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("usePositionEffect"));
                serializedObject.ApplyModifiedProperties();

                if (uiInfo.usePositionEffect)
                {

                    EditorGUILayout.PropertyField(serializedObject.FindProperty("DefaultPos"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("TargetPos"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("duration"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("EaseEffect"));
                    EditorGUILayout.Space(10);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("onOff"));
                    serializedObject.ApplyModifiedProperties();
                }
            }

            EditorGUILayout.Space(10);

            showScale = EditorGUILayout.Foldout(showPos, "  \u25bc UI ScaleEffect", true, labelStyle);
            if (showScale)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("useScaleEffect"));
                serializedObject.ApplyModifiedProperties();

                if (uiInfo.useScaleEffect)
                {

                    EditorGUILayout.PropertyField(serializedObject.FindProperty("DefaultScale"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("TargetScale"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("ScaleEffectDuration"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("ScaleEaseEffect"));
                    EditorGUILayout.Space(10);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("scaleEffectOnOff"));
                    serializedObject.ApplyModifiedProperties();
                }
            }

            EditorGUILayout.Space(10);


            showColor = EditorGUILayout.Foldout(showColor, "  \u25bc UI ColorEffect", true, labelStyle);
            if (showColor)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("useColorEffect"));
                serializedObject.ApplyModifiedProperties();

                if (uiInfo.useColorEffect)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("DefaultColor"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("TargetColor"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("ColorEffectDuration"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("ColorEaseEffect"));
                    EditorGUILayout.Space(10);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("colorOnOff"));
                    serializedObject.ApplyModifiedProperties();
                }
            }

            EditorGUILayout.Space(10);

            showButton = EditorGUILayout.Foldout(showButton, "  \u25bc UI Mouse Event", true, labelStyle);
            if (showButton)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("isButton"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("isDetectMouse"));
                serializedObject.ApplyModifiedProperties();

                EditorGUILayout.Space(10);
                if (uiInfo.isButton)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("OnClick"));
                    serializedObject.ApplyModifiedProperties();
                }

                if (uiInfo.isDetectMouse)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("OnMouseEnter"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("OnMouseExit"));
                    serializedObject.ApplyModifiedProperties();
                }
            }

            #endregion

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndVertical();
        }
    }
}
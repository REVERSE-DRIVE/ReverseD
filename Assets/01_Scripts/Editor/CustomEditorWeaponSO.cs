﻿using UnityEditor;
using UnityEngine;

namespace AttackManage
{
    [CustomEditor(typeof(WeaponSO))]
    public class CustomEditorWeaponSO : Editor
    {

        private SerializedProperty rankProp;
        private SerializedProperty idProp;

        private SerializedProperty weaponTypeProp;
        private SerializedProperty weaponNameProp;
        private SerializedProperty descriptionProp;

        private SerializedProperty damageProp;
        private SerializedProperty attackCooltimeProp;
        private SerializedProperty attackTimeProp;
        private SerializedProperty attackRangeProp;
        private SerializedProperty isNockBackProp;
        private SerializedProperty knockBackPowerProp;

        private SerializedProperty weaponPrefabProp;
        
        private void OnEnable()
        {

            idProp = serializedObject.FindProperty("id");
            rankProp = serializedObject.FindProperty("rank");
            
            weaponTypeProp = serializedObject.FindProperty("weaponType");
            weaponNameProp = serializedObject.FindProperty("weaponName");
            descriptionProp = serializedObject.FindProperty("description");
            
            damageProp = serializedObject.FindProperty("damage");
            attackCooltimeProp = serializedObject.FindProperty("attackCooltime");
            attackTimeProp = serializedObject.FindProperty("attackTime");
            
            attackRangeProp = serializedObject.FindProperty("attackRange");
            isNockBackProp = serializedObject.FindProperty("isKnockBack");
            knockBackPowerProp = serializedObject.FindProperty("knockBackPower");
            
            weaponPrefabProp = serializedObject.FindProperty("weaponPrefab");
        
        
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.PropertyField(idProp);
                
                EditorGUILayout.Space(10);

                EditorGUILayout.PropertyField(weaponTypeProp);
                EditorGUILayout.PropertyField(rankProp);
                EditorGUI.BeginChangeCheck(); // 변경을 체크
                string prevName = weaponNameProp.stringValue;
                // 엔터가 쳐지거나 포커스가 나갈때 까지 변경을 저장하지 않는다
                EditorGUILayout.DelayedTextField(weaponNameProp);

                if (EditorGUI.EndChangeCheck())
                {
                    // 현재 편집중인 에셋의 경로를 알아내기
                    string assetPath = AssetDatabase.GetAssetPath(target);
                    string newName = $"Weapon_{weaponNameProp.stringValue}";
                    serializedObject.ApplyModifiedProperties();

                    string msg = AssetDatabase.RenameAsset(assetPath, newName);
                    if (string.IsNullOrEmpty(msg))
                    {
                        // 성공적으로 파일명을 변경함
                        target.name = newName;
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.EndHorizontal();
                        return;
                    }

                    // ㅈ박음
                    weaponNameProp.stringValue = prevName;

                }

                EditorGUILayout.PropertyField(descriptionProp);
                EditorGUILayout.Space(10);

                EditorGUILayout.PropertyField(damageProp);
                EditorGUILayout.PropertyField(attackRangeProp);    
                EditorGUILayout.PropertyField(attackCooltimeProp);
                EditorGUILayout.PropertyField(attackTimeProp);
                EditorGUILayout.PropertyField(isNockBackProp);
                if (isNockBackProp.boolValue)
                {
                    EditorGUILayout.PropertyField(knockBackPowerProp);

                }

                EditorGUILayout.Space(15);
                EditorGUILayout.PropertyField(weaponPrefabProp);

            }
            
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical();
            {
                if (GUILayout.Button("Weapon Setting"))
                {
                    WeaponSettingWithExcel();
                }
            }
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();

        }

        private void WeaponSettingWithExcel()
        {
            ExcelImporter.ImportExcel(idProp.intValue);
            weaponNameProp.stringValue = ExcelImporter.Weapon_Name;
            damageProp.intValue = int.Parse(ExcelImporter.Weapon_Damage);
            attackCooltimeProp.floatValue = float.Parse(ExcelImporter.Weapon_AttackSpeed);
            attackRangeProp.floatValue = float.Parse(ExcelImporter.Weapon_Range.Replace(" Tile", ""));
            //rankProp.enumValueIndex = (int) Enum.Parse(typeof(Rank), ExcelImporter.Weapon_Rank);
            //weaponTypeProp.enumValueIndex = (int) Enum.Parse(typeof(WeaponType), ExcelImporter.Weapon_Type);
            serializedObject.ApplyModifiedProperties();
        }
    }
    
}
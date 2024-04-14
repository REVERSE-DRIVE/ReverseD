using System.Collections.Generic;
using SoundManage;
using UnityEditor;
using UnityEngine;

public class AudioPackSOMaker : EditorWindow
{
    private List<AudioClip> audioClips = new List<AudioClip>();
    private readonly string audioClipPath = "Assets/10_Database/Audio";
    private AudioPack audioPack;
    private Vector2 scrollPosition;
    private int count;
    private string audioPackSOName;
    private string audioPackName;
    
    
    [MenuItem("Tools/Audio/AudioPackSOMaker")]
    public static void ShowWindow()
    {
        AudioPackSOMaker window = GetWindow<AudioPackSOMaker>("AudioPackSOMaker");
        window.minSize = new Vector2(500, 300);
        window.Show();
    }

    private void OnEnable()
    {
        audioClips = new List<AudioClip>()
        {
            null
        };
    }

    private void OnGUI()
    {
        GUILayout.Label("AudioPackSO Maker", EditorStyles.boldLabel);
        EditorGUILayout.Space(20);
        
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        GUILayout.Label("Audio Setting", EditorStyles.boldLabel);

        #region AudioClip List
        EditorGUILayout.BeginVertical("helpbox");
        {
            if (audioClips != null)
            {
                for (int i = 0; i < audioClips.Count; i++)
                {
                    audioClips[i] =
                        (AudioClip)EditorGUILayout.ObjectField("AudioClip " + (i + 1), audioClips[i], typeof(AudioClip),
                            false);
                }
            }
        }
        EditorGUILayout.EndVertical();
        #endregion

        #region Add, Remove, Count Button
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Add"))
            {
                audioClips.Add(null);
            }

            if (GUILayout.Button("Remove") && audioClips.Count > 0)
            {
                audioClips.RemoveAt(audioClips.Count - 1);
            } 
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(10);
        EditorGUILayout.BeginVertical();
        {
            count = EditorGUILayout.IntField("Count", count);
            if (GUILayout.Button("Count"))
            {
                audioClips = null;
                audioClips = new List<AudioClip>(new AudioClip[count]);
            }
        }
        EditorGUILayout.EndVertical();
        #endregion
        
        EditorGUILayout.Space(20);

        #region Create AudioPackSO
        GUILayout.Label("Name Setting", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical();
        {
            EditorGUILayout.BeginVertical("helpbox");
            {
                audioPackSOName = EditorGUILayout.TextField("AudioPack SO Name", audioPackSOName);
                audioPackName = EditorGUILayout.TextField("AudioPack Name", audioPackName);
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(10);
            if (GUILayout.Button("Create"))
            {
                if (audioClips.Count == 0 || audioClips[0] == null)
                {
                    Debug.LogError("AudioClip is Empty");
                    return;
                }
                if (audioPackSOName == "")
                {
                    Debug.LogError("AudioPack Name is Empty");
                    return;
                }
                CreateAudioPackSO();
            }
        }
        EditorGUILayout.EndVertical();
        #endregion
        EditorGUILayout.EndScrollView();
    }
    
    private void CreateAudioPackSO()
    {
        audioPack = CreateInstance<AudioPack>();
        audioPack.audioClips = audioClips.ToArray();
        audioPack.packName = audioPackName;
        string fileName = AssetDatabase.GenerateUniqueAssetPath
            ($"{audioClipPath}/{audioPackSOName}.asset");
        AssetDatabase.CreateAsset(audioPack, fileName);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}

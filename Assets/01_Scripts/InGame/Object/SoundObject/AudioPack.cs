using UnityEngine;

namespace SoundManage
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "SO/SoundManage/AudioPack")]
    public class AudioPack : ScriptableObject
    {
        public string packName;
        public AudioClip[] audioClips;
        
        
    }
}
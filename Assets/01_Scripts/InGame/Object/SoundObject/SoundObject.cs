using UnityEngine;
using UnityEngine.Audio;
using SoundManage;

[RequireComponent(typeof(AudioSource))]
public class SoundObject : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup;

    [SerializeField] private AudioPack _audioPack;

    private AudioSource _audioSource;

    public void PlayAudio(int index)
    {
        _audioSource.clip = _audioPack.audioClips[index];
            
            
    }
}
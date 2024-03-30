using System;
using UnityEngine;
using UnityEngine.Audio;
using SoundManage;

[RequireComponent(typeof(AudioSource))]
public class SoundObject : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup;

    [SerializeField] private AudioPack _audioPack;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.outputAudioMixerGroup = _audioMixerGroup;
    
    }

    public void PlayAudio(int index)
    {
        _audioSource.clip = _audioPack.audioClips[index];
            
        _audioSource.Play();
    }
}
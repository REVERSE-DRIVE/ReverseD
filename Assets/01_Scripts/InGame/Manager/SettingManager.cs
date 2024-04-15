using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [Header("Sound_Setting")] 
    [SerializeField] private AudioMixerGroup _bgm;
    [SerializeField] private AudioMixerGroup _sfx;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;
    
    [Header("Screen_Setting")]
    [SerializeField] private bool _isShakingEffect;
    [SerializeField] private float _lightingStrength;
    
    private void OnEnable()
    {
        TimeManager.TimeScale = 0;
    }

    private void SetBGMVolume()
    {
        _bgm.audioMixer.SetFloat("BGMVolume", _bgmSlider.value);
    }
    
    private void SetSFXVolume()
    {
        _sfx.audioMixer.SetFloat("SFXVolume", _sfxSlider.value);
    }

    private void ShakingEffect(bool value)
    {
        _isShakingEffect = value;
    }
    
    private void SetLightingStrength(float value)
    {
        _lightingStrength = value;
    }
}

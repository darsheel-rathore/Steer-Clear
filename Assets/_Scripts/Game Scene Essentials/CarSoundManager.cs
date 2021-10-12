using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSoundManager : MonoBehaviour
{

    [SerializeField] private AudioSource engineSource, effectsSource;

    private float engineVol, effectsVol;

    private void OnEnable()
    {
        InGameUIEventManager.carSoundToggle += OnSoundToggle;
    }

    private void OnDisable()
    {
        InGameUIEventManager.carSoundToggle -= OnSoundToggle;
    }

    private void Start()
    {
        engineVol = engineSource.volume;
        effectsVol = effectsSource.volume;
    }

    private void OnSoundToggle()
    {
        float engineVolume = engineSource.volume;
        float effectsVolume = effectsSource.volume;

        engineSource.volume = engineVolume <= 0f ? engineVol : 0f;
        effectsSource.volume = effectsVolume <= 0f ? effectsVol : 0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static void PlaySound(AudioClip clip)
    {
        GameObject soundGO = new GameObject("Sound");
        AudioSource audioSource = soundGO.AddComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
    }

    public static void StopSound()
    {
        GameObject soundGO = new GameObject("Sound");
        AudioSource audioSource = soundGO.AddComponent<AudioSource>();
        audioSource.Stop();
    }
}
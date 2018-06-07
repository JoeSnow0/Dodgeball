using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioHelper {
    public static void PlayOneShot(AudioSource src, AudioClip clip, float pitchMin = 1, float pitchMax = 1)
    {
        src.pitch = Random.Range(pitchMin, pitchMax);
        src.PlayOneShot(clip);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogbookTurnPageAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;

    public void Play()
    {
        audioSource.clip = RandomAudioClip();
        audioSource.Play();
    }

    private AudioClip RandomAudioClip()
    {
        return audioClips[Random.Range(0, audioClips.Count - 1)];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    [RequireComponent(typeof(AudioSource))]
    public class RandomizeAudio : MonoBehaviour
    {
        [SerializeField] AudioClip[] clips;

        AudioSource audioSource;
        AudioClip currentClip = null;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayRandomClip()
        {
            currentClip = clips[Random.Range(0, clips.Length)];
            audioSource.clip = currentClip;
            audioSource.Play();
        }
    }
}
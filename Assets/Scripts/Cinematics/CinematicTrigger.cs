using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using GameDevTV.Saving;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        bool hasPlayed = false;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !hasPlayed)
            {
                GetComponent<PlayableDirector>().Play();
                hasPlayed = true;
            }
        }

        public object CaptureState()
        {
            return hasPlayed;
        }

        public void RestoreState(object state)
        {
            hasPlayed = (bool)state;
        }
    }
}
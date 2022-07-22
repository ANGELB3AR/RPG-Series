using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.Attributes
{
    public class Experience : MonoBehaviour, ISaveable
    {
        float experiencePoints = 0;

        public void GainExperience(float XP)
        {
            experiencePoints += XP;
        }

        public float GetCurrentExperience()
        {
            return experiencePoints;
        }

        public object CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }
    }
}
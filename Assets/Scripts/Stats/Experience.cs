using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Saving;
using System;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        float experiencePoints = 0;

        public event Action onExperienceGained;

        public void GainExperience(float XP)
        {
            experiencePoints += XP;
            onExperienceGained();
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
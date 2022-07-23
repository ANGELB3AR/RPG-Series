using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] float baseValue;

        public float GetValue()
        {
            return baseValue;
        }
    }
}
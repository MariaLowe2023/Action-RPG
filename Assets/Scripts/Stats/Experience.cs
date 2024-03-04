using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float expieriencePoints = 0;

        public event Action onExperienceGained;

        public void GainExperience(float experience)
        {
            expieriencePoints += experience;
            onExperienceGained();
        }
        
        public float GetPoints() { return expieriencePoints; }

        public object CaptureState()
        {
            return expieriencePoints;
        }

        public void RestoreState(object state)
        {
            expieriencePoints = (float)state;
        }
    }
}

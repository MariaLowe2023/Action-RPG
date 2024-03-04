using UnityEngine;
using UnityEngine.Playables;
using RPG.Saving;
using RPG.Core;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        bool alreadyTriggeredCinematic = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!alreadyTriggeredCinematic && other.gameObject.tag == "Player")
            {
                alreadyTriggeredCinematic = true;
                GetComponent<PlayableDirector>().Play();
            }
            else { return; }
        }

        public object CaptureState()
        {
            return alreadyTriggeredCinematic;
        }

        public void RestoreState(object state)
        {
            alreadyTriggeredCinematic = (bool)state;
        }
    }
}

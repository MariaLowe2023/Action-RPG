using RPG.Attributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;

        void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        void Update()
        {
            if (fighter.GetTarget() == null)
            {
                GetComponent<TextMeshProUGUI>().SetText("N/A");
                return;
            }
            Health health = fighter.GetTarget();
            GetComponent<TextMeshProUGUI>().SetText("{0:0}%", health.GetPercentage()); //0:0.0 for decimal
        }
    }
}

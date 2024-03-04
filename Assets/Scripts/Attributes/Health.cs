using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using GameDevTV.Utils;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthRegeneration = 70f;
        [SerializeField] UnityEvent<float> takeDamage;
        [SerializeField] UnityEvent onDie;

        LazyValue<float> health;

        bool isDead = false;

        void Awake()
        {
            health = new LazyValue<float>(GetMaxHealth);
        }

        float GetMaxHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        void Start()
        {
            health.ForceInit();
        }

        void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }

        void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            health.value = Mathf.Max(health.value - damage, 0);
            if (health.value == 0)
            {
                onDie.Invoke();
                Death();
                AwardExperience(instigator);
            }
            else
            {
                takeDamage.Invoke(damage);
            }
        }

        void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) { return; }
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }
        void RegenerateHealth()
        {
            float healthRegenerationPercentage = GetMaxHealth() * (healthRegeneration / 100);
            health.value = Mathf.Max(health.value, healthRegenerationPercentage);
        }

        public void Heal(float healAmount)
        {
            health.value = Mathf.Min(health.value + healAmount, GetMaxHealth());
        }

        public float GetPercentage()
        {
            return 100 * (GetFraction());
        }
        public float GetFraction()
        {
            return (health.value / GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        void Death()
        {
            if (isDead) { return; }

            isDead = true;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return health.value;
        }

        public void RestoreState(object state)
        {
            health.value = (float)state;
            if (health.value <= 0)
            {
                Death();
            }
        }
    }
}
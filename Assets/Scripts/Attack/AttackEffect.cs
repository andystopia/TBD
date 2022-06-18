using Entity;
using UnityEngine;

namespace Attack
{
    public abstract class AttackEffect : MonoBehaviour
    {
        [SerializeField]
        private int duration;
        [SerializeField]
        private int durationRemaining;

        [SerializeField]
        private int damagePerSecond;

        public int DamagePerSecond => damagePerSecond;

        public abstract string Name { get;  }

        public virtual AttackExpiry MakeExpiry()
        {
            return new AttackExpiry(durationRemaining, this);
        }

        public virtual void Apply(Health health)
        {
            health.ChangeHealth(-damagePerSecond);
        } 
    }
}
using UnityEngine;

namespace Attack
{
    public class AttackExpiry
    {
        public float durationRemaining;
        public readonly AttackEffect effect;

        public AttackExpiry(float durationRemaining, AttackEffect effect)
        {
            this.durationRemaining = durationRemaining;
            this.effect = effect;
        }


        public bool IsValid => durationRemaining > 0.0;
        
        // note must call
        public void Update()
        {
            durationRemaining -= Time.deltaTime;
        }
    }
}
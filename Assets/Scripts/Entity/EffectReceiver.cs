using System;
using System.Collections.Generic;
using System.Linq;
using Attack;
using UnityEngine;


namespace Entity
{
    public class EffectReceiver : MonoBehaviour
    {
        private List<AttackExpiry> effects;
        private Health _healthComponent;
        [SerializeField] private EntityStats entityStats;
        
        private void Awake()
        {
            _healthComponent = GetComponent<Health>();
        }

        private void Update()
        {
            effects.RemoveAll(effect => !effect.IsValid);
            foreach (var attackEffect in effects)
            {
                new HealthModification(attackEffect.effect.DamagePerSecond).Apply(_healthComponent);
                attackEffect.Update();
            }
        }
    }
}
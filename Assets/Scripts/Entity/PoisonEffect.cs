using UnityEngine;

namespace Entity
{
        public class PoisonEffect : EntityEffect
        {
            public override int HealthDecreasePerSecond => 2;
        }
}
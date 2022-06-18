using System.Collections.Generic;
using System.Linq;
using Entity;
using UnityEditor;

// let's scare the oop devs by slapping them with some modern functional design
namespace Attack
{
    public class HealthModification
    {
        private readonly int healthDelta;

        private readonly List<HealthBaseModification> modifications = new();

        public HealthModification(int healthDelta)
        {
            this.healthDelta = healthDelta;
        }

        public static HealthModification HealthIncrease(int increase)
        {
            return new HealthModification(increase);
        }
        
        
        public static HealthModification HealthDecrease(int decrease)
        {
            return new HealthModification(-decrease);
        }

        public HealthModification Add(HealthBaseModification modification)
        {
            modifications.Add(modification);
            return this;
        }
        public HealthModification AddAll(IEnumerable<HealthBaseModification> modification)
        {
            foreach (var healthBaseModification in modification)
            {
                modifications.Add(healthBaseModification);
            }
            return this;
        }

        public void Apply(Health health)
        {
            health.ChangeHealth(modifications.Aggregate(healthDelta, (runningHealth, nextHealth) => nextHealth.Apply(runningHealth)));
        }
    }

    public abstract class HealthBaseModification
    {
        public abstract int Apply(int health);
    }

    public sealed class HealthFactorModification : HealthBaseModification
    {
        private readonly float factor;

        public float Factor => factor;

        public HealthFactorModification(float factor)
        {
            this.factor = factor;
        }

        public override int Apply(int health)
        {
            return (int) this.factor * health;
        }
    }

    public sealed class HealthAddendModification: HealthBaseModification
    {
        private readonly int addend;
        
        public int Addend => addend;

        public HealthAddendModification(int addend)
        {
            this.addend = addend;
        }

        public override int Apply(int health)
        {
            return health + addend;
        }
    }
}
using UnityEngine;

namespace Entity
{
    [System.Serializable]
    public abstract class EntityEffect : MonoBehaviour
    {
        public abstract int HealthDecreasePerSecond { get; } 
    }
}
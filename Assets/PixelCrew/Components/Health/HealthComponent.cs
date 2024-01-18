using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.Serialization;

namespace PixelCrew.Utils.Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [FormerlySerializedAs("Health")] [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private UnityEvent _onHeal;

        public void ModifyHealth(int healthDelta)
        {
            _health += healthDelta;

            if (healthDelta > 0)
            {
                _onHeal?.Invoke();
            }
            
            if (healthDelta < 0)
            {
                _onDamage?.Invoke();
                if (CompareTag("Hero"))
                {
                    print($"Всего хп: {_health}");
                }
            }

            if (_health <= 0 )
            {
                _onDie?.Invoke();
            }
        }

        internal void SetHealth(int health)
        {
            _health = health;
        }
    }

    
}

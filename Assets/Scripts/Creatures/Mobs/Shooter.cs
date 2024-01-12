using PixelCrew.Components.GoBased;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Creatures.Mobs
{
    public class Shooter : MonoBehaviour
    {
        [Header("Range")]
        [SerializeField] protected SpawnComponent RangeEnemyAttack;

        private Animator _animator;
        private static readonly int Range = Animator.StringToHash("range");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        public void RangeAttack()
        {
            _animator.SetTrigger(Range);
        }

        public void OnRangeTotemAttack()
        {           
            RangeEnemyAttack.Spawn();
        }       

    }
    [Serializable]
    public class OnGameObjectEvent : UnityEvent<GameObject>
    {

    }
}

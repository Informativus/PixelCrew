using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.GoBased;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs
{
    public class ShootingTrapAI : MonoBehaviour
    {

        [SerializeField] private LayerCheck _vision;

        [Header("Range")]
        [SerializeField] protected SpawnComponent RangeEnemyAttack;
        [SerializeField] protected Cooldown RangeCooldown;


        private Animator _animator;
        private static readonly int Range = Animator.StringToHash("range");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            
            if (_vision.IsTouchingLayer)
            {
                if (RangeCooldown.IsReady)
                {
                    RangeAttack();
                    RangeCooldown.Reset();
                }
                
            }
        }

        public void RangeAttack()
        {
            _animator.SetTrigger(Range);
        }

        public void OnRangeAttack()
        {
            RangeEnemyAttack.Spawn();
        }
                

    }
    
}

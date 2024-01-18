using PixelCrew.Utils.Creatures.Mobs;
using PixelCrew.Utils.Components.ColiderBased;
using PixelCrew.Utils.Untils;
using UnityEngine;

namespace PixelCrew.Utils.Creatures.Mobs
{
    public class MeleeTrapAI : MonoBehaviour
    {
        [Header("Melee")]
        [SerializeField] private CheckOverLap _meleeAttack;
        [SerializeField] private LayerCheck _meleeCanAttack;
        [SerializeField] private Cooldown _meleeCooldown;

        private Animator _animator;
        private static readonly int Melee = Animator.StringToHash("melee");
 
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        private void Update()
        {
            if (_meleeCanAttack.IsTouchingLayer)
            {
                if (_meleeCooldown.IsReady)
                    MeleeAttack();
                return;
            }
        }
        private void MeleeAttack()
        {
            _animator.SetTrigger(Melee);
            _meleeCooldown.Reset();
        }

        public void OnMeleeAttack()
        {
            _meleeAttack.Check();
        }

    }
}

using PixelCrew.Components.Health;
using PixelCrew.Model;
using PixelCrew.Utils;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;
using PixelCrew.Components;

namespace PixelCrew.Creatures.Hero
{
    public class Hero : Creature
    {
        [SerializeField] private CheckOverLap _interactionCheck; 
        [SerializeField] private float _lungeImpulse;


        [Space] [Header("Particles")]
        [SerializeField] private ParticleSystem _hitParticles;

        private static readonly int ThrowKey = Animator.StringToHash("Throw");

        [SerializeField] private Cooldown _throwCooldown, _lungeCooldown, _attackCooldown;

        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;

        [SerializeField] private UnityEvent _OnHeal;
        
        private int SwordCount => _session.Data.Inventory.Count("Sword");
        private int CoinsCount => _session.Data.Inventory.Count("Coin");


        private bool _allowDoubleJump;
        private GameSession _session;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();

            _session.Data.Inventory.OnChanged += OnInventoryChanged;

            var health = GetComponent<HealthComponent>();
            health.SetHealth(_session.Data.Hp);
            UpdateHeroWeapon(); 
        }

        private void OnInventoryChanged(string id, int value)
        {
            if (id == "Sword")
                UpdateHeroWeapon();
        }
        
        private void OnDestroy()
        {
            _session.Data.Inventory.OnChanged -= OnInventoryChanged;
        }
        
        public void Throw()
        {
            if (_session.Data.Inventory.Count("Sword") <= 1) return;

            if (_throwCooldown.IsReady)
            {
                _session.Data.Inventory.Remove("Sword", 1);
                Animator.SetTrigger(ThrowKey);
                _throwCooldown.Reset();
                Sounds.Play("Range");
            }

        }

        protected override float CalculateYVelocity()
        {            
            var _isJumpPressing = Direction.y > 0;

            if (IsGrounded)
            {
                _allowDoubleJump = true;
            }            

            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelocity)
        {
             if (!IsGrounded && _allowDoubleJump)
             {
                _allowDoubleJump = false;
                DoJumpVfx();
                return JumpSpeed;
             }
             return base.CalculateJumpVelocity(yVelocity);
        }

        public override void TakeDamage()
        {
            base.TakeDamage();
            if (CoinsCount > 0)
            {
                SpawnCoins();
            }
        }

        private void SpawnCoins()
        {
            var numCoinsToDispose = Mathf.Min(CoinsCount , 5);
            _session.Data.Inventory.Remove("Coin", numCoinsToDispose);

            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = numCoinsToDispose;
            _hitParticles.emission.SetBurst(0, burst);

            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();
        }

        public void Interact()
        {
            _interactionCheck.Check();
        }       


        public void Lunge()
        {
            if (_lungeCooldown.IsReady)
            {                
                Animator.Play("lunge");
                _lungeCooldown.Reset();
                Rigidbody.velocity = new Vector2(0, 0);

                if (Rigidbody.transform.localScale.x > 0)
                {
                    Rigidbody.AddForce(Vector2.right * _lungeImpulse);
                }
                else
                {
                    Rigidbody.AddForce(Vector2.left * _lungeImpulse);
                }
            }
        }
        public override void Attack()
        {
            if (SwordCount <= 0) return;
            if (_attackCooldown.IsReady)
            {
                base.Attack();
                Particles.Spawn("Attack");
                Sounds.Play("Melee");
                _attackCooldown.Reset();
            }
            
        }

        public void AddInInventory(string id, int value)
        {
            _session.Data.Inventory.Add(id, value);
        }

        private void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = SwordCount > 0 ? _armed : _disarmed;            
        }

        public void Treatment()
        {

            if (_session.Data.Inventory.Count("HealthPotions") == 0) return;

            _session.Data.Inventory.Remove("HealthPotions", 1);
            
            _OnHeal?.Invoke();
        }
    }
}

using PixelCrew.Components.Health;
using PixelCrew.Model;
using PixelCrew.Utils;
using UnityEditor.Animations;
using UnityEngine;
using PixelCrew.Components;
using PixelCrew.Components.GoBased;
using PixelCrew.Model.Definitions;

namespace PixelCrew.Creatures.Hero
{
    public class Hero : Creature
    {
        [SerializeField] private CheckOverLap _interactionCheck;
        [SerializeField] private float _lungeImpulse;


        [Space] [Header("Particles")] [SerializeField]
        private ParticleSystem _hitParticles;

        private static readonly int ThrowKey = Animator.StringToHash("Throw");
        private new static readonly int AttackKey = Animator.StringToHash("attack");

        [SerializeField] private Cooldown _throwCooldown, _lungeCooldown, _attackCooldown;

        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;

        [SerializeField] private SpawnComponent _throwSpawner;

        [SerializeField] private TreatmentComponent _treatmentComponent;

        private const string SwordId = "Sword";
        private const string CoinId = "Coin";
        
        private int SwordCount => _session.Data.Inventory.Count(SwordId);
        private int CoinsCount => _session.Data.Inventory.Count(CoinId);


        private bool _allowDoubleJump;
        private GameSession _session;
        
        private string SelectedItemId => _session.InventoryModel.SelectedItem?.Id;
        

        private bool CanThrow
        {
            get
            {
                if (SelectedItemId == SwordId )
                    return SwordCount > 1;

                var def = DefsFacade.I.Items.Get(SelectedItemId);

                return def.HasTag(ItemTag.Throwable);
            }
        }

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();

            _session.Data.Inventory.OnChanged += OnInventoryChanged;

            var health = GetComponent<HealthComponent>();
            health.SetHealth(_session.Data.Hp.Value);
            UpdateHeroWeapon();
        }

        private void OnInventoryChanged(string id, int value)
        {
            if (id == SwordId)
                UpdateHeroWeapon();
        }

        private void OnDestroy()
        {
            _session.Data.Inventory.OnChanged -= OnInventoryChanged;
        }

        public void ThrowAndRemoveFromInventory()
        {
            if (!CanThrow) return;

            var throwableId = _session.InventoryModel.SelectedItem.Id;
            var throwableDef = DefsFacade.I.Throwable.Get(throwableId);
            Animator.SetTrigger(ThrowKey);
            Sounds.Play("Range");

            _throwSpawner.SetPrefab(throwableDef.Projectile);
            _throwSpawner.Spawn();

            _session.Data.Inventory.Remove(throwableId, 1);
        }

        protected override float CalculateYVelocity()
        {
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
            var numCoinsToDispose = Mathf.Min(CoinsCount, 5);
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
                Animator.SetTrigger(AttackKey);
                Particles.Spawn("Attack");
                Sounds.Play("Melee");
                _attackCooldown.Reset();
            }
        }

        public void AddInInventory(string id, int value)
        {
            _session.Data.Inventory.TryAdd(id, value);
        }

        private void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = SwordCount > 0 ? _armed : _disarmed;
        }

        public void Treatment()
        {
            _treatmentComponent.ModifyHealth(gameObject, Sounds);
        }

        public void NextItem()
        {
            _session.InventoryModel.SetNextItem();
        }
    }
}
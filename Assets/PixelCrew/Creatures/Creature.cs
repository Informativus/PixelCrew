using UnityEngine;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.GoBased;
using PixelCrew.Components.Audio;
using PixelCrew.Components;

namespace PixelCrew.Creatures
{
    public class Creature : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] protected bool InvertScale;
        [SerializeField] private float _speed;
        [SerializeField] protected float JumpSpeed;
        [SerializeField] private float _damageJumpSpeed;
        [SerializeField] private float _scaleGo;

        [Header("Checkers")]
        [SerializeField] protected LayerCheck GroundCheck;
        [SerializeField] protected SpawnListComponent Particles;
        [SerializeField] private CheckOverLap _attackRange;

        protected Vector2 Direction;
        protected Rigidbody2D Rigidbody;
        protected Animator Animator;
        protected PlaySoundsComponent Sounds;
        protected bool IsGrounded;
        protected bool IsJumping;  

        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunning = Animator.StringToHash("is-runing");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");
        protected static readonly int AttackKey = Animator.StringToHash("attack");

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            Sounds = GetComponent<PlaySoundsComponent>();
        }

        protected virtual void Update()
        {
            IsGrounded = GroundCheck.IsTouchingLayer;
        }

        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }
        private void FixedUpdate()
        {
            var xVelocity = Direction.x * _speed;
            var yVelocity = CalculateYVelocity();
            Rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            Animator.SetBool(IsGroundKey, IsGrounded);
            Animator.SetBool(IsRunning, Direction.x != 0);
            Animator.SetFloat(VerticalVelocity, Rigidbody.velocity.y);

            UpdateSpriteDirection(Direction);

        }

        protected virtual float CalculateYVelocity()
        {
            var yVelocity = Rigidbody.velocity.y;
            var IsJumpPressing = Direction.y > 0;

            if (IsGrounded)
            {
                IsJumping = false;
            }
            if (IsJumpPressing)
            {
                IsJumping = true;
                var isFalling = Rigidbody.velocity.y <= 0.01f;
  
                yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;
            }
            else if (Rigidbody.velocity.y > 0)
            {
                yVelocity *= 0.5f;
            }

            return yVelocity;
        }

        protected virtual float CalculateJumpVelocity(float yVelocity)
        {
            if (!IsGrounded) return yVelocity;
            yVelocity += JumpSpeed;
            DoJumpVfx();
            return yVelocity;
        }

        protected void DoJumpVfx()
        {
            Particles.Spawn("Jump");
            Sounds.Play("Jump");
        }

        public void UpdateSpriteDirection(Vector2 direction)
        {
            float multiplier = InvertScale ? -1 : 1;

            if (Direction.x > 0)
            {
                transform.localScale = new Vector3(_scaleGo * multiplier, _scaleGo, 1);
            }
            else if (Direction.x < 0)
            {
                transform.localScale = new Vector3(-_scaleGo * multiplier, _scaleGo, -1);
            }
        }

        public virtual void TakeDamage()
        {
            IsJumping = false;
            Animator.SetTrigger(Hit);
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _damageJumpSpeed);
        }

        public virtual void Attack()
        {         
            Animator.SetTrigger(AttackKey);
            Sounds.Play("Melee");
        }
        public void OnDoAttack()
        {
            _attackRange.Check();
        }

        public void OnDoThrow()
        {
            Particles.Spawn("Throw");
        }

    }
}

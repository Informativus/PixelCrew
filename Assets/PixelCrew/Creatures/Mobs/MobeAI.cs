using System.Collections;
using UnityEngine;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.GoBased;
using PixelCrew.Creatures.Patrolling;

namespace PixelCrew.Creatures.Mobs
{
    public class MobeAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private LayerCheck _canAttack;

        [SerializeField] private float _damageWaitTime = 1.5f;
        [SerializeField] private float _alarmDelay = 0.5f;

        private string _visionAnim = "Exclamation";

        private Coroutine _current;
        private GameObject _target;
        private SpawnListComponent _particles;
        private Creature _creature;
        


        private float _attackCooldown = 1f;
        private bool _isDead;
        [SerializeField] private Patrol _patrol;

        private void Awake()
        {
            _particles = GetComponent<SpawnListComponent>();
            _creature = GetComponent<Creature>();
        }

        private void Start()
        {
            Patrolling();
        }

        public void OnHeroInVision(GameObject og)
        {
            if (_isDead) return;
            _target = og;
            StartState(AgroToHero());
        }
        private IEnumerator AgroToHero()
        {
            LookAtHero();
            _particles.Spawn(_visionAnim);
            yield return new WaitForSeconds(_alarmDelay);
            
            StartState(GoToHero());
        }

        private void LookAtHero()
        {
            var direction = GetDirectionTarget();
            _creature.SetDirection(Vector2.zero);
            _creature.UpdateSpriteDirection(direction);
        }

        private IEnumerator GoToHero()
        {

            while (_vision.IsTouchingLayer)
            {
                if (_canAttack.IsTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                {
                    SetDirectionToTarget();
                }
                yield return null;
            }

            Patrolling();
             
        }
        private void Patrolling()
        {
            if (_patrol != null)
            {
                StartState(_patrol.DoPatrol());
            }
        }

        private IEnumerator TakeDamage()
        {
            _creature.SetDirection(Vector2.zero);
            yield return new WaitForSeconds(_damageWaitTime);
            StartState(GoToHero());
        }
            
        public void DoTakeDamage()
        {
            StartState(TakeDamage());
        }
        private IEnumerator Attack()
        {

            while (_canAttack.IsTouchingLayer)
            {
                _creature.Attack();
                yield return new WaitForSeconds(_attackCooldown);
            }

            StartState(GoToHero()); 
        }

        private void SetDirectionToTarget()
        {
            var direction = GetDirectionTarget();
            direction.y = 0;
            _creature.SetDirection(direction.normalized);
        }

        private Vector2 GetDirectionTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            return direction.normalized;
        }

        public void StartState(IEnumerator coroutine)
        {

            _creature.SetDirection(Vector2.zero);
            if (_current != null)
                StopCoroutine(_current);

            _current = StartCoroutine(coroutine);
        }
    }
}

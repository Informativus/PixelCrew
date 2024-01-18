using System.Collections;
using System.Collections.Generic;
using PixelCrew.Components.ColliderBased;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs
{
    public class TotemsShootingAI : MonoBehaviour
    {
        [SerializeField] protected List<GameObject> GameObjects;
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private float _waitTime;
        private Coroutine _coroutine;
        private int _count = 0;


        private IEnumerator Shoot()
        {
            
            while (_vision.IsTouchingLayer)
            {
                
                GameObject go = GameObjects[_count];

                var shooting = go.GetComponent<Shooter>();
                _count++;
                if (_count >= GameObjects.Count)
                {
                    _count = 0;
                }
                shooting.RangeAttack();
                yield return new WaitForSeconds(_waitTime);                                               
            }
            _coroutine = null;
        }
        public void StartState()
        {
            if(_coroutine == null)
            {
                _coroutine = StartCoroutine(Shoot());
            }
        }

        public void RemoveGO(GameObject go)
        {
            if (!GameObjects.Contains(go)) return;           
            GameObjects.Remove(go);
            if (GameObjects.Count <= 0)
            {
                StopCoroutine(_coroutine);
                Destroy(this.gameObject);
            }
            _count = 0;
        }
    }
}

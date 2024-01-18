using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using System.Linq;

namespace PixelCrew.Utils
{
    public class CheckOverLap : MonoBehaviour
    {
        [SerializeField] private float _radius = 1f;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private string[] _tags;

        [SerializeField] private OnOverlapEvent _onOverlap;

        private Collider2D[] _intaractionResult = new Collider2D[10];

        private void OnDrawGizmosSelected()
        {
            Handles.color = HandlesUtils.TransparentRed;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }

        internal void Check()
        {
            var size = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _intaractionResult, _mask);
            for (int i = 0; i < size; i++)
            {
                var overlapResult = _intaractionResult[i];
                var IsInTags = _tags.Any(tag => overlapResult.CompareTag(tag));
                if (IsInTags)
                {
                    _onOverlap?.Invoke(overlapResult.gameObject);
                }
            }

        }

        [Serializable]
        public class OnOverlapEvent : UnityEvent<GameObject>
        {

        }
    }
}

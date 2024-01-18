using UnityEngine;
using PixelCrew.Utils.Utils;

namespace PixelCrew.Utils.Components.ColiderBased
{
    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private LayerMask _layer = ~0;
        [SerializeField] private EnterEvent _actions;
        private void OnTriggerEnter2D(Collider2D colider)
        {
            if (!colider.gameObject.IsInLayer(_layer)) return;
            if (!string.IsNullOrEmpty(_tag) && !colider.gameObject.CompareTag(_tag)) return;
            _actions?.Invoke(colider.gameObject);
        }

    }
}

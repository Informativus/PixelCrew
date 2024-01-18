using UnityEngine;

namespace PixelCrew.Components.Interactions
{
    public class SwitchComponent : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private bool _state;
        public string _animatorKey;

        public void Switch()
        {
            _state = !_state;
            _animator.SetBool(_animatorKey, _state);
        }

        [ContextMenu("Switch")]
        public void SwitchIn()
        {
            Switch();
        }
    }
}

using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Utils.Components.Interactions
{
    public class interactableComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _action;

        public void Interact()
        {
            _action?.Invoke();
        }

    }
}


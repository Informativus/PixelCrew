using UnityEngine;

namespace PixelCrew.Utils.Components.Interactions
{
    public class DoInteractionComponent : MonoBehaviour
    {
        public void DoInteraction(GameObject go)
        {
            var interactable = go.GetComponent<interactableComponent>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }


    }
}

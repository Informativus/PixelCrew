using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelCrew.Utils.Creatures.Hero
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private Hero _hero;
        public void OnHorizontalMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _hero.SetDirection(direction);
        }

        public void OnInteraction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Interact();
            }
        }
        public void OnLunge(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Lunge();
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Attack();
            }
        }
        public void OnThrow(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Throw();
            }
        }
        public void OnTreatment(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Treatment();
            }
        }


    }
}

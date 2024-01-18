using PixelCrew.Model;
using UnityEngine;

namespace PixelCrew.Components.Health
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private int _hpDelta;

        public void ApplyHealthDelta(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ModifyHealth(_hpDelta);
            }

            if (target.CompareTag("Hero"))
            {
                GameObject sessionGameObject = GameObject.Find("Session");
                sessionGameObject.GetComponent<GameSession>()._data.Hp += _hpDelta;
            }
        }
    }
}

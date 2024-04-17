using PixelCrew.Model;
using PixelCrew.Model.Definitions;
using Unity.VisualScripting;
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
                var sessionGameObject = GameObject.Find("Session").GetComponent<GameSession>();
                if (sessionGameObject._data.Hp.Value + _hpDelta >= DefsFacade.I.Player.MaxHealth)
                {
                    sessionGameObject._data.Hp.Value = DefsFacade.I.Player.MaxHealth;
                    return;
                }
                sessionGameObject._data.Hp.Value += _hpDelta;
            }
        }
    }
}

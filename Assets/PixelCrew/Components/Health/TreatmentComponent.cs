using System;
using PixelCrew.Model;
using UnityEngine;
using PixelCrew.Components.Audio;
using PixelCrew.Model.Definitions;

namespace PixelCrew.Components.Health
{
    public class TreatmentComponent : MonoBehaviour
    {
        [SerializeField] private ModifyHealthComponent _modifyHealth;
        
        private GameSession _session;
        
        private const string BigHealthId = "BigHealthPotion";
        private const string HealthId = "HealthPotion";
        
        private int BigHealthCount => _session.Data.Inventory.Count(BigHealthId);
        private int HealthCount => _session.Data.Inventory.Count(HealthId);

        private bool CanHeal
        {
            get
            {
                if (_session.InventoryModel.SelectedItem.Id is HealthId or BigHealthId)
                    return BigHealthCount > 0 || HealthCount > 0;

                return false;
            }
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
        }

        public void ModifyHealth(GameObject go, PlaySoundsComponent playSoundsComponent)
        {
            if (!CanHeal) return;
            var potionId = _session.InventoryModel.SelectedItem.Id;

            if (_session.Data.Hp.Value == DefsFacade.I.Player.MaxHealth)
            {
                print("Лечение не требуется!");
                return;
            }
            
            switch (potionId)
            {
                case HealthId:
                    TreatmentValue(go, playSoundsComponent, 3, potionId);
                    break;
                case BigHealthId:
                    TreatmentValue(go, playSoundsComponent, 6, potionId);
                    break;
            }
        }

        private void TreatmentValue(GameObject go, PlaySoundsComponent playSoundsComponent, int healthDelta, string potionId)
        {
            playSoundsComponent.Play("Treatment");
            _modifyHealth.SetHealthDelta(healthDelta);
            _modifyHealth.ApplyHealthDelta(go);
            _session.Data.Inventory.Remove(potionId, 1);
        }
    }
}
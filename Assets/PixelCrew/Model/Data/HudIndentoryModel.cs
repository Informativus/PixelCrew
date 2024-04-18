using System;
using PixelCrew.Model.Data.Properties;
using PixelCrew.Model.Definitions;
using PixelCrew.Utils.Disposables;
using UnityEngine;

namespace PixelCrew.Model.Data
{
    public class HudIndentoryModel
    {
        private readonly PlayerData _data;
        public InventoryItemData[] Inventory { get; private set; }

        public readonly IntProperty SelectedIndex = new IntProperty();

        public event Action OnChanged; 
        
        public InventoryItemData SelectedItem
        {
            get
            {
                if (Inventory.Length > 0 && Inventory.Length > SelectedIndex.Value)
                    return Inventory[SelectedIndex.Value];
                return null;
            }
            
        }

        public HudIndentoryModel(PlayerData data)
        {
            _data = data;
            
            Inventory = _data.Inventory.GetAll(ItemTag.Usable);
            _data.Inventory.OnChanged += OnChangedInventory;
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        private void OnChangedInventory(string id, int value)
        {
            var indexFound = Array.FindIndex(Inventory, x => x.Id == id);
            if (indexFound == -1)
            {
                Inventory = _data.Inventory.GetAll(ItemTag.Usable);
                SelectedIndex.Value = Mathf.Clamp(SelectedIndex.Value, 0, Inventory.Length - 1);
                OnChanged?.Invoke();
            }
        }

        public void SetNextItem()
        {
            if (Inventory.Length == 0) return;
            SelectedIndex.Value = (int) Mathf.Repeat(SelectedIndex.Value + 1, Inventory.Length);
        }
    }
}
using PixelCrew.Model.Data.Properties;
using Unity.VisualScripting;
using UnityEngine;

namespace PixelCrew.Model.Data
{
    public class QuickInventoryModel 
    {
        private readonly PlayerData _data;

        public readonly IntProperty SelectedIndex = new IntProperty();
        public InventoryItemData[] Inventory { get; private set; }

        public QuickInventoryModel(PlayerData data)
        {
            _data = data;

            Inventory = _data.Inventory.GetAll();
            _data.Inventory.OnChanged += OnChanged;
        }

        private void OnChanged(string id, int value)
        {
            Inventory = _data.Inventory.GetAll();
            SelectedIndex.Value = Mathf.Clamp(SelectedIndex.Value, 0, Inventory.Length - 1);
        }

        private void OnDestroy()
        {
            _data.Inventory.OnChanged -= OnChanged;
        }
    }

    
}
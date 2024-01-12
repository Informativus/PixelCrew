using System;
using PixelCrew.Model;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.Model.Data
{
    
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData _inventory;
        
        public int Hp;
        public InventoryData Inventory => _inventory;

        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }

}

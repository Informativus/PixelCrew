using System;
using System.Linq;
using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/PotionItems", fileName = "PotionItems")]
    public class PotionItemsDef : ScriptableObject
    {
        [SerializeField] private PotionDef[] _items;

        public PotionDef Get(string id)
        {
            var item = _items.FirstOrDefault(itemDef => itemDef.Id == id);
            return item;
        }
    }

    [Serializable]
    public struct PotionDef
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private PotionTag[] _tags;
        public string Id => _id;
        
        public bool HasTag(PotionTag tag)
        {
            return _tags.Contains(tag);
        }
    }
}
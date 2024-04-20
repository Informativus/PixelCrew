using PixelCrew.Utils;
using PixelCrew.Model.Data;
using UnityEngine;
using PixelCrew.Model.Definitions;
using UnityEngine.Events;

namespace PixelCrew.Components.Collectables
{
    public class InventoryAddComponent : MonoBehaviour
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private UnityEvent _onSuccess;
        [SerializeField] private int _count;
        public void Add(GameObject go)
        {
            var canAddInInventory = go.GetInterface<ICanAddInInventory>();
            if (canAddInInventory?.AddInInventory(_id, _count) ?? false)
            {
                _onSuccess?.Invoke();
            }
        }
    }
}
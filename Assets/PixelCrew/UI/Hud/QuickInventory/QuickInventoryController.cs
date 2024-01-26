using System.Collections.Generic;
using PixelCrew.Model;
using PixelCrew.Model.Data;
using PixelCrew.UI.Widgets;
using PixelCrew.Utils.Disposables;
using UnityEngine;

namespace PixelCrew.UI.Hud.QuickInventory
{
    public class QuickInventoryController : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private InventoryItemWidget _prefab;
        
        private GameSession _session;
        private InventoryItemData[] _inventory;
        private List<InventoryItemWidget> _createdItem = new List<InventoryItemWidget>();

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private void Start()
        {
            _session = FindObjectOfType<GameSession>(); 

            Rebuild();
        }

        private void Rebuild()
        {
            _inventory = _session.Data.Inventory.GetAll();

            for (var i = _createdItem.Count; i < _inventory.Length; i++)
            {
                var item = Instantiate(_prefab, _container); 
                _createdItem.Add(item);
            }

            for (int i = 0; i < _inventory.Length; i++)
            {
                _createdItem[i].SetData(_inventory[i],i);
                _createdItem[i].gameObject.SetActive(true);
            }

            for (int i = _inventory.Length; i < _createdItem.Count; i++)
            {
                _createdItem[i].gameObject.SetActive(false);
            }
        }
    }
}
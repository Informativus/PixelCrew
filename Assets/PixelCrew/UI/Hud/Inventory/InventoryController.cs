using System.Collections.Generic;
using PixelCrew.Model;
using PixelCrew.Utils.Disposable;
using UnityEngine;

namespace PixelCrew.UI.Hud.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private InventoryItemWidget _item;

        private CompositeDisposable _trash = new CompositeDisposable();

        private GameSession _session;
        private readonly List<InventoryItemWidget> _createdItem = new List<InventoryItemWidget>();

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _trash.Retain(_session.InventoryModel.Subscribe(Rebuild));

            Rebuild();
        }

        private void Rebuild()
        {
            var _inventory = _session.InventoryModel.Inventory;

            for (var i = _createdItem.Count; i < _inventory.Length; i++)
            {
                var item = Instantiate(_item, _container);
                _createdItem.Add(item);
            }

            for (int i = 0; i < _inventory.Length; i++)
            {
                _createdItem[i].SetData(_inventory[i], i);
                _createdItem[i].gameObject.SetActive(true);
            }

            for (int i = _inventory.Length; i < _createdItem.Count; i++)
            {
                _createdItem[i].gameObject.SetActive(false);
            }
        }
    }
}
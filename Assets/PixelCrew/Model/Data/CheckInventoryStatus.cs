using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Model.Data
{
    public class CheckInventoryStatus : MonoBehaviour
    {
        [SerializeField] private EnterEvent _event;

        private GameSession _session;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
        }

        public void InventoryIsFull()
        {
           // if(_session.Data.Inventory)
        }
    }

    [Serializable]
    public class EnterEvent : UnityEvent<GameObject>
    {
        
    }
}
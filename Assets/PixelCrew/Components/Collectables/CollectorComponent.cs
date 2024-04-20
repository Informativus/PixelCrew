using System;
using System.Collections.Generic;
using PixelCrew.Model;
using PixelCrew.Model.Data;
using UnityEngine;

namespace PixelCrew.Components.Collectables
{
    public class CollectorComponent : MonoBehaviour, ICanAddInInventory
    {
        private GameSession _session;

        private void Awake()
        {
            _session = FindObjectOfType<GameSession>();
        }

        public bool AddInInventory(string id, int value)
        {
            return _session.Data.Inventory.TryAdd(id, value);
        }
    }
}
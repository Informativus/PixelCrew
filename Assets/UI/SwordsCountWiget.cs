using UnityEngine;
using UnityEngine.UI;
using PixelCrew.Model;

namespace PixelCrew.UI
{
    class SwordsCountWiget : MonoBehaviour
    {
        [SerializeField] private Text _text;
        private GameSession _session;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();

            _session.Data.Inventory.OnChanged += OnInventoryChanged;

            _text.text = $"{_session.Data.Inventory.Count("Sword")}";
        }
        private void OnInventoryChanged(string id, int value)
        {
            if (id == "Sword")
                _text.text = $"{value}";
        }

        private void OnDestroy()
        {
            _session.Data.Inventory.OnChanged -= OnInventoryChanged;
        }
    }
}

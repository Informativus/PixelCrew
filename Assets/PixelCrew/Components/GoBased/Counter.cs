using PixelCrew.Utils.Components.ColiderBased;
using PixelCrew.Utils.Untils;
using UnityEngine;

namespace PixelCrew.Utils.Components.GoBased
{
    public class Counter : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;

        [SerializeField] private Cooldown _spawnEnemyCooldown;
        [SerializeField] private SpawnComponent _spawnEnemy;

        private void Update()
        {
            if (_vision.IsTouchingLayer)
            {
                if (_spawnEnemyCooldown.IsReady)
                {
                    _spawnEnemy.Spawn();
                    _spawnEnemyCooldown.Reset();
                }
            }
        }

    }
}

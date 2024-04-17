using UnityEngine;

namespace PixelCrew.Components.GoBased
{

    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var instantiate = Instantiate(_prefab, transform.position, Quaternion.identity);
            instantiate.transform.localScale = _target.lossyScale;
            instantiate.SetActive(true);
        }

        public void SetPrefab(GameObject prefab)
        {
            _prefab = prefab;
        }
    }
}

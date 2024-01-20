using UnityEngine;
using PixelCrew.Model;

namespace PixelCrew.Components.Health
{
    
    [CreateAssetMenu(menuName = "Health", fileName = "ModifyHealthDelta")]
    public class ModifyHealthSo : ScriptableObject
    {
        [SerializeField] private int _hpDelta;
        
        public void ApplyHealthDelta(GameObject target)
        {
            GameObject sessionGameObject = GameObject.Find("Session");
            sessionGameObject.GetComponent<GameSession>()._data.Hp.Value += _hpDelta;
        }
    }
}

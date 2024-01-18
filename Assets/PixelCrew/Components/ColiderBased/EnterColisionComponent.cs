using System;
using UnityEngine;
using UnityEngine.Events;


namespace PixelCrew.Utils.Components.ColiderBased
{
    public class EnterColisionComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private EnterEvent _actions;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(_tag))
            {
                _actions?.Invoke(collision.gameObject);
            }
        }

    }
    

    [Serializable]
    public class EnterEvent : UnityEvent<GameObject>
    {

    }
}

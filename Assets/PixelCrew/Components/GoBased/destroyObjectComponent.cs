using UnityEngine;


namespace PixelCrew.Components.GoBased
{
    public class destroyObjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToDestroy;
        public void DestroyObject()
        {          
            Destroy(_objectToDestroy);           
        }       
    }
}




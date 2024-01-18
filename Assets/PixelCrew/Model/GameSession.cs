using UnityEngine;
using PixelCrew.Utils.Model.Data;

namespace PixelCrew.Utils.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] public PlayerData _data;
        public PlayerData Data => _data;

        private void Awake()
        {
            if (IsSessionExit())
            {
                Destroy(gameObject);
            }
            else 
            {
                DontDestroyOnLoad(this);
            }
        }

        private bool IsSessionExit()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var gameSession in sessions)
            {
                if (gameSession != this)
                    return true;
                
            }

            return false;
        }
    }

    
}

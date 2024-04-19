using UnityEngine;
using PixelCrew.Model.Data;
using UnityEngine.SceneManagement;

namespace PixelCrew.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] public PlayerData _data;
        public PlayerData Data => _data;
        
        public HudInventoryModel InventoryModel { get; private set; }

        private void Awake()
        {
            LoadHud();
            if (IsSessionExit())
            {
                Destroy(gameObject);
            }
            else
            {
                InitModels();
                DontDestroyOnLoad(this);
            }
        }

        private void InitModels()
        {
            InventoryModel = new HudInventoryModel(Data);
        }

        private void LoadHud()
        {
            SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
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

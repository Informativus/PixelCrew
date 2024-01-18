using PixelCrew.Utils.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.Utils.Components.LevelManegement
{
    public class ReloadLevelComponent : MonoBehaviour
    {
        public void Reload()
        {
            var _session = FindObjectOfType<GameSession>();
            Destroy(_session.gameObject);

            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        
    }
}

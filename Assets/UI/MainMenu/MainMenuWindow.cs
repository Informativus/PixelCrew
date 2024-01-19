using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrew.UI.WindowAnimations;

namespace PixelCrew.UI.MainMenu
{
    public class MainMenuWindow : AnimatedWindow
    {
        private Action _closeAction;
        
        public void OnStartGame()
        {
            _closeAction = () =>
            {
                SceneManager.LoadScene("Level 1");
            };
            Close();
        }
        
        public void OnExit()
        {
            _closeAction = () =>
            {
                Application.Quit();

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            };
            Close();
        }
        
        public override void OnCloseAnimationComplete()
        {
            base.OnCloseAnimationComplete();
            _closeAction?.Invoke();
        }

    }
}

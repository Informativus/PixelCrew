using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.UI.MainMenu
{
    public class MainMenuWindow : AnimatedWindow
    {
        private Action _closeAction;
        [SerializeField] private Transform _optionsPosition;
        
        //public void OnShowSettings()
        //{
        //    _optionsPosition.position = new(0, 0);
        //}

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

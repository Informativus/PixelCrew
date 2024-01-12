using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseLevel : MonoBehaviour
{
    [SerializeField] private int _pauseState;
    public void Pause()
    {
        Time.timeScale = _pauseState;
    }
}

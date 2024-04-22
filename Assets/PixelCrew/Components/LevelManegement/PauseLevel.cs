using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyNamespace
{
    
}
public class PauseLevel : MonoBehaviour
{
    public void Pause(int pauseState)
    {
        Time.timeScale = pauseState;
    }
}

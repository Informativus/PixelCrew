using UnityEngine;
using System.Collections;

namespace PixelCrew.Utils.Creatures.Patrolling
{
    public abstract class Patrol : MonoBehaviour
    {
        public abstract IEnumerator DoPatrol();
    }
}

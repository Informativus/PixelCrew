using UnityEngine;
using System.Collections;

namespace PixelCrew.Creatures.Patrolling
{
    public abstract class Patrol : MonoBehaviour
    {
        public abstract IEnumerator DoPatrol();
    }
}

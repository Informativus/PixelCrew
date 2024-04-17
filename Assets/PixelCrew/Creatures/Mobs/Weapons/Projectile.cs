using UnityEngine;

namespace PixelCrew.Creatures.Mobs.Weapons
{
    public class Projectile : BaseProjectile
    {
        protected override void Start() 
        {
            base.Start();
            var force = new Vector2(Direction * FlySpeed, 0);
            Rigidbody.AddForce(force, ForceMode2D.Impulse);
        }
    }
}

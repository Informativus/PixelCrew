using UnityEngine;

namespace PixelCrew.Utils.Creature.Mobs.Weapons
{
    public class Projectile : BaseProjectile
    {
        protected override void Start() 
        {
            base.Start();
            var force = new Vector2(Direction * FlySpeed, 0);
            Rigidbody.AddForce(force, ForceMode2D.Impulse);
        }

        //private void FixedUpdate()
        //{   
        //    var position = Rigidbody.position;
        //    position.x += Direction * FlySpeed;
        //    Rigidbody.MovePosition(position);
        //}
    }
}

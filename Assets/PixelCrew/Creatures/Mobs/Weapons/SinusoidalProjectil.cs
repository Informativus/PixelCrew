using PixelCrew.Utils.Creature.Mobs.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Utils.Creatures.Mobs
{
    public class SinusoidalProjectil : BaseProjectile
    {
        [SerializeField] private float _frequency = 1f;
        [SerializeField] private float _amplitude = 1f;
        private float _originalY;
        private float _time;
        protected override void Start()
        {
            base.Start();
            _originalY = Rigidbody.position.y;
        }

        private void FixedUpdate()
        {
            var position = Rigidbody.position;
            position.x += Direction * FlySpeed;
            position.y = _originalY + Mathf.Sin(_time * _frequency) * _amplitude;
            Rigidbody.MovePosition(position);
            _time += Time.fixedDeltaTime;
        }
    }
}

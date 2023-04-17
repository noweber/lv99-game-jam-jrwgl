using System;
using UnityEngine;

namespace Assets.Scripts.Damage
{
    public interface IHurtBox
    {
        float GetHitPoints();

        void TakeDamage(float damage);


        event Action<GameObject> OnHurt;
    }
}

using System;
using UnityEngine;

namespace Assets.Scripts.HitHurt
{
    public interface IHurtBox
    {
        float GetHitPoints();

        void TakeDamage(float damage);

        event Action<GameObject> OnHurt;

        event Action HitPointsAreZero;
    }
}

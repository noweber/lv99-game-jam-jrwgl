using System.Collections;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts.Damage
{
    public class HitBox : MonoBehaviour
    {
        [SerializeField] protected float damage;

        [SerializeField] protected float rateOfFireInSeconds;

        protected bool canDamage;

        private void Awake()
        {
            canDamage = true;
        }

        public virtual float GetDamage()
        {
            return damage;
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(MethodBase.GetCurrentMethod().DeclaringType.Name + "::" + MethodBase.GetCurrentMethod());
            TryDamageObject(other);
        }

        protected virtual void TryDamageObject(Collider2D other)
        {
            if (canDamage)
            {
                if (!other.gameObject.CompareTag(gameObject.tag))
                {
                    canDamage = false;
                    StartCoroutine(RateOfFireDamageReset());
                    other.GetComponent<IHurtBox>()?.TakeDamage(GetDamage());
                }
            }
        }

        protected virtual IEnumerator RateOfFireDamageReset()
        {
            yield return new WaitForSeconds(rateOfFireInSeconds);
            canDamage = true;
        }
    }
}

using System;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Damage
{
    public class HurtBox : MonoBehaviour, IHurtBox
    {

        [SerializeField] private float hitPoints = 10;

        [SerializeField] private TextMeshProUGUI hitPointsText;

        [SerializeField] private GameObject damageText;

        public event Action<GameObject> OnHurt;
        public Action OnReceiveDmg;
       
        void Start()
        {
            UpdateHitPoints();
        }

        public float GetHitPoints()
        {
            return hitPoints;
        }

        public void Heal(int healAmount)
        {
            UpdateHitPoints();
            hitPoints += healAmount;
        }

        public void TakeDamage(float damage)
        {
            Debug.Log(MethodBase.GetCurrentMethod().DeclaringType.Name + "::" + MethodBase.GetCurrentMethod());
            hitPoints -= damage;
            Debug.Log("Damage: " + damage);
            UpdateHitPoints();
            //OnReceiveDmg.Invoke();
            if (hitPoints <= 0)
            {
                if (damageText != null)
                {
                    var newObject = Instantiate(damageText, transform.position, Quaternion.identity);
                    var floatingText = newObject.GetComponent<DamageText>();
                    if (floatingText != null)
                    {
                        floatingText.SetText(damage.ToString());
                    }
                }

                Destroy(gameObject);
            }
        }

        protected virtual void UpdateHitPoints()
        {

            if (hitPointsText != null)
            {
                hitPointsText.text = GetHitPoints().ToString();
            }
        }
    }
}

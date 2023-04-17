using System;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

namespace Assets.Scripts.Damage
{
    public class HurtBox : MonoBehaviour, IHurtBox
    {

        [SerializeField] private float hitPoints = 10;

        [SerializeField] private TextMeshProUGUI hitPointsText;

        [SerializeField] private GameObject damageText;

        public event Action<GameObject> OnHurt;
        public Action OnPlayerReceiveDmg;

        public Action<Vector3, int> OnDamaged;
        void Start()
        {
            UpdateHitPoints();

            if (gameObject.CompareTag("Enemy"))
            {
                OnDamaged += (Vector3 position, int damage) => { DamagePopup.Create(position, damage); };
            }

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
            //Debug.Log(MethodBase.GetCurrentMethod().DeclaringType.Name + "::" + MethodBase.GetCurrentMethod());
            hitPoints -= damage;
            //Debug.Log("Damage: " + damage);
            UpdateHitPoints();

            if (gameObject.CompareTag("Player"))
            {
                OnPlayerReceiveDmg.Invoke();
            }
            

            OnDamaged?.Invoke(transform.position, (int)damage);
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

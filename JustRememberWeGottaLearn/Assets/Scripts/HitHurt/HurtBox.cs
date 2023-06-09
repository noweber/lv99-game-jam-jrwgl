﻿using Assets.Scripts.HitHurt;
using System;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

namespace Assets.Scripts.HitHurt
{
    public class HurtBox : MonoBehaviour, IHurtBox
    {
        [SerializeField] public float maxHP = 10;

        [SerializeField] public float hitPoints = 10;

        [SerializeField] private TextMeshProUGUI hitPointsText;

        [SerializeField] private GameObject damageText;

        public event Action<GameObject> OnHurt;

        public event Action HitPointsAreZero;

        [SerializeField] private bool destroyGameObjectWhenHitPointsAreZero = true;

        public Action OnPlayerReceiveDmg;

        public Action<Vector3, int> OnDamaged;

        void Start()
        {
            UpdateHitPoints();

            if (gameObject.CompareTag(Tags.Enemy))
            {
                OnDamaged += (Vector3 position, int damage) => { DamagePopup.Create(position, damage); };
            }
            if (destroyGameObjectWhenHitPointsAreZero)
            {
                HitPointsAreZero = Die;
            }
            if(gameObject.tag == "Player")
            {
                Experience.Instance.OnLevelUp += RefillHP;
            }
        }

        public float GetHitPoints()
        {
            return hitPoints;
        }

        public void RefillHP()
        {
            hitPoints = maxHP;
        }

        public void IncreaseMaxHP(int amount)
        {
            maxHP += amount;
        }
        public void Heal(int healAmount)
        {
            UpdateHitPoints();
            hitPoints += healAmount;
        }

        public void TakeDamage(float damage)
        {
            hitPoints -= damage;

            if (this.gameObject.tag == "Player")
            {
                AudioManager.instance.RequestSFX(SFXTYPE.health_reduction);
            }
            else if (this.gameObject.tag == "Enemy")
            {
                AudioManager.instance.RequestSFX(SFXTYPE.player_attack);
            }
            if (hitPoints <=0 && this.gameObject.CompareTag("Player"))
            {
                AudioManager.instance.gameObject.SetActive(false);
            }

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
                    Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageText>().SetText(damage.ToString());
                }
                if (HitPointsAreZero != null)
                {
                    HitPointsAreZero.Invoke();
                }
            }
        }

        protected virtual void UpdateHitPoints()
        {

            if (hitPointsText != null)
            {
                hitPointsText.text = GetHitPoints().ToString();
            }
        }
        protected virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}

using TMPro;
using UnityEngine;

namespace Assets.Scripts.Damage
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;

        private TextMeshPro damageText;

        public void SetText(string value)
        {
            if (damageText != null)
            {
                damageText.text = value;
            }
        }
        void FixedUpdate()
        {
            transform.Translate(new Vector3(0, speed * Time.fixedDeltaTime, 0));
        }
    }
}
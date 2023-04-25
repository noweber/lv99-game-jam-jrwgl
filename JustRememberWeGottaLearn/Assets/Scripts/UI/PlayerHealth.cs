using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image healthBarFill;

    private void Start()
    {
        Player.Instance.playerHurtBox.OnPlayerReceiveDmg += ShowWhenPlayerIsHit;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        float hpPercentage = Player.Instance.playerHurtBox.hitPoints / Player.Instance.playerHurtBox.maxHP;
        healthBarFill.rectTransform.localScale = new Vector3(hpPercentage, 1, 1);
    }

    private void ShowWhenPlayerIsHit()
    {
        gameObject.SetActive(true);
        StartCoroutine(HideAfter2S());
    }

    private IEnumerator HideAfter2S()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);

    }
}

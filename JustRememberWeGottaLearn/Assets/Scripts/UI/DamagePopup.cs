using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public static DamagePopup Create(Vector3 position, int damageAmount)
    {
        Transform damagePopupTransform =
            Instantiate(GameManager.Instance.pfDamagePopup.transform, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);
        return damagePopup;
    }

    private TextMeshPro textMesh;
    private float moveYSpeed = 5f;
    private float disappearTimer = 0.5f;
    private float fadeSpeed = 3f;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
        Color textColor = textMesh.color;
        textColor.a = 0;

        //effect for color
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(0.5f);
        sequence.Append(textMesh.DOColor(textColor, 1f));
        sequence.onComplete = () => { Destroy(gameObject); };

        //effect for position
        transform.DOMove(transform.position + Vector3.up, 1f);

        //effect for scale
        transform.DOScale(transform.localScale * 1.2f, 1.5f);
    }
}

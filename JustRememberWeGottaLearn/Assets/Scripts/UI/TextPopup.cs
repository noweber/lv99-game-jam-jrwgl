using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    public static TextPopup Create(Vector3 position, string _text)
    {
        Transform damagePopupTransform =
            Instantiate(GameManager.Instance.pfTextPopup.transform, position, Quaternion.identity);
        TextPopup damagePopup = damagePopupTransform.GetComponent<TextPopup>();
        damagePopup.Setup(_text);
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

    public void Setup(string _text)
    {
        //Debug.Log("Setup text popup");
        //Debug.Log(_text);
        textMesh.SetText(_text.ToString());
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

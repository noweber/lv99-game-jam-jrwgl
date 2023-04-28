using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct LanguageText
{
    public Language Language;
    public string Text;
}
public class MLButton : MonoBehaviour
{
    public int dummy;
    public List<LanguageText> ButtonTexts = new List<LanguageText>();

    public void UpdateLanguage(Language language)
    {
        TextMeshProUGUI textMesh = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        if(textMesh != null)
        {
            foreach(LanguageText buttonText in ButtonTexts)
            {
                if(buttonText.Language == language)
                {
                    textMesh.text = buttonText.Text;
                }
            }
        }
    }
}

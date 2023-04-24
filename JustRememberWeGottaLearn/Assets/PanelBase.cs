using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelBase : MonoBehaviour
{
    private Button closeBTN;

    private void Awake()
    {
        closeBTN = FindObjectOfType<Button>();
        closeBTN.onClick.AddListener(OnClose);
    }

    private void OnClose()
    {
        this.gameObject.SetActive(false);
    }
}

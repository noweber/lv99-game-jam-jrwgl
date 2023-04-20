using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_StartMenu : MonoBehaviour
{

    [SerializeField]
    private Button startBtn;
    [SerializeField]
    private Button creditBtn;

    private void Awake()
    {
        if(startBtn != null)
            startBtn.onClick.AddListener(StartBtnClicked);
        if(creditBtn != null)
            creditBtn.onClick.AddListener(CreditBtnClicked);
    }

    private void StartBtnClicked()
    {
        SceneManager.LoadScene("Play");
    }

    private void CreditBtnClicked()
    {
        Debug.Log("Show all credit information");
    }
}

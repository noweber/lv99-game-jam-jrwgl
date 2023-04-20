using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_RetryPanel : MonoBehaviour
{
    [SerializeField]
    private Button retryBtn;

    private void Awake()
    {
        retryBtn.onClick.AddListener(Retry);
    }

    void Retry()
    {
        SceneManager.LoadScene("Start Scene");
    }
}

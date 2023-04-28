using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject tutorialPanel;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if(tutorialPanel.active == false)
        {
            tutorialPanel.SetActive(true);
        }
    }


}

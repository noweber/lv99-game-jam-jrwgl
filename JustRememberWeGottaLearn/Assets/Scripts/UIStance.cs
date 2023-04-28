using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStance : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        Stance.Instance.onSwitchStance += UpdateStanceUI;
    }

    void UpdateStanceUI()
    {
        _text.text = Stance.Instance.currentStance.ToString();
    }
}

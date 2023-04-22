using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBreathFrequency : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI _textMesh;

    void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //_textMesh.text = "BPM: " + TempoGenerator.Instance.GetBreathFrequency().ToString();
        _textMesh.text = "BPM: " + TempoSystem.Instance.BreathFrequency.ToString();
    }
}

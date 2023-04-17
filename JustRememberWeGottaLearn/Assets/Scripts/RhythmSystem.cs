using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditor.PackageManager.UI;
using UnityEditor.Profiling;
using UnityEngine;


public class RhythmSystem : Singleton<RhythmSystem>
{
    [SerializeField] private float frequencySamplingPeroid;
    private List<float> timeSamples = new List<float>();
    private float breathFrequency;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            timeSamples.Add(Time.time);
        }
        List<float> newTimeSamples = new List<float>();

        for (int i = 0; i < timeSamples.Count; i++)
        {
            if (Time.time - timeSamples[i] < frequencySamplingPeroid)
            {
                newTimeSamples.Add(timeSamples[i]);
            }
        }

        //Breath per minutes
        breathFrequency = (newTimeSamples.Count / frequencySamplingPeroid);
        timeSamples = newTimeSamples;
        //Debug.Log(breathFrequency);
    }

    public float GetFrequency()
    {
        return breathFrequency;
    }
}

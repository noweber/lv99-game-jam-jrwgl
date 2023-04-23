using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.QC;

public class Experience : Singleton<Experience>
{
    [SerializeField] private int m_baseExp=40;
    [SerializeField] private int m_expFactor=2;

    private int m_level = 1;
    private int m_currentExp = 0;
    
    public Action OnLevelUp;
    public Action<float> OnExpChangePercentage; // For UI
    public int Level { get { return m_level; } }
    [Command()]
    public void GainExp(int expAmount)
    {
        int nextLevelExp = m_baseExp * (m_level + 1) * m_expFactor;
        m_currentExp += expAmount;
        //Debug.Log("Gain Exp");
       
        
        if (nextLevelExp <= m_currentExp)
        {
            Debug.Log(m_level);
            m_currentExp = (m_currentExp) - nextLevelExp;
            m_level += 1;
            OnLevelUp?.Invoke();
        }

        OnExpChangePercentage?.Invoke((float)m_currentExp / nextLevelExp);
    }
}

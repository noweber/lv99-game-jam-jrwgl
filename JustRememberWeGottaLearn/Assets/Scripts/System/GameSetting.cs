using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Language
{
    English,
    Chinese,
    Japanese,
    French,
    Spanish
}
public class GameSetting : Singleton<GameSetting>
{
    [SerializeField] private Language m_language  = Language.English;

    public Language Language
    {
        get { return m_language; }
    }

    public Action<Language> OnLanguageChange;

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        
    }

    




}

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
    [SerializeField] private Language DefaultLanguage = Language.English;

    public Language language { get; private set; }

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        language = DefaultLanguage;
    }




}

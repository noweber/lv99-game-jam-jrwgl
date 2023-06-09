using UnityEngine;
using UnityEngine.UI;

public class PlayerOOBEffect : MonoBehaviour
{
    public Color originalColor;
    public Image image;
    public float timeRemaining = 0f;

    private bool _eventInitialized = false;
    public void Awake()
    {
        originalColor = image.color;
        originalColor.a = 0.6f;
        image.enabled = true;
    }

    private void Start()
    {
        TempoSystem.Instance.OnBpmChange += TiggerEffect;
        //Player.Instance.GetComponent<HurtBox>().OnPlayerReceiveDmg += TiggerEffect;
    }

    
    public void TiggerEffect(BPM bpm)
    {
        //Debug.Log("Triggered injure effects");
        if (bpm == BPM.bpm180plus)
        {
            timeRemaining = 100.0f;
        }
        else
        {
            timeRemaining = 0.0f;
        }
    }
    /*
    public void TiggerEffect()
    {
        //Debug.Log("Triggered injure effects");
        timeRemaining = 0.4f;
    }
    */
    private void Update()
    {
        Color transparent = originalColor;
        transparent.a = 0;
        if (timeRemaining >= 0)
        {

            image.color = Color.Lerp(transparent, originalColor, (timeRemaining) / 0.4f);
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            image.color = transparent;
        }
    }
}
